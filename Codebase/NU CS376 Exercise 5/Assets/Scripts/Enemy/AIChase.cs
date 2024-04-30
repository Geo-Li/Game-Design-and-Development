using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float moveSpeed = 15;
    private float distance;
    private bool facingRight = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Flip() {
        Vector3 currScale = gameObject.transform.localScale;
        currScale.x *= -1;
        gameObject.transform.localScale = currScale;
        facingRight = !facingRight;
    }

    void FixedUpdate()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        if (direction.x > 0 && !facingRight) {
            Flip();
        } else if (direction.x < 0 && facingRight) {
            Flip();
        }
    }
}
