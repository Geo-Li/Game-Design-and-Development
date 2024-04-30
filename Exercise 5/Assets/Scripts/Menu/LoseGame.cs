using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseGame : MonoBehaviour
{
    public void LoadMainScene(int mainSceneIndex) {
        SceneManager.LoadScene(0);
    }
}
