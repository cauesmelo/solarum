using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    private TextMeshProUGUI startSelect;
    private TextMeshProUGUI creditsSelect;
    //private TextMeshProUGUI exitSelect;
    private int selected = 0;


    public void HoverStart()
    {
        GameManager.instance.Play("change");
        selected = 0;
    }

    public void HoverCredits()
    {
        GameManager.instance.Play("change");
        selected = 1;
    }

    public void HoverExit()
    {
        GameManager.instance.Play("change");
        selected = 2;
    }

    // Start is called before the first frame update
    void Start()
    {
        startSelect = GameObject.Find("startSelect").GetComponent<TextMeshProUGUI>();
        creditsSelect = GameObject.Find("creditsSelect").GetComponent<TextMeshProUGUI>();
        //exitSelect = GameObject.Find("exitSelect").GetComponent<TextMeshProUGUI>();

        creditsSelect.enabled = false;
        //exitSelect.enabled = false;
    }

    public void PlayGame()
    {
        GameManager.instance.PlayGame();
    }

    public void ShowCredits()
    {
        GameManager.instance.ShowCredits();
    }

    public void ExitGame()
    {
        GameManager.instance.ExitGame();
    }

    // Update is called once per frame
    void Update()
    {
        switch (selected)
        {
            case 0:
                startSelect.enabled = true;
                creditsSelect.enabled = false;
                //exitSelect.enabled = false;
                break;
            case 1:
                startSelect.enabled = false;
                creditsSelect.enabled = true;
                //exitSelect.enabled = false;
                break;
            case 2:
                startSelect.enabled = false;
                creditsSelect.enabled = false;
                //exitSelect.enabled = true;
                break;
        }


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (selected > 0)
            {
                GameManager.instance.Play("change");
                selected--;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (selected < 3)
            {
                GameManager.instance.Play("change");
                selected++;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Return))
        {
            switch (selected)
            {
                case 0:
                    GameManager.instance.PlayGame();
                    break;
                case 1:
                    GameManager.instance.ShowCredits();
                    break;
                case 2:
                    GameManager.instance.ExitGame();
                    break;
            }
        }

    }
}
