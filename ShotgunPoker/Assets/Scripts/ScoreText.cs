using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    public int score = 0;
    int dispScore = 0;

    int difference = 0;
    TextMeshProUGUI textMesh;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = gameObject.GetComponent<TextMeshProUGUI>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (difference != 0) {
            dispScore += difference;
            if (difference > 0 &&  dispScore > score || difference < 0 &&  dispScore < score) {
                dispScore = score;
                difference = 0;
            }
        }
        textMesh.text = "Score " + dispScore;
    }

    public void addScore(int value)
    {
        if (value == 0) return;
        score += value;
        difference = (score - dispScore) / 30;
        if (difference == 0) difference = 1;
    }
}
