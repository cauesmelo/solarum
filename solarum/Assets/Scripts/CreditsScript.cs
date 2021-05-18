using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour
{
    public void Back()
    {
        GameManager.instance.Play("select");
        GameManager.instance.BackToMenu();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Return))
        {
            Back();
        }
    }

    public void BackHover()
    {
        GameManager.instance.Play("change");
    }
}
