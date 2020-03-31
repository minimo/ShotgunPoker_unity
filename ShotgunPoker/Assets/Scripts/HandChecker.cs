using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandChecker : MonoBehaviour
{
    public PokerHand check(List<GameObject> hands)
    {
        PokerHand result = new PokerHand();
        if (hands.Count < 5) {
            result.isMiss = true;
            return result;
        }
        List<int> numbers = new List<int>();
        hands.ForEach(card => numbers.Add(card.GetComponent<Card>().getNumber()));
        List<int> suits = new List<int>();
        hands.ForEach(card => suits.Add(card.GetComponent<Card>().getSuit()));

        //手札内ジョーカー有り判定
        bool inJoker = false;
        hands.ForEach(card => {
            if (card.GetComponent<Card>().isJoker()) inJoker = true;
        });

        //フラッシュ判別
        result.isFlush = true;
        int max = inJoker ? 4: 5;
        int suit = suits[0];
        for (int i = 1; i < max; i++) {
            if (suit != suits[i]) result.isFlush = false;
        }

        //ストレート判別
        result.isStraight = true;
        if (!inJoker) {
            //通常の判定
            int start = numbers[0] + 1;
            for (int i = 1; i < 5; i++) {
                if (start != numbers[i]) result.isStraight = false;
                start++;
            }
            //特殊なストレート（ロイヤルストレート）
            if (!result.isStraight) {
                result.isStraight = true;
                if (numbers[0] == 1 && numbers[1] == 10) {
                    start = numbers[1] + 1;
                    for (int i = 2; i < 5; i++) {
                        if (start != numbers[i]) result.isStraight = false;
                        start++;
                    }
                } else {
                    result.isStraight = false;
                }
                if (result.isStraight) result.isRoyal = true;
            }
        } else {
            //ジョーカー有りの場合
            int skip = 0;
            int count = 0;
            result.isStraight = false;
            int start = numbers[0] + 1;
            for (int i = 1; i < 4; i++) {
                if (start == numbers[i]) count++;
                if (start + 1 == numbers[i] && skip < 1) {
                    skip++;
                    count++;
                    start++;
                }
                start++;
            }
            if (count == 3) result.isStraight = true;
        }

        //ストレートorフラッシュの場合は役確定	
        if (result.isStraight || result.isFlush) return result;

        //スリーカード、フォーカード判別
        if (numbers[0] == numbers[3] || numbers[1] == numbers[4]) {
            if (inJoker) {
                result.isFiveCard = true;
            } else {
                result.isFourCard = true;
            }
            return result;
        }
        bool three = false;
        if (numbers[0] == numbers[2] || numbers[1] == numbers[3] || numbers[2] == numbers[4]) {
            if (inJoker) {
                result.isFourCard = true;
                return result;
            } else {
                result.isThreeCard = true;
            }
        }

        //スリーカード成立の場合は前か後二枚を判別
        if (result.isThreeCard) {
            if (numbers[0] == numbers[2] && numbers[3] == numbers[4]
            || numbers[2] == numbers[4] && numbers[0] == numbers[1]) {
                result.isFullHouse = true;
                result.isThreeCard = false;
            }
            return result;
        }

        //ペア判別
        int pair = 0;
        for (int i = 0; i < 5; i++) {
            for (int j = i+1; j < 5; j++) {
                if (numbers[i] == numbers[j]) pair++;
            }
        }
        if (pair == 1) {
            if (inJoker) result.isThreeCard = true; else result.isOnePair = true;
            return result;
        }
        if (pair == 2) {
            if (inJoker) result.isFullHouse = true; else result.isTwoPair = true;
            return result;
        }

        //ハイカード判定
        if (inJoker) result.isOnePair = true; else result.isHighCard = true;
        return result;
    }

    public string getHandName(PokerHand hand) {
        if (hand.isHighCard) return "High card";
        if (hand.isOnePair) return "One pair";
        if (hand.isTwoPair) return "Two pair";
        if (hand.isThreeCard) return "Three card";
        if (hand.isFourCard) return "Four card";
        if (hand.isFiveCard) return "Five card";
        if (hand.isFullHouse) return "Full house";
        if (hand.isStraight) {
            if (hand.isFlush) {
                if (hand.isRoyal) return "Royal Straight Flush";
                return "Straight Flush";
            }
            return "Straight";
        }
        if (hand.isFlush) return "Flush";
        if (hand.isHighCard) return "High card";
        return "Miss";
    }
}

public class PokerHand {
    public bool isMiss = false;
    public bool isHighCard = false;
    public bool isOnePair = false;
    public bool isTwoPair = false;
    public bool isThreeCard = false;
    public bool isFourCard = false;
    public bool isFiveCard = false;
    public bool isFlush = false;
    public bool isStraight = false;
    public bool isRoyal = false;
    public bool isFullHouse = false;
}
