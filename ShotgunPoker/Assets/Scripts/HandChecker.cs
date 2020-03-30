using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    PokerHand check(List<GameObject> hands)
    {
        PokerHand result = new PokerHand();
        if (hands.Count < 5) {
            result.isMiss = true;
            return result;
        }
        return result;
    }
}

public class PokerHand {
    public bool isMiss = false;
    public bool isOnePair = false;
    public bool isTwoPair = false;
    public bool isThreeCard = false;
    public bool isFourCard = false;
    public bool isFiveCard = false;
    public bool isFlush = false;
    public bool isStraight = false;
    public bool isRoyal = false;
}
