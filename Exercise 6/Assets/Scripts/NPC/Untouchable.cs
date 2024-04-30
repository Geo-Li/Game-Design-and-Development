using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Untouchable : MonoBehaviour
{
    private Vector2 respawnPosition;
    [SerializeField]
    private float spawnX = -21f;
    [SerializeField]
    private float spawnY = 38f;
    [SerializeField]
    private AudioSource hit;
    [SerializeField]
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        respawnPosition = new Vector2(spawnX, spawnY);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            hit.Play();
            player.position = respawnPosition;
        }
    }
}
