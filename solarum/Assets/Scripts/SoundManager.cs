using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static AudioClip select, change, star, track, jump, dash,
        death, failureLevel, successLevel;
    static AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        select = Resources.Load<AudioClip>("Audio/select");
        change = Resources.Load<AudioClip>("Audio/change");
        star = Resources.Load<AudioClip>("Audio/star");
        track = Resources.Load<AudioClip>("Audio/soundtrack2");
        jump = Resources.Load<AudioClip>("Audio/Jump");
        dash = Resources.Load<AudioClip>("Audio/Dash");
        death = Resources.Load<AudioClip>("Audio/death");
        failureLevel = Resources.Load<AudioClip>("Audio/failureLevel");
        successLevel = Resources.Load<AudioClip>("Audio/successLevel");

        audioSource = GameObject.Find("SoundManager").GetComponent<AudioSource>();
    }

    void Awake()
    {
        
    }

    public static void Play(string clip)
    {
        switch (clip)
        {
            case "change":
                audioSource.PlayOneShot(change, GameManager.instance.GetVolume());
                break;
            case "select":
                audioSource.PlayOneShot(select, GameManager.instance.GetVolume());
                break;
            case "star":
                audioSource.PlayOneShot(star, GameManager.instance.GetVolume());
                break;
            case "soundtrack":
                audioSource.PlayOneShot(track, GameManager.instance.GetSoundtrackVolume());
                break;
            case "jump":
                audioSource.PlayOneShot(jump, GameManager.instance.GetVolume());
                break;
            case "dash":
                audioSource.PlayOneShot(dash, GameManager.instance.GetVolume());
                break;
            case "failure":
                audioSource.PlayOneShot(failureLevel, GameManager.instance.GetVolume());
                break;
            case "success":
                audioSource.PlayOneShot(successLevel, GameManager.instance.GetVolume());
                break;
            case "death":
                audioSource.PlayOneShot(death, GameManager.instance.GetVolume());
                break;
        }
    }
}
