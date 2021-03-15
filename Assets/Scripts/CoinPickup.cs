using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip CoinPickupSFX;
    [SerializeField] int ScoreValue = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(CoinPickupSFX, Camera.main.transform.position);
        FindObjectOfType<GameSession>().IncreaseScore(ScoreValue);

        Destroy(gameObject);
    }
}
