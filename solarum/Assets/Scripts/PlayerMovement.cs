using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Player Controller
    public bool PlayerHasControl { get; set; }

    // Physics
    private Rigidbody2D p_RigidBody2D = null; // player rigid body
    private Vector3 p_velocity = Vector3.zero; // player velocity
    public float gravityDelay = 0.3f; // player gravity delay
    public float fallThreshold = 6; // minimum speed for character to be falling or rising
    public float verticalSpeed;

    // Physics Method Call Flag
    private bool jump = false;
    private bool lateralDash = false;
    private bool upDash = false;
    private bool downDash = false;
    private bool dropRoll = false;

    // Running Attributes
    public enum Direction { Right, Left }
    public Direction direction = Direction.Right; // direction which the player is going
    public bool running = false; // toggle running state
    private float speed = 35; // speed of the running
    private float currentSpeed;

    // Jumping and Grounding
    public int maxJumps = 2; // maximum jumps player can use
    public int jumpCount = 0; // how many jumps player has already used
    private float jumpForce = 13; // force of jump
    private bool isGrounded = false; // if player is on the ground/plaform
    private float airTime; // controller for landing animation
    private GameObject Platform { get; set; } // what platform player is colliding with

    // Dash attributtes
    public bool canDash;
    private bool readyToDash;
    private float dashTimePassed = 0; // counter for Dash Recharge
    private float dashCooldown = 6f;
    public float upDashForce = 13;
    public float downDashForce = 20;
    public float rightDashForce = 10;
    private bool canDropRoll; // controller for dropping animation

    // Cape Attributes
    public bool hasCape;

    // Animation & Sprites Controller
    private Animator capeAnimator;
    private Animator animator;
    private SpriteRenderer sprites;
    public AnimatorOverrideController animatorOverrider;

    public bool IsDead { get; set; }

    private void Awake()
    {
        // WARNING: ALL TO 'FALSE' ON GAMEBUILD OR ACCORDING TO GAMEMANAGER STATE
        // Setting Initial Attributes
        PlayerHasControl = true;
        running = true;
        hasCape = true;
        canDash = true;
        IsDead = false;
        canDropRoll = true;
        currentSpeed = speed;
        dashTimePassed = dashCooldown;

        // Getting Object References
        p_RigidBody2D = GetComponent<Rigidbody2D>();
        sprites = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        capeAnimator = transform.GetChild(0).GetComponent<Animator>();

        // Setting Animator Controller for Cape
        if (!GameManager.instance.GetCape()) animator.runtimeAnimatorController = animatorOverrider;

    }

    void FixedUpdate()
    {
        // Automatic running
        float movement = speed * Time.fixedDeltaTime;
        Vector3 targetVelocity = Vector2.zero;

        if (running)
        {
            if (speed == 0) ResumeRunning();

            if (direction == Direction.Right)
            {
                targetVelocity = new Vector2(movement * 10f, p_RigidBody2D.velocity.y);
                sprites.flipX = false;
            }

            if (direction == Direction.Left)
            {
                targetVelocity = new Vector2(-(movement * 10f), p_RigidBody2D.velocity.y);
                sprites.flipX = true;
            }
        }
        else
        {
            StopRunning();
        }

        p_RigidBody2D.velocity = Vector3.SmoothDamp(p_RigidBody2D.velocity, targetVelocity, ref p_velocity, 0.05f);
        verticalSpeed = p_RigidBody2D.velocity.y;

        if (canDash)
        {
            // Dash cooldown
            if (dashTimePassed < dashCooldown)
            {
                dashTimePassed += Time.fixedDeltaTime;
                readyToDash = false;

            }
            else
            {
                dashTimePassed = dashCooldown;
                readyToDash = true;
            }
        }

        // Airtime Counter
        if (!isGrounded)
        {
            airTime += Time.fixedDeltaTime;
        }

        // Grounding Check
        if (isGrounded && (verticalSpeed < fallThreshold))
        {
            // Animation Cycle
            animator.SetBool("grounded", true);

            if (airTime > 1.5)
                animator.SetTrigger("hardLanding");
            else if ((airTime > 0.05 && airTime <= 1.5))
                animator.SetTrigger("softLanding");

            // Locic Cycle
            airTime = 0;
        }

        // Controller Physics Execution
        if (running)
        {
            if (jump)
            {
                p_RigidBody2D.velocity = new Vector2(p_RigidBody2D.velocity.x, jumpForce);
                jump = false;
            }
            if (lateralDash)
            {
                StartCoroutine(PauseGravity());
                p_RigidBody2D.velocity = new Vector2(rightDashForce, 0);
                GameManager.instance.SetCape(false);
                lateralDash = false;
            }
            if (downDash)
            {
                StartCoroutine(PauseSpeed(.3f, speed));
                StartCoroutine(PauseGravity());
                p_RigidBody2D.velocity = new Vector2(p_RigidBody2D.velocity.x, -downDashForce);

                downDash = false;
            }
            if (upDash)
            {
                p_RigidBody2D.velocity = new Vector2(p_RigidBody2D.velocity.x, upDashForce);
                upDash = false;
            }
            if (dropRoll)
            {
                p_RigidBody2D.velocity = new Vector2(p_RigidBody2D.velocity.x, -jumpForce * .75f);
                dropRoll = false;
            }
        }
    }

    private void Update()
    {
        // Inputs
        if (PlayerHasControl)
        {
            // Jump movement
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                if (jumpCount < maxJumps)
                    Jump();
            }

            // Drop movement
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                Drop();
            }

            // CHEATER
            //if (Input.GetKeyDown(KeyCode.P))
            //{
            //    GameManager.instance.NextLevel();
            //}

            //if (Input.GetKeyDown(KeyCode.O))
            //{
            //    GameManager.instance.RunTest();
            //}

            // RESET LEVEL
            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetLevel();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.instance.EscapeGame();
            }

            //// Up dash movement
            //if (Input.GetKeyDown(KeyCode.W))
            //{
            //    if (readyToDash && !isGrounded)
            //        UpDash();
            //}

            //// Down dash movement
            //if (Input.GetKeyDown(KeyCode.S))
            //{
            //    if (readyToDash && !isGrounded)
            //        DownDash();
            //}

            // Right dash movement
            if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && GameManager.instance.GetLevel() != 1)
            {
                if (readyToDash && !isGrounded)
                    LateralDash();
            }
        }
    }

    private void Jump()
    {

        // Animation Cycle
        if (jumpCount == 1)
        {
            capeAnimator.SetTrigger("interrupt");
            animator.SetTrigger("jump2");
        }
        else
        {
            capeAnimator.SetTrigger("interrupt");
            animator.SetTrigger("jump1");
        }

        // Audio Cycle
        GameManager.instance.Play("jump");

        // Logic Cycle
        jumpCount++;

        // Physics Cycle
        jump = true;
    }

    private void Drop()
    {
        // should be a raycast until finds "ground", 
        // but since I'm tired and this shit is taking too long
        // let's leave like that pls

        if (canDropRoll)
        {
            animator.SetTrigger("dropping");
            canDropRoll = false;
            StartCoroutine(PauseDrop());
        }

        if (Platform != null)
        {
            Platform.GetComponent<EdgeCollider2D>().enabled = false;
        }

        dropRoll = true;
    }

    private void LateralDash()
    {
        // Animation Cycle
        animator.SetTrigger("lateralDash");
        capeAnimator.SetTrigger("lateralDash");

        // Audio Cycle
        GameManager.instance.Play("dash");

        // Logic Cycle
        dashTimePassed = 0;

        // Physics Cycle
        lateralDash = true;
    }

    private void DownDash()
    {
        // Animation Cycle
        animator.SetTrigger("downDash");
        capeAnimator.SetTrigger("lateralDash");

        // Audio Cycle
        GameManager.instance.Play("dash");

        // Logic Cycle
        dashTimePassed = 0;
        airTime += 1.5f; // deixar aqui ou n�o funciona!

        // Physics Cycle
        downDash = true;
    }
    private void UpDash()
    {
        // Animation Cycle
        animator.SetTrigger("upDash");
        capeAnimator.SetTrigger("lateralDash");

        // Audio Cycle
        GameManager.instance.Play("dash");

        // Logic Cycle
        dashTimePassed = 0;

        // Physics Cycle
        upDash = true;
    }

    IEnumerator PauseGravity()
    {
        p_RigidBody2D.gravityScale = 0;
        yield return new WaitForSeconds(gravityDelay);
        p_RigidBody2D.gravityScale = 2;
    }

    IEnumerator PauseSpeed(float speedDelay, float currentSpeed)
    {
        speed = 0;
        yield return new WaitForSeconds(speedDelay);
        speed = currentSpeed;
    }

    IEnumerator PauseDrop()
    {
        yield return new WaitForSeconds(1.2f);
        canDropRoll = true;
    }

    public void StopRunning()
    {
        if (speed > 0)
        {
            animator.SetTrigger("stopping");
            animator.SetBool("running", false);
            capeAnimator.SetTrigger("interrupt");

            speed = 0;
            running = false;
        }
    }
    public void ResumeRunning()
    {
        if (speed == 0)
        {
            animator.SetBool("running", true);

            speed = currentSpeed;
            running = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (verticalSpeed < fallThreshold)
        {

            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
                jumpCount = 0;
            }

            if (collision.gameObject.CompareTag("Platform"))
            {

                Platform = collision.gameObject;
                isGrounded = true;
                jumpCount = 0;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }

        if (collision.gameObject.CompareTag("Platform"))
        {
            Platform = null;
            isGrounded = false;
        }
    }
    public bool GetReadyToDash()
    {
        return readyToDash;
    }

    public float GetDashCooldown()
    {
        return dashCooldown;
    }

    public void ResetLevel()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject deathSpawnPoint = GameObject.Find("Spawn");

        player.transform.position = deathSpawnPoint.transform.position;
        GameManager.instance.ResetStarScore();
        GameManager.instance.AddDeath();
        GameManager.instance.Play("failure");

    }

}
