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
    [SerializeField] RetroSprite retroSprite;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_isReverse) setFrameIndex(53);
    }

    public void setFrameIndex(int index) {
        retroSprite.setFrameIndex(index);
    }

    public void setData(int suit, int num) {
        int index = suit * 13 + num - 1;
        if (_isJoker) index = 4 * 13 + 2;
        setFrameIndex(index);
    }

    public void joker() {
        _isJoker = true;
        int index = 4 * 13 + 2;
        setFrameIndex(index);
    }

    public bool isJoker() {
        return _isJoker;
    }

    public void reverse(bool flag) {
        _isReverse = flag;
    }

    public bool isReverse() {
        return _isReverse;
    }
}
