using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObjects : MonoBehaviour
{
    public LayerMask blockingLayer;
    public float moveTime = 0.1f;
    public Vector2 faceTo;
    //public Vector2 moveDistance;
    public BoxCollider2D boxCollider2D;
    public Rigidbody2D rb2D;
    float inverseMoveTime;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
        boxCollider2D = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / inverseMoveTime;
    }
    protected virtual bool AttemptMove<T>(Vector2 moveDistance,bool changeFace = true)
        where T : Component
    {
        RaycastHit2D hit;

        bool canMove = Move(moveDistance, out hit, changeFace);

        if (hit.transform == null)
        {
            return true;
        }
        T hitComponent = hit.transform.GetComponent<T>();
        if (!canMove && hitComponent != null)
        {
            OnCantMove(hitComponent);
        }
        return false;
    }

    protected bool Move(Vector2 moveDistance, out RaycastHit2D hit,bool changeFace)
    {
        Vector2 start = transform.position;
        Vector2 end = start + moveDistance;

        boxCollider2D.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        boxCollider2D.enabled = true;

        if(hit.transform == null)
        {
            StartCoroutine(SmoothMovement(end));
            if (changeFace)
            {
                faceTo = moveDistance;
            }

            return true;
        }
        return false;
    }
    protected IEnumerator SmoothMovement(Vector3 end)
    {

        float sqrRemainDistance = (transform.position - end).sqrMagnitude;

        while (sqrRemainDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
            rb2D.MovePosition(newPosition);
            sqrRemainDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }
        GameController.instance.playerTurn = true;
    }
    // Update is called once per frame
    protected abstract void OnCantMove<T>(T component)
    where T : Component;
    
}
