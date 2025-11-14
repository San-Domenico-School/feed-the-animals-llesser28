using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper Instance;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI remainingText;

    private int score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    public void UpdateScore (int changeInScore)
    {
        score += changeInScore;

        scoreText.text = "Score: " + score.ToString();
    } 
        
    public void UpdateRemaining(int animalsRemaining)
    {
        
        if (animalsRemaining < 0)

            animalsRemaining = 0;

        remainingText.text = "Animals Remaining: " + animalsRemaining.ToString();


    }
}



