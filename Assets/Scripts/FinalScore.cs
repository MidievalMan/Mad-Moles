using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour
{

    public Text finalScore;

    void Awake()
    {
        finalScore.text = "Score: " + Score.score.ToString("0");
    }

}
