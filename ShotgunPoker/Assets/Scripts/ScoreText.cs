using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    public int score = 0;
    public int dispScore = 0;

    public int difference = 0;
    TextMeshProUGUI textMesh;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = gameObject.GetComponent<TextMeshProUGUI>();        
    }

    // Update is called once per frame
    void Update()
    {
        dispScore += difference;
        if (dispScore > score) dispScore = score;
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
