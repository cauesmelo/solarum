using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private float time = 0;
    private float waitTime = 0.5f;

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (GetComponent<EdgeCollider2D>().enabled == false)
        {
            time += Time.fixedDeltaTime;
            if (time >= waitTime)
            {
                GetComponent<EdgeCollider2D>().enabled = true;
                time = 0;
            }
        }
    }

}
