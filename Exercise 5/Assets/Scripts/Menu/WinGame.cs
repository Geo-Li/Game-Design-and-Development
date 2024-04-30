using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGame : MonoBehaviour
{
    public void LoadMainScene(int mainSceneIndex) {
        SceneManager.LoadScene(0);
    }
}
