using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectStar : MonoBehaviour
{
    private int starCount;
    // Start is called before the first frame update
    void Start()
    {
        this.starCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Star"))
        {
            starCount++;
            Destroy(trigger.gameObject);
        }
    }

    public int getStarCount()
    {
        return this.starCount;
    }
}
