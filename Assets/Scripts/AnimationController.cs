using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator warriorAnimator;
    public Animator shootAnimator;
    public MovingObjects movingObject;
    public Vector2 faceTo;
    public bool isMonster;
    // Start is called before the first frame update
    void Start()
    {
        if (movingObject.GetComponent<Monster>())
        {
            isMonster = true;
        }
        else
        {
            isMonster = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
    }

    public void Walk()
    {
        faceTo = movingObject.faceTo;
        if (warriorAnimator)
        {
            warriorAnimator.SetFloat("x", faceTo.x);
            warriorAnimator.SetFloat("y", faceTo.y);
        }
        if (shootAnimator)
        {
            shootAnimator.SetFloat("x", faceTo.x);
            shootAnimator.SetFloat("y", faceTo.y);
        }
    }
    public void Attack()
    {
        if (warriorAnimator)
        {
            warriorAnimator.SetTrigger("attack");
        }
        if (shootAnimator)
        {
            shootAnimator.SetTrigger("attack");
        }
    }
    public void Hurt()
    {
        if (warriorAnimator)
        {
            warriorAnimator.SetTrigger("hurt");
        }
        if (shootAnimator)
        {
            shootAnimator.SetTrigger("hurt");
        }
    }
    public void Die()
    {
        if (warriorAnimator)
        {
            warriorAnimator.SetTrigger("die");
        }
        if (shootAnimator)
        {
            shootAnimator.SetTrigger("die");
        }
    }

    public void Defense()
    {
        if (warriorAnimator)
        {
            warriorAnimator.SetTrigger("defense");
        }
    }
    public void QuitDefence()
    {
        if (warriorAnimator)
        {
            warriorAnimator.SetTrigger("quitDefense");
        }
    }

      public void PlayerDie()
        {
         movingObject.gameObject.GetComponent<PlayerMovement>().enabled = false;
        
        }
}
