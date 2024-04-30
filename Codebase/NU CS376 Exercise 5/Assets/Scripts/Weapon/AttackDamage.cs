using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AttackDamage : MonoBehaviour
{
    [SerializeField]
    private int attackDamage = 4;

    public int GetAttackDamage() {
        return attackDamage;
    }

    public void DestroyWeapon() {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.GetComponent<EnemyHealth>() != null) {
            DestroyWeapon();
        }
    }
}
