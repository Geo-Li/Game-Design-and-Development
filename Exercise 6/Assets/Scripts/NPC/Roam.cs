using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roam : MonoBehaviour
{
    // private Rigidbody2D npcRB;
    [SerializeField]
    private bool facingRight = true;
    [SerializeField]
    private float moveSpeed = 0.1f;

    // Left Point
    [Header("Left Point")]
    [SerializeField]
    private float leftX;
    [SerializeField]
    private float leftY;

    // Right Point
    [Header("Right Point")]
    [SerializeField]
    private float rightX;
    [SerializeField]
    private float rightY;

    private Vector2 target;

    // Start is called before the first frame update
    void Start()
    {
        // npcRB = GetComponent<Rigidbody2D>();
        target = new Vector2(rightX, rightY);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= rightX) {
            Flip();
            target = new Vector2(leftX, leftY);
        } else if (transform.position.x <= leftX) {
            Flip();
            target = new Vector2(rightX, rightY);
        }
        transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed);
    }

    private void Flip() {
        Vector3 currScale = gameObject.transform.localScale;
        currScale.x *= -1;
        gameObject.transform.localScale = currScale;
        facingRight = !facingRight;
    }
}
