using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarAnimatorController : MonoBehaviour
{
    private bool trigger;
    private Animator[] childrenAnimator;

    // Start is called before the first frame update
    void Start()
    {
        childrenAnimator = GetComponentsInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger)
        {
            StartCoroutine(RandomizeDeathPositions());
        }
    }

    private IEnumerator RandomizeDeathPositions()
    {

        foreach (Animator compChild in childrenAnimator)
        {
            float death = Random.Range(0,9);

            yield return new WaitForSeconds(Random.Range(0, 1));

            if (death > 5)
            {
                compChild.SetTrigger("death");
                compChild.GetComponentInParent<Transform>().position.Set(Random.Range(-855, 855), Random.Range(-490, 490),0);
            }
        }
    }
}
