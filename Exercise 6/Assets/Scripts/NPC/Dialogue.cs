using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogue;
    
    private void OnTriggerEnter2D(Collider2D collision) {
        dialogue.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        dialogue.SetActive(false);
    }
}
