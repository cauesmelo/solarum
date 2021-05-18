using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapeController : MonoBehaviour
{
    // Animation States
    public bool HasCape { get; set; }
    public bool Running { get; set; }
    public bool IsGrounded { get; set; }
    public bool Jump1 { get; set; }
    public bool Jump2 { get; set; }
    public bool Dropping { get; set; }
    public bool OnAir { get; set; }
    public bool SoftLanding { get; set; }
    public bool HardLanding { get; set; }
    public bool CanDash { get; set; }
    public bool UpDash { get; set; }
    public bool DownDash { get; set; }
    public bool LateralDash { get; set; }
    public bool RechargingDash { get; set; }

    // Controllers
    private Animator animator;
    private SpriteRenderer sprites;

    void Awake()
    {
        animator = GetComponent<Animator>();
        sprites = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (HasCape)
        {
            if (Running)
            {
                animator.SetBool("running", true);

                if (CanDash) animator.SetBool("canDash", true);
                if (OnAir) animator.SetBool("onAir", true);
                if (Jump1) animator.SetBool("jump1", true);
                if (Jump2) animator.SetBool("jump2", true);
                if (LateralDash) animator.SetBool("dashing", true);
                if (RechargingDash) animator.SetBool("recharging", true);
                if (SoftLanding) animator.SetBool("softLanding", true);
                if (HardLanding) animator.SetBool("hardLanding", true);
            } 
            else
            {
                animator.SetBool("running", false);
            }
        }
    }
}
