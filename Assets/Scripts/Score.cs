using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour
{

    public Text scoreText;
    public static float score = 0f;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void FixedUpdate()
    {
        if (scoreText != null)
        {
            score += 1 * Time.deltaTime;
            scoreText.text = score.ToString("0");
        }

    }
}
