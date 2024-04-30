using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private Rigidbody2D playerRB;
    private PlayerMovement playerMovement;

    private PlayerInput input = null;
    private Animator animator = null;

    [SerializeField]
    private GameObject weaponPrefab;
    // private float timeBetweenAttack;
    // [SerializeField]
    // private float attackStartTime;
    private int damage;

    private void Awake() {
        playerRB = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        input = new PlayerInput();
        animator = GetComponent<Animator>();
    }

    private void OnEnable() {
        input.Enable();
        input.Player.Attack.performed += Attack;
    }

    private void OnDisable() {
        input.Disable();
        input.Player.Attack.performed -= Attack;
    }


    private void FixedUpdate() {
    }

    private void Attack(InputAction.CallbackContext value) {
        animator.SetTrigger("attack");
        var weaponPos = new Vector2(playerRB.position.x + playerRB.transform.right.x, playerRB.position.y);
        if (!playerMovement.GetFacingDirection()) {
            weaponPos = new Vector2(playerRB.position.x - playerRB.transform.right.x, playerRB.position.y);
        }
        var weapon = Instantiate(weaponPrefab, weaponPos, Quaternion.identity);
    }
}
