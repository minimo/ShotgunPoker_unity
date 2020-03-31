using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    //スイート番号
    // 0:スペード 1:クローバー 2:ダイヤ 3:ハート
    int _suit = 0;
    int _num = 1;
    bool _isReverse = false;
    bool _isJoker = false;

    public int sortKey = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_isReverse) setFrameIndex(53);
    }

    public Sprite GetSprite(string fileName , string spriteName) {
        Sprite[] sprites = Resources.LoadAll<Sprite>(fileName);
        return System.Array.Find<Sprite>(sprites, (sprite) => sprite.name.Equals(spriteName));
    }

    public Card setFrameIndex(int index) {
        string name = "trump_" + index;
        Sprite sp = GetSprite("Textures/trump", name);
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = sp;
        return this;
    }

    public Card setData(int suit, int num, bool isJoker) {
        int index = suit * 13 + num - 1;
        if (isJoker) {
            index = 4 * 13 + 2;
            _isJoker = true;
            _suit = 0;
            _num = 0;
            sortKey = 99;
        } else {
            _suit = suit;
            _num = num;
            sortKey = _num;
        }
        setFrameIndex(index);
        return this;
    }

    public Card joker() {
        _isJoker = true;
        setFrameIndex(4 * 13 + 2);
        return this;
    }

    public int getSuit() {
        return _suit;
    }

    public int getNumber() {
        return _num;
    }

    public bool isJoker() {
        return _isJoker;
    }

    public Card reverse(bool flag) {
        _isReverse = flag;
        if (flag) {
            setFrameIndex(52);
        } else {
            setData(_suit, _num, _isJoker);
        }
        return this;
    }

    public bool isReverse() {
        return _isReverse;
    }

    public void onTouch() {
        this.transform.parent.gameObject.GetComponent<GameController>().AddCard(this.gameObject);
    }
}
