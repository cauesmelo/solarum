using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    public GameObject escMenu;
    private GameObject player;
    public Score score;
    public bool available = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (available)
            {
                EscMenu();
            }
        }
    }

    public void LoadGame()
    {
        //GameManager.instance.LoadGame();
    }

    public void PauseGame()
    {
        player.GetComponent<Animator>().speed = 0;
        player.GetComponent<PlayerMovement>().StopRunning();
        score.StopCounting();
    }

    public void ResumeGame()
    {
        player.GetComponent<Animator>().speed = 1;
        player.GetComponent<PlayerMovement>().ResumeRunning();
        score.ResumeCounting();
    }

    public void EscMenu()
    {
        if (escMenu.activeSelf)
        {
            escMenu.SetActive(false);
            ResumeGame();
        }
        else
        {
            escMenu.SetActive(true);
            PauseGame();
        }
    }

}
