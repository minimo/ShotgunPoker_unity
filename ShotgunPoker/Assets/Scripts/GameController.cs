using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject HandChecker;

    List<GameObject> cardList = new List<GameObject>();
    List<GameObject> hands = new List<GameObject>();
    List<GameObject> outsideCards = new List<GameObject>();

    GameObject outSide;

    bool isDisableTouch = false;

    private Vector3 touchStartPos;
    private Vector3 touchEndPos;

    private float deltaTime = 0;

    void Start()
    {
        outSide = new GameObject();
        outSide.name = "OutSide";
        outSide.transform.SetParent(this.transform);
        setupCardDeck();
    }

    // Update is called once per frame
    void Update()
    {
        detectFlick();
        deltaTime += Time.deltaTime;
    }

    void detectFlick(){
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            touchStartPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            deltaTime = 0;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && deltaTime < 0.2f){
            touchEndPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            float directionX = touchEndPos.x - touchStartPos.x;
            float directionY = touchEndPos.y - touchStartPos.y;
            string flickDir = "";
            if (Mathf.Abs(directionY) < Mathf.Abs(directionX)) {
                Debug.Log(deltaTime);
                if (Mathf.Abs(directionX) > 600) {
                    if (600 < directionX){
                        //右向きにフリック
                        flickDir = "right";
                    } else if (-600 > directionX){
                        //左向きにフリック
                        flickDir = "left";
                    }
                }
            } else if (Mathf.Abs(directionX) < Mathf.Abs(directionY)) {
                if (130 < directionY){
                    //上向きにフリック
                    flickDir = "up";
                } else if (-30 > directionY) {
                    //下向きのフリック
                    flickDir = "down";
                }
            } else {
                //タッチを検出
                flickDir = "touch";
            }
            switch(flickDir) {
                case "right":
                case "left":
                    shuffle();
                    break;
            }
        }
    }


    void setupCardDeck()
    {
        for (int s = 0; s < 4; s++) {
            for (int n = 0; n < 13; n++) {
                float x = Random.Range(-2.0f, 2.0f);
                float y = Random.Range(-3.0f, 3.0f);
                float r = Random.Range(0, 359);
                GameObject card = enterCard(s, n + 1, false);
                card.transform.position = new Vector3(x, y, 0.0f);
                card.transform.rotation = Quaternion.Euler(0.0f, 0.0f, r);
                cardList.Add(card);
            }
        }
        //シャッフル
        for (int i = 0; i < cardList.Count; i++) {
            GameObject temp = cardList[i];
            int randomIndex = Random.Range(0, cardList.Count);
            cardList[i] = cardList[randomIndex];
            cardList[randomIndex] = temp;
        }
        int count = 0;
        cardList.ForEach(card => {
            Vector3 p = card.transform.position;
            card.transform.position = new Vector3(p.x, p.y);
            card.GetComponent<Card>().setZOrder(count + 1);
            count++;
        });
    }

    GameObject enterCard(int suit, int num, bool isJoker = false, int order = 1)
    {
        GameObject card;
        card = Instantiate((GameObject)Resources.Load("PreFab/Card"));
        card.transform.SetParent(this.transform);
        card.GetComponent<Card>().setData(suit, num, isJoker);
        card.GetComponent<Card>().setZOrder(order);
        return card;
    }

    public void addHand(GameObject card)
    {
        if (isDisableTouch) return;
        card.transform.SetParent(outSide.transform);
        float x = -1.0f + hands.Count * 0.5f;
        float y = -5.0f;
        hands.Add(card);
        if (hands.Count == 5) isDisableTouch = true;
        int _count = hands.Count;
        Sequence seq = DOTween.Sequence();
        seq.Append(card.transform.DOMove(new Vector3(x, y), 0.2f).SetEase(Ease.InOutSine));
        seq.Join(card.transform.DORotate(new Vector3(0.0f, 0.0f, 0.0f), 0.2f));
        seq.OnComplete(() => {
            card.GetComponent<Card>().setZOrder(_count);
            if (_count == 5) {
                complete();
            }
        });
    }

    public void shuffle() {
        isDisableTouch = true;
        int count = 1;
        foreach (Transform childTransform in transform) {
            if (childTransform.gameObject.name == "OutSide") continue;
            float x = Random.Range(-2.0f, 2.0f);
            float y = Random.Range(-3.0f, 3.0f);
            float r = Random.Range(0, 359);
            Sequence seq = DOTween.Sequence();
            seq.Append(childTransform.transform.DOMove(new Vector3(x, y), 0.5f).SetEase(Ease.InOutSine));
            seq.Join(childTransform.transform.DORotate(new Vector3(0.0f, 0.0f, r), 0.5f));
            seq.AppendCallback(() => isDisableTouch = false);
            childTransform.gameObject.GetComponentInParent<Card>().setZOrder(count);
            count++;
        }
    }

    void complete() {
        int count = 0;
        hands.Sort((a, b) => a.GetComponent<Card>().sortKey - b.GetComponent<Card>().sortKey);
        hands.ForEach(card => {
            int _count = count;
            float x = -1.0f + count * 0.5f;
            var seq = DOTween.Sequence();
            card.GetComponent<Card>().reverse(true);
            seq.Append(card.transform.DOMove(new Vector3(-1.0f, -5.0f), 0.2f))
                .AppendCallback(() => {
                    Vector3 p = card.transform.position;
                    card.transform.position = new Vector3(p.x, p.y, count * -0.00f);
                    card.GetComponent<Card>().reverse(false);
                    card.GetComponent<Card>().setZOrder(_count + 1);
                })
                .Append(card.transform.DOMove(new Vector3(x, -5.0f, count * -0.00f), 0.2f));
            if (count == 4) {
                seq.AppendInterval(1.0f)
                    .AppendCallback(() => exitCard());
            }
            count++;
        });

        PokerHand result = GetComponent<HandChecker>().check(hands);
        Debug.Log(GetComponent<HandChecker>().getHandName(result));
    }

    //手札を場から下げる
    void exitCard() {
        int count = 0;
        hands.ForEach(card => {
            Vector3 p = card.transform.position;
            card.transform.DOMove(new Vector3(p.x, p.y - 3.0f), 0.2f);
            outsideCards.Add(card);
            count++;
        });
        hands.Clear();
        isDisableTouch = false;
        Debug.Log("exit card");
        if (outsideCards.Count > 19) returnCard();
    }

    //下げられたカードを場に戻す
    void returnCard() {
        shuffle();
        outsideCards.ForEach(card => {
            Vector3 p = card.transform.position;
            float x = Random.Range(-2.0f, 2.0f);
            float y = Random.Range(-3.0f, 3.0f);
            float r = Random.Range(0, 359);
            card.transform.position = new Vector3(x, p.y);
            card.transform.DOMove(new Vector3(x, y), 0.5f).SetEase(Ease.OutQuad);
            card.transform.DORotate(new Vector3(0, 0, r), 0.5f);
            card.transform.SetParent(this.transform);
        });
        outsideCards.Clear();

        //下げ札を含めた表示順のシャッフル
        for (int i = 0; i < cardList.Count; i++) {
            GameObject temp = cardList[i];
            int randomIndex = Random.Range(0, cardList.Count);
            cardList[i] = cardList[randomIndex];
            cardList[randomIndex] = temp;
        }
        int count = 0;
        cardList.ForEach(card => {
            Vector3 p = card.transform.position;
            card.transform.position = new Vector3(p.x, p.y);
            card.GetComponent<Card>().setZOrder(count + 1);
            count++;
        });
    }
}
