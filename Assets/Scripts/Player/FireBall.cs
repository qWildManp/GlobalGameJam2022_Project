using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : ProjectileBehavior
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Monster>())
        {
            Debug.Log("Hit the monster");
            if(other.GetComponent<Monster>().monsterColor == Color.white)
            {
                other.GetComponent<Monster>().Die();
            }
            Destroy(gameObject);
        }
        else if (other.GetComponent<Wall>())
        {
            Debug.Log("Hit wall");
            Destroy(gameObject);
        }
    }
}
