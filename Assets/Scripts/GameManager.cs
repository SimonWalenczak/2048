using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TileBoard _board;
    public CanvasGroup gameOver;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HighScoreText;

    private int score;
    
    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        SetScore(0);
            
        //PlayerPrefs.SetInt("HighScore", 0);
        
        HighScoreText.text = LoadHighScore().ToString();
        
        gameOver.alpha = 0f;
        gameOver.interactable = false;
        
        _board.ClearBoard();
        _board.CreateTile();
        _board.CreateTile();
        _board.enabled = true;
    }

    public void GameOver()
    {
        _board.enabled = false;
        gameOver.interactable = true;
        
        StartCoroutine(Fade(gameOver, 1f, 1f));
    }

    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;

        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = to;
    }

    public void IncreaseScore(int points)
    {
        SetScore(score + points);
    }
    
    public void SetScore(int score)
    {
        this.score = score;
        ScoreText.text = score.ToString();
        
        SaveHighScore();
    }

    public void SaveHighScore()
    {
        int highScore = LoadHighScore();

        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    private int LoadHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }
}