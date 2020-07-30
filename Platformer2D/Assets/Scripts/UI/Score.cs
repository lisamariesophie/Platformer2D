using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score instance;
    [Header("Score Settings")]
    public int scoreNeeded;
    private int score = 0;
    private TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        scoreText.text = 0 + "/" + scoreNeeded;
        if (instance == null)
        {
            instance = this;
        } 
    }

    public void IncreaseScore()
    {
        score += 1;
        scoreText.text = score.ToString() + "/" + scoreNeeded;
    }

    public int GetScore(){
        return score;
    }

    public int GetNeedScore(){
        return scoreNeeded;
    }
}
