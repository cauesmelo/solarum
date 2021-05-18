using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CadentStar : MonoBehaviour
{

    private Animator animator;
    private bool big;
    private bool shootable;
    //GameObject[] cadent;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        big = false;
        shootable = true;
        animator.Play("dead");
        //cadent = GameObject.FindGameObjectsWithTag("Cadent");

        //FadeCadent();
    }

    // Update is called once per frame
    void Update()
    {
        if (shootable)
        {
            shootable = false;
            StartCoroutine(SpawnStar());
        }
    }

    IEnumerator SpawnStar()
    {

        yield return new WaitForSeconds(Random.Range(1,6));

        if (big)
        {
            animator.SetTrigger("shootBig");
            big = false;
        }
        else
        {
            animator.SetTrigger("shootSmall");
            big = true;
        }

        shootable = true;
    }

    //IEnumerator FadeCadent()
    //{
    //    for (float i = 0; i <= 0; i += Time.deltaTime)
    //    {
    //        foreach(GameObject star in cadent)
    //        {
    //            SpriteRenderer renderer = star.gameObject.GetComponent<Renderer>() as SpriteRenderer;
    //            renderer.color = new Color(1, 1, 1, i);
    //            yield return null;
    //        }
            
    //    }

    //}
}
