using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        setupCardDeck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setupCardDeck() {
        for (int i = 0; i < 52; i++) {
            float x = Random.Range(-2.0f, 2.0f);
            float y = Random.Range(-3.0f, 3.0f);
            float r = Random.Range(0, 359);
            enterCard(i, x, y, r);
        }
    }

    void enterCard(int index, float x, float y, float r) {
        GameObject card;
        card = Instantiate((GameObject)Resources.Load("Card"));
        card.transform.SetParent(this.transform);
        card.transform.position = new Vector3(x, y, 0.0f);
        card.transform.rotation = Quaternion.Euler(0.0f, 0.0f, r);
        card.GetComponent<Card>().setFrameIndex(index);
    }
}
