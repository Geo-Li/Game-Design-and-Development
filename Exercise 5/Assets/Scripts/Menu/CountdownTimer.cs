using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    private float currTime;
    [SerializeField]
    private float startingTime = 60;

    [SerializeField]
    private TextMeshProUGUI countdownText;

    // Start is called before the first frame update
    void Start()
    {
        currTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (currTime > 0) {
            currTime -= 1 * Time.deltaTime;
        } else {
            SceneManager.LoadScene(2);
        }
        countdownText.text = "Time Remaining: " + currTime.ToString("0");
    }
}
