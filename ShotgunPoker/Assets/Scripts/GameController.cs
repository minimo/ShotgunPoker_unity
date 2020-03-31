using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject HandChecker;
    List<GameObject> hands = new List<GameObject>();

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
        Sequence seq = DOTween.Sequence();
        float x = -2.0f + hands.Count * 0.5f;
        seq.Append(card.transform.DOMove(new Vector3(x, -5.0f, hands.Count * 0.1f), 0.2f));
        seq.Join(card.transform.DORotate(new Vector3(0.0f, 0.0f, 0.0f), 0.2f));
        seq.OnComplete(() => {
            hands.Add(card);
            if (hands.Count == 5) {
                complete();
            }
        });
    }

    void complete() {
        Sequence seq = DOTween.Sequence();
        hands.ForEach(card => seq.Join(card.transform.DOMove(new Vector3(-2.0f, -5.0f), 0.2f)));
        seq.AppendCallback(() => {
            hands.Sort((a, b) => a.GetComponent<Card>().sortKey - b.GetComponent<Card>().sortKey);
            int count = 0;
            hands.ForEach(card => {
                Vector3 p = card.transform.position;
                card.transform.position = new Vector3(p.x, p.y, count * 0.1f);
                float x = -2.0f + count * 0.5f;
                seq.Join(card.transform.DOMove(new Vector3(x, -5.0f, count * 0.1f), 0.2f));
                count++;
            });
            PokerHand result = GetComponent<HandChecker>().check(hands);
            Debug.Log(GetComponent<HandChecker>().getHandName(result));
        });
    }
}
