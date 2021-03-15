using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float LevelLoadDelay = 2.0f;
    [SerializeField] float LevelExitSlowMotionFactor = 0.2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(ExitTheLevel());
    }

    private IEnumerator ExitTheLevel()
    {
        Time.timeScale = LevelExitSlowMotionFactor;

        yield return new WaitForSecondsRealtime(LevelLoadDelay);

        Time.timeScale = 1.0f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
