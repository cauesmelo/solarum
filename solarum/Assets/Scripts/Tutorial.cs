using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private Tutorial Instance { get; set; }

    // Score
    public Score score;
    public Options options;

    // Tutorial UI
    public GameObject background;
    public GameObject instruction0;
    public GameObject instruction1;
    public GameObject instruction2;
    public GameObject instruction3;

    private GameObject instruction;

    public int instructionCount = 0;

    private float delay = 6;
    public float timePassed = 0;

    private PlayerMovement player;

    private bool repeat = true;
    public GameObject extraPlatform;

    private void Awake()
    {
        Instance = this;
        score.gameObject.SetActive(false);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        player.StopRunning();
        player.hasCape = false;
        player.canDash = false;
        player.maxJumps = 1;
        extraPlatform.SetActive(false);
        HideAllInstructions();
        ShowBackground();
    }

    void Update()
    {
        if (instructionCount == 0 && background.GetComponent<Image>().color.a == 1) { Instruction0(); }
        if (instructionCount == 1) { Instruction1(); }
        if (instructionCount == 2) { Instruction2(); }
        if (instructionCount == 3) { Instruction3(); }
    }

    private void ShowBackground() { LeanTween.alpha(background.GetComponent<Image>().rectTransform, 1f, 1f); }

    private void HideBackground() { LeanTween.alpha(background.GetComponent<Image>().rectTransform, 0f, 1f); }

    private void ShowInstruction() { instruction.SetActive(true); }

    private void HideInstruction() { instruction.SetActive(false); }

    private void Instruction0()
    {
        instruction = instruction0;
        ShowInstruction();

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            instructionCount++;
            player.maxJumps = 2;
        }
    }

    private void Instruction1()
    {
        HideInstruction();
        instruction = instruction1;
        ShowInstruction();

        if (player.jumpCount > 1)
        {
            instructionCount++;
        }
    }

    private void Instruction2()
    {
        HideInstruction();
        instruction = instruction2;
        ShowInstruction();

        score.gameObject.SetActive(true);
        score.isCounting = true;

        timePassed += Time.fixedDeltaTime;

        if (timePassed >= delay && repeat)
        {
            player.ResumeRunning();
            repeat = false;
        }

        if (player.IsDead)
        {
            instructionCount++;
            timePassed = 0;
        }
    }

    private void Instruction3()
    {
        HideInstruction();
        instruction = instruction3;
        ShowInstruction();

        options.PauseGame();

        timePassed += Time.fixedDeltaTime;

        if (timePassed >= delay)
        {
            HideInstruction();
            HideBackground();
            options.ResumeGame();
            instructionCount++;
            options.available = true;
            extraPlatform.SetActive(true);
        }
    }

    private void HideAllInstructions()
    {
        instruction0.SetActive(false);
        instruction1.SetActive(false);
        instruction2.SetActive(false);
        instruction3.SetActive(false);
    }

}
