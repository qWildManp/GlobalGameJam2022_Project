using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    public bool canAttack = false;
    public bool playerInRange = false;
    public GameObject player;
    public float timer=2.0f;
    float attackCD=2.0f;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {
        timer += attackCD;
        if (other.CompareTag("Player") && timer>=attackCD)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            player = collision.gameObject;
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = null;
            playerInRange = false;
        }
    }
}
