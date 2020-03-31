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
    List<GameObject> hands = new List<GameObject>();
    List<GameObject> outsideCards = new List<GameObject>();

    bool isDisableTouch = false;

    // Start is called before the first frame update
    void Start()
    {
        setupCardDeck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setupCardDeck()
    {
        List<GameObject> cards = new List<GameObject>();
        for (int s = 0; s < 4; s++) {
            for (int n = 0; n < 13; n++) {
                float x = Random.Range(-2.0f, 2.0f);
                float y = Random.Range(-3.0f, 3.0f);
                float r = Random.Range(0, 359);
                GameObject card = enterCard(s, n + 1, false);
                card.transform.position = new Vector3(x, y, 0.0f);
                card.transform.rotation = Quaternion.Euler(0.0f, 0.0f, r);
                cards.Add(card);
            }
        }
        //シャッフル
        for (int i = 0; i < cards.Count; i++) {
            GameObject temp = cards[i];
            int randomIndex = Random.Range(0, cards.Count);
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
        int count = 0;
        cards.ForEach(card => {
            Vector3 p = card.transform.position;
            card.transform.position = new Vector3(p.x, p.y, count * 0.1f);
            count++;
        });
    }

    GameObject enterCard(int suit, int num, bool isJoker = false)
    {
        GameObject card;
        card = Instantiate((GameObject)Resources.Load("PreFab/Card"));
        card.transform.SetParent(this.transform);
        card.GetComponent<Card>().setData(suit, num, isJoker);
        return card;
    }

    public void AddCard(GameObject card)
    {
        if (isDisableTouch) return;
        int count = hands.Count;
        hands.Add(card);
        Sequence seq = DOTween.Sequence();
        float x = -2.0f + count * 0.5f;
        seq.Append(card.transform.DOMove(new Vector3(x, -5.0f, count * -0.1f), 0.2f));
        seq.Join(card.transform.DORotate(new Vector3(0.0f, 0.0f, 0.0f), 0.2f));
        seq.OnComplete(() => {
            if (hands.Count == 5) {
                complete();
            }
        });
    }

    public void shuffle() {
        
    }

    void complete() {
        isDisableTouch = true;
        int count = 0;
        hands.Sort((a, b) => a.GetComponent<Card>().sortKey - b.GetComponent<Card>().sortKey);
        hands.ForEach(card => {
            float x = -2.0f + count * 0.5f;
            var seq = DOTween.Sequence();
            card.GetComponent<Card>().reverse(true);
            seq.Append(card.transform.DOMove(new Vector3(-2.0f, -5.0f), 0.2f))
                .AppendCallback(() => {
                    Vector3 p = card.transform.position;
                    card.transform.position = new Vector3(p.x, p.y, count * -0.1f);
                    card.GetComponent<Card>().reverse(false);
                })
                .Append(card.transform.DOMove(new Vector3(x, -5.0f, count * -0.1f), 0.2f));
            if (count == 4) {
                seq.AppendInterval(0.5f)
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
        outsideCards.ForEach(card => {
            Vector3 p = card.transform.position;
            float x = Random.Range(-2.0f, 2.0f);
            float y = Random.Range(-3.0f, 3.0f);
            float r = Random.Range(0, 359);
            card.transform.position = new Vector3(x, p.y);
            card.transform.DOMove(new Vector3(x, y), 0.5f);
            card.transform.DORotate(new Vector3(0, 0, r), 0.5f);
        });
        outsideCards.Clear();
    }
}
