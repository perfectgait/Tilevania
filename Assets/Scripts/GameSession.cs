using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int PlayerLives = 3;
    [SerializeField] int Score = 0;
    [SerializeField] Text LivesText;
    [SerializeField] Text ScoreText;

    private void Awake()
    {
        int numberOfGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numberOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        LivesText.text = PlayerLives.ToString();
        ScoreText.text = Score.ToString();
    }

    public void PlayerDeath()
    {
        PlayerLives--;
        PlayerLives = Mathf.Max(0, PlayerLives);

        if (PlayerLives <= 0)
        {
            SceneManager.LoadScene(0);

            Destroy(gameObject);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            LivesText.text = PlayerLives.ToString();
        }
    }

    public void IncreaseScore(int amount)
    {
        Score += amount;
        ScoreText.text = Score.ToString();
    }
}
