using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int starCount = 0;
    private int score = 0;
    private int totalStarCount = 0;
    private int totalScore = 0;
    private int[] starsRequired = new int[10] { 3, 4, 3, 6, 8, 10, 7, 9, 20, 0};
    private int level = 0;
    private int deathCount = 0;
    private bool hasCape = false;
    public bool debug = false;

    // Gambiarra variable
    private bool notEnoughStar = false;

    AudioSource soundtrack;

    // Game sound volume :))))))))))
    private float volume = 0.3f;
    private float soundtrackVolume = 0.3f;

    private void Awake()
    {
        instance = this;
        soundtrack = GameObject.FindGameObjectWithTag("soundtrack").GetComponent<AudioSource>();
        SceneManager.LoadSceneAsync((int)ScenesIndexes.MENU, LoadSceneMode.Additive);
    }

    // Load first level scene
    public void PlayGame()
    {
        soundtrack.volume = soundtrackVolume;
        soundtrack.Play();
        GameManager.instance.Play("select");
        // Scene change
        SceneManager.UnloadSceneAsync((int)ScenesIndexes.MENU);
        //if (debug)
        //{
        //    hasCape = true;
        //    SceneManager.LoadSceneAsync(13, LoadSceneMode.Additive);
        //}
        //else
        //{
            level++;
            SceneManager.LoadSceneAsync(level + 3, LoadSceneMode.Additive);
        //}
    }

    public void RunTest()
    {
        hasCape = true;
        SceneManager.UnloadSceneAsync(level + 3);
        level = 10;
        SceneManager.LoadSceneAsync(level + 3, LoadSceneMode.Additive);
    }

    public void BackToMenu()
    {
        SceneManager.UnloadSceneAsync((int)ScenesIndexes.CREDITS);
        SceneManager.LoadSceneAsync((int)ScenesIndexes.MENU, LoadSceneMode.Additive);
    }

    // Exit game - desktop builds only
    public void ExitGame()
    {
        GameManager.instance.Play("select");
        Application.Quit();
    }

    public void ShowCredits()
    {
        GameManager.instance.Play("select");
        // Scene change
        SceneManager.UnloadSceneAsync((int)ScenesIndexes.MENU);
        SceneManager.LoadSceneAsync((int)ScenesIndexes.CREDITS, LoadSceneMode.Additive);
    }

    public void Play(string sound)
    {
        SoundManager.Play(sound);
    }

    public void AddStar()
    {
        starCount++;
    }

    public int GetStar()
    {
        return starCount;
    }

    public void AddScore(int newScore)
    {
        score += newScore;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetTotalScore()
    {
        return totalScore;
    }

    public int GetTotalStar()
    {
        return totalStarCount;
    }

    public int GetLevel()
    {
        return level;
    }

    public void NextLevel()
    {
        totalStarCount += starCount;
        starCount = 0;
        totalScore += score;
        score = 0;
        SceneManager.UnloadSceneAsync(level + 3);
        level++;
        SceneManager.LoadSceneAsync(level + 3, LoadSceneMode.Additive);
        if (level != 1)
        {
            hasCape = true;
        }
    }

    public void ResetStarScore()
    {
        GameObject[] stars;
        stars = GameObject.FindGameObjectsWithTag("Star");

        foreach (GameObject star in stars)
        {
            star.gameObject.transform.position = new Vector3(star.gameObject.transform.position.x, star.gameObject.transform.position.y, 0);
            star.GetComponent<Animator>().SetTrigger("respawn");
        }

        score = 0;
        starCount = 0;
    }

    public int GetStarsRequired()
    {
        if (level == 0)
            return 5;
        return starsRequired[level - 1];
    }

    public float GetVolume()
    {
        return volume;
    }

    public float GetSoundtrackVolume()
    {
        return soundtrackVolume;
    }

    public int GetDeathCount()
    {
        return deathCount;
    }

    public void AddDeath()
    {
        deathCount++;
    }

    public void SetFailedStar(bool value)
    {
        notEnoughStar = value;
    }

    public bool GetFailedStar()
    {
        return notEnoughStar;
    }

    public bool GetCape()
    {
        return hasCape;
    }

    public void SetCape(bool value)
    {
        hasCape = value;
    }

    public void EscapeGame()
    {
        SceneManager.UnloadSceneAsync(level + 3);
        SceneManager.LoadSceneAsync((int)ScenesIndexes.MENU, LoadSceneMode.Additive);
        level--;
        ResetStarScore();
        soundtrack.Stop();
    }


    // TODO IMPLEMENT PAUSE
    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }


}