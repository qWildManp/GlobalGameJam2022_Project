using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmBox : MonoBehaviour
{

    public bool ifFoundTarget = false;
    public GameObject chasedTarget;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            chasedTarget = other.gameObject;
            ifFoundTarget = true;
        }
    }

}
