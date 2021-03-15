using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int PlayerLives = 3;

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

    }

    public void PlayerDeath()
    {
        PlayerLives--;

        if (PlayerLives <= 0)
        {
            SceneManager.LoadScene(0);

            Destroy(gameObject);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
