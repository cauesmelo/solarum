using System.Collections;
using UnityEngine;

public class StarCollector : MonoBehaviour
{
    public AnimatorOverrideController overrider;
    private Animator animator;
    private Collider2D starCollider;
    //private bool collected;

    private void Start()
    {
        //collected = false;
        float aleatorio = Random.Range(0, 10);
        
        animator = GetComponent<Animator>();

        if (aleatorio > 5)
            animator.runtimeAnimatorController = overrider;

        animator.Play("star");
    }

    private void FixedUpdate()
    {
        //if (collected)
        //{
        //    this.gameObject.transform.position = new Vector3(starCollider.gameObject.transform.position.x, starCollider.gameObject.transform.position.y, -999);
        //}
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision is CircleCollider2D && !collected)
        if (collision is CircleCollider2D)
        {
            animator.SetTrigger("collected");
            SoundManager.Play("star");
            GameManager.instance.AddStar();
            GameManager.instance.AddScore(200);

            starCollider = collision;
            
            StartCoroutine(DespawnStar());
        }
    }

    IEnumerator DespawnStar()
    {
        yield return new WaitForSeconds(1f);
        //collected = true;
    }

}
