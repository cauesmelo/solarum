using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarAnimator : MonoBehaviour
    {

    private Animator animator;
    private AnimatorOverrideController animatorOverrider;
    private bool dead;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("birth");
    }
    IEnumerator Create()
    {
        if (dead)
        {
            dead = false;
            animator.SetTrigger("birth");

            yield return new WaitForSeconds(Random.Range(2, 20));

            animator.SetTrigger("death");
            dead = true;
        }
    }
}
