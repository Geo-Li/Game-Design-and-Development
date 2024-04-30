using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput input = null;
    private Vector2 moveVector = Vector2.zero;
    private Rigidbody2D playerRB = null;

    private bool facingRight = false;

    [SerializeField]
    private float moveSpeed = 10;
    private Animator animator = null;

    private void Awake() {
        input = new PlayerInput();
        playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable() {
        input.Enable();
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCanceled;
    }

    private void OnDisable() {
        input.Disable();
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCanceled;
    }

    private void Flip() {
        Vector3 currScale = gameObject.transform.localScale;
        currScale.x *= -1;
        gameObject.transform.localScale = currScale;
        facingRight = !facingRight;
    }

    private void FixedUpdate() {
        playerRB.velocity = moveVector * moveSpeed;
        if (moveVector.x > 0 && !facingRight) {
            Flip();
        } else if (moveVector.x < 0 && facingRight) {
            Flip();
        }
    }

    public bool GetFacingDirection() {
        return facingRight;
    }

    private void OnMovementPerformed(InputAction.CallbackContext value) {
        moveVector = new Vector2(value.ReadValue<Vector2>().x, 0);
        // moveVector = value.ReadValue<Vector2>();
        animator.SetBool("isRunning", true);
    }

    private void OnMovementCanceled(InputAction.CallbackContext value) {
        moveVector = Vector2.zero;
        animator.SetBool("isRunning", false);
    }
}
