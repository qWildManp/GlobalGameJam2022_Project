using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MovingObjects
{
    public ColorBar colorBar;
    public float coolDown;
    public float currentTime;
    public bool canWalk = true;
    protected override void OnCantMove<T>(T component)
    {
        return;
    }
    protected override bool AttemptMove<T>(Vector2 moveDistance, bool changeFace = true) 
    {
        RaycastHit2D hit;

        bool canMove = Move(moveDistance, out hit,changeFace);

        if (hit.transform == null)
        {
            colorBar.UseColor(1);
            return true;
        }
        T hitComponent = hit.transform.GetComponent<T>();
        if (!canMove && hitComponent != null)
        {
            OnCantMove(hitComponent);
        }
        return false;
    }
    // Update is called once per frame
    void Update()
    {


        int horizontal = 0;
        int vertical = 0;
        if (!canWalk)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= coolDown)
            {
                currentTime = 0;
                canWalk = true;
            }
            return;
        }
        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");

        if(horizontal != 0)
        {
            vertical = 0;
        }


        if ((horizontal != 0 || vertical!= 0)){
            canWalk = false;
            AttemptMove<Wall>(new Vector2(horizontal, vertical));
        }
    }


    
}
