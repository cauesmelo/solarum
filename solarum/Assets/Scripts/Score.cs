using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public GameObject scoreUI;
    public bool isCounting = false;

    void Start()
    {
        InvokeRepeating("CountScore", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (isCounting)
        {
            scoreUI.SetActive(true);

            scoreText.text = score.ToString();
        }

    }

    private void CountScore()
    {
        score += 10;
    }

    public void ResetScore()
    {
        score = 0;
    }

    public void ResumeCounting()
    {
        InvokeRepeating("CountScore", 1f, 1f);
    }

    public void StopCounting()
    {
        CancelInvoke();
    }

}
