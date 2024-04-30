using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRB;
    private Animator animator;

    // For Movement
    private float horizontal;
    private float vertical;
    private bool facingRight = false;
    [Header("Movement")]
    [SerializeField]
    private float moveSpeed;

    // For Jump
    private bool isGrounded = false;
    [Header("Jump")]
    [SerializeField]
    private float jumpForce = 20;
    [SerializeField]
    private float gravityScale = 10;
    [SerializeField]
    private float fallGravityScale = 15;
    [SerializeField]
    private AudioSource jumpSFX;

    // For Dashing
    private bool canDash = true;
    private bool isDashing = false;
    [Header("Dashing")]
    [SerializeField]
    private float dashingPower = 24f;
    [SerializeField]
    private float dashingTime = 0.2f;
    [SerializeField]
    private float dashingCooldown = 1f;
    [SerializeField]
    private AudioSource dashSFX;
    
    // For Climbing Ladder
    [Header("Ladder Climbing")]
    [SerializeField]
    private float climbingSpeed = 8f;
    [SerializeField]
    private AudioSource ladderClimbingSFX;
    private bool isLadder;


    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing) {
            return;
        }
        // Player Movement
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(horizontal) > 0) {
            animator.SetBool("isRunning", true);
            playerRB.velocity = new Vector2(horizontal * moveSpeed, playerRB.velocity.y);
            if (horizontal > 0 && !facingRight) {
                Flip();
            } else if (horizontal < 0 && facingRight) {
                Flip();
            }
        } else {
            animator.SetBool("isRunning", false);
        }
        // Player Jump
        if (Input.GetKeyDown("space") && isGrounded) {
            animator.SetBool("isRunning", false);
            jumpSFX.Play();
            playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        if (playerRB.velocity.y > 0) {
            playerRB.gravityScale = gravityScale;
        } else {
            playerRB.gravityScale = fallGravityScale;
        }
        // Player Dash
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && canDash) {
            StartCoroutine(Dash());
        }
        // Climbing Ladder
        if (isLadder && Mathf.Abs(vertical) > 0) {
            playerRB.gravityScale = 0f;
            playerRB.velocity = new Vector2(playerRB.velocity.x, vertical * climbingSpeed);
        } else {
            playerRB.gravityScale = fallGravityScale;
        }
    }

    private void Flip() {
        Vector3 currScale = gameObject.transform.localScale;
        currScale.x *= -1;
        gameObject.transform.localScale = currScale;
        facingRight = !facingRight;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Ladder") {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Ladder") {
            isLadder = false;
        }
    }

    private IEnumerator Dash() {
        animator.SetBool("isRunning", false);
        dashSFX.Play();
        canDash = false;
        isDashing = true;
        float originalGravity = playerRB.gravityScale;
        playerRB.gravityScale = 0f;
        playerRB.velocity = new Vector2(-transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        playerRB.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    // void FixedUpdate() {
    //     if (horizontal > 0 || horizontal < 0) {
    //         playerRB.velocity = new Vector2(horizontal, 0) * moveSpeed;
    //     }
    // }
}
