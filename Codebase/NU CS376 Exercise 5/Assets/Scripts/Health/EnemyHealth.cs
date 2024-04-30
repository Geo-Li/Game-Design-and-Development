using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 20;
    private int currHealth;
    [SerializeField]
    private HealthBar healthBar;

    [SerializeField]
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator waitSceneCoroutine() {
        yield return new WaitForSeconds(1);
    }

    public void TakeDamage(int damage) {
        currHealth -= damage;
        healthBar.SetHealth(currHealth);
        if (currHealth <= 0) {
            StartCoroutine(waitSceneCoroutine());
            SceneManager.LoadScene(1);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<AttackDamage>() != null) {
            audioSource.Play();
            TakeDamage(collision.collider.GetComponent<AttackDamage>().GetAttackDamage());
        }
    }
}
