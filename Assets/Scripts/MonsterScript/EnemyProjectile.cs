using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : ProjectileBehavior
{
    // Start is called before the first frame update
    public Monster source;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<PlayerStatus>())
        {
            Debug.Log("Hit player");
            PlayerStatus playerStatus = collider.GetComponent<PlayerStatus>();
            Vector2 dir = source.GetComponent<Monster>().faceTo;
            playerStatus.GetHit(-1,dir);
            Destroy(gameObject);
        }
    }
}
