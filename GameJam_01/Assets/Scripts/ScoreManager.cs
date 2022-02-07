using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp_score;
    [SerializeField] private int score;

    public void Init()
    {

    }

    public void IncreaseScore(int value)
    {
        this.score += value;

        tmp_score.text = "Score: " + this.score.ToString();
    }
}
