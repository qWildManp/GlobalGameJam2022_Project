using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public float flyingTime;
    public float currentTime;
    public Vector2 end;
    public BoxCollider2D boxCollider2D;
    public Rigidbody2D rb2D;

    void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime > flyingTime)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 dir, float force)
    {
        rb2D.AddForce(dir * force);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Monster>())
        {
            Debug.Log("Hit the monster");
            Destroy(gameObject);
        }
        else if (other.GetComponent<Wall>())
        {
            Debug.Log("Hit wall");
            Destroy(gameObject);
        }
    }
}
