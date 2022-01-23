using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MovingObjects
{
    BoxCollider2D hurtBox;
    AlarmBox alarmBox;
    AttackBox attackBox;
    public GameObject projectilePrefab;
    public float projectiledMonsterAttackStartDistance = 3.0f;
    public float saberMonsterAttackStartDistance = 1.0f;
    public bool isProjectileMonster = false;
    int retrieveCounter = 0;
    public int retrievMaximumStep = 3;
    Vector2 monsterMoveLastDirection;
    //parameter use to spesfify the distance of a single attack.
    public float attackCheckDistance = 1.0f;
    public int hp;
    int fixClosedToWallFactor = 4;
    //speficy how often the state of the monster change
    public float stateCD = 1.0f;
    public bool isClosedToWall = true;
    float stateCDTimer = 0.0f;
    protected bool isImmortal;
    public Color monsterColor = Color.black;

    protected enum MonsterState
    {
        Idle,
        Roaming,
        Attack,
        Skill,
        Chasing,
        Retrieve
    }

    protected MonsterState monsterState;

    private void Awake()
    {
        if (GetComponentInChildren<AlarmBox>() != null)
        {
            alarmBox = GetComponentInChildren<AlarmBox>();
            attackBox = GetComponentInChildren<AttackBox>();
            InitialState();
        }
        else
        {
            Debug.LogError("Monster Parent Get AlarmBox Error£¡");
        }
    }


    void InitialState()
    {
        monsterState = MonsterState.Idle;
    }

    protected void UpdateState(MonsterState monsterState)
    {
        //Debug.Log("Current State: " + monsterState);
        switch (monsterState)
        {
            case MonsterState.Idle:
                Idle();
                break;
            case MonsterState.Attack:
                StartAttack();
                break;
            case MonsterState.Chasing:
                Chasing();
                break;
            case MonsterState.Roaming:
                Roaming();
                break;
            case MonsterState.Skill:
                Skill();
                break;
            case MonsterState.Retrieve:
                Retrieve();
                break;
            default:
                Debug.LogError("Monster state switches error!");
                break;

        }
    }

    void updateChildrenComponentDirection()
    {
        if (faceTo==Vector2.left)
        {
            attackBox.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            alarmBox.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (faceTo==Vector2.right)
        {
            attackBox.transform.localRotation = Quaternion.Euler(0f, 0f, 180f);
            alarmBox.transform.localRotation = Quaternion.Euler(0f, 0f, 180f);
        }
        else if (faceTo==Vector2.up)
        {
            attackBox.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
            alarmBox.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
        }
        else if(faceTo==Vector2.down)
        {
            attackBox.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
            alarmBox.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        }
    }

    protected void Roaming()
    {
        //monster is closed to the wall
        if (!isClosedToWall && fixClosedToWallFactor>0)
        {
           //Debug.Log("Closed to wall!");
           AttemptMove<Wall>(-monsterMoveLastDirection);
           updateChildrenComponentDirection();
           fixClosedToWallFactor--;
        }
        else
        {
            int walkDirection = MakeDice(0, 4);

            switch (walkDirection)
            {
                //positive x direction
                case 0:
                    isClosedToWall = AttemptMove<Wall>(new Vector2(1f, 0f));
                    monsterMoveLastDirection = new Vector2(1f, 0f);
                    updateChildrenComponentDirection();
                    break;
                //negative x direciton
                case 1:
                    isClosedToWall = AttemptMove<Wall>(new Vector2(-1f, 0f));
                    monsterMoveLastDirection = new Vector2(-1f, 0f);
                    updateChildrenComponentDirection();
                    break;
                //positive y direction
                case 2:
                    isClosedToWall = AttemptMove<Wall>(new Vector2(0f, 1f));
                    monsterMoveLastDirection = new Vector2(0f, 1f);
                    updateChildrenComponentDirection();
                    break;
                //negative y direction
                case 3:
                    isClosedToWall = AttemptMove<Wall>(new Vector2(0f, -1f));
                    monsterMoveLastDirection = new Vector2(0f, -1f);
                    updateChildrenComponentDirection();
                    break;
            }
            if (!isClosedToWall)
            {
                fixClosedToWallFactor = 3;
            }
        }
    }




    protected void SaberAttack()
    {
        //do some animation and check hit via event
        AnimationController animController = GetComponent<AnimationController>();
        animController.Attack();
        
    }
    protected void SetRetrive()
    {
        monsterState = MonsterState.Retrieve;
    }
    public void CalculateDamage()
    {
        if (attackBox.playerInRange)
        {
            Debug.Log("Hit" + attackBox.player);
            Vector2 dir = gameObject.GetComponent<Monster>().faceTo;
            attackBox.player.GetComponent<PlayerStatus>().GetHit(-1,dir);
        }
    }

    protected void StartAttack()
    {
        //if(isProjectileMonster){
        //    attackCheckDistance = 3.0f;
        //}
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, faceTo, attackCheckDistance);
        //if (hit.collider.CompareTag("Player"))
        //{
        if (isProjectileMonster)
        {
            Projectile();
        }
        else
        {
            SaberAttack();
        }

        //}

        
    }

    protected void Projectile()
    {
        AnimationController animController = GetComponent<AnimationController>();
        animController.Attack();
        GameObject projectile = Instantiate(projectilePrefab, rb2D.position, Quaternion.identity);
        
        if (faceTo == Vector2.left)
        {
            projectile.transform.localRotation = Quaternion.Euler(0f, 0f, 180f);
        }
        else if (faceTo == Vector2.right)
        {
            projectile.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (faceTo == Vector2.up)
        {
            projectile.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        }
        else if (faceTo == Vector2.down)
        {
            projectile.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);

        }
        EnemyProjectile fireballBehavior = projectile.GetComponent<EnemyProjectile>();
        fireballBehavior.source = gameObject.GetComponent<Monster>();
        fireballBehavior.Launch(faceTo, 300);
        Debug.Log("Throw a Projectile!");
        monsterState = MonsterState.Chasing;
    }

    protected void Skill()
    {
        Debug.Log("Taste my blade!");
    }

    protected void Idle()
    {
        monsterState = MonsterState.Roaming;
    }

    protected void Chasing()
    {
        Vector2 targetDirection = alarmBox.chasedTarget.transform.position - transform.position;
        if (isProjectileMonster)
        {
            //if the monster and the player is in a column but beyond the attack range
            if (targetDirection.x == 0f)
            {
                if (targetDirection.magnitude > projectiledMonsterAttackStartDistance)
                {
                    if (targetDirection.normalized.y>0)
                    {
                        AttemptMove<Wall>(new Vector2(0f, 1f));
                    }
                    else
                    {
                        AttemptMove<Wall>(new Vector2(0f, -1f));
                    }
                    updateChildrenComponentDirection();
                }
                else
                {
                    monsterState = MonsterState.Attack;
                }
                
            }
            //if the monster and the player is in a row but beyond the attack range
            else if (targetDirection.y == 0f)
            {
                if (targetDirection.magnitude > projectiledMonsterAttackStartDistance)
                {
                    if (targetDirection.normalized.x>0)
                    {
                        AttemptMove<Wall>(new Vector2(1f, 0f));
                    }
                    else
                    {
                        AttemptMove<Wall>(new Vector2(-1f, 0f));
                    }
                    updateChildrenComponentDirection();
                }
                else
                {
                    monsterState = MonsterState.Attack;
                }
            }
            //neither the monster and the player is in a column not is in a row
            else
            {
                if (MakeDice(0, 2) == 0)
                {
                    if (targetDirection.normalized.x > 0)
                    {
                        AttemptMove<Wall>(new Vector2(1f, 0f));
                    }
                    else
                    {
                        AttemptMove<Wall>(new Vector2(-1f, 0f));
                    }
                    updateChildrenComponentDirection();
                }
                else
                {
                    if (targetDirection.normalized.y > 0)
                    {
                        AttemptMove<Wall>(new Vector2(0f, 1f));
                    }
                    else
                    {
                        AttemptMove<Wall>(new Vector2(0f, -1f));
                    }
                    updateChildrenComponentDirection();
                }
            }
        }
        else
        {
            
            if (targetDirection.magnitude > saberMonsterAttackStartDistance)
            {
                //Debug.Log("Make Dice: " + MakeDice(0, 2));
                if (MakeDice(0, 2) == 0)
                {
                    if (targetDirection.normalized.x > 0)
                    {
                        AttemptMove<Wall>(new Vector2(1f, 0f));
                    }
                    else
                    {
                        AttemptMove<Wall>(new Vector2(-1f, 0f));
                    }
                    updateChildrenComponentDirection();
                }
                else
                {
                    if (targetDirection.normalized.y > 0)
                    {
                        AttemptMove<Wall>(new Vector2(0f, 1f));
                    }
                    else
                    {
                        AttemptMove<Wall>(new Vector2(0f, -1f));
                    }
                    updateChildrenComponentDirection();
                }
            }
            else
            {
                //TBD, need a method to turn the monster always toward the players
                monsterState = MonsterState.Attack;
            }
        }
    }

    //Only Saber used
    protected void Retrieve()
    {
        Vector2 targetDirection = alarmBox.chasedTarget.transform.position - transform.position;
        if (retrieveCounter <= retrievMaximumStep)
        {
            if (MakeDice(0, 2) == 0)
            {
                if (targetDirection.normalized.x > 0)
                {
                    AttemptMove<Wall>(new Vector2(-1f, 0f), false);
                }
                else
                {
                    AttemptMove<Wall>(new Vector2(1f, 0f), false);
                }
                updateChildrenComponentDirection();
            }
            else
            {
                if (targetDirection.normalized.y > 0)
                {
                    AttemptMove<Wall>(new Vector2(0f, -1f), false);
                }
                else
                {
                    AttemptMove<Wall>(new Vector2(0f, 1f), false);
                }
                updateChildrenComponentDirection();
            }
            retrieveCounter++;
        }
        else
        {
            retrieveCounter = 0;
            monsterState = MonsterState.Chasing;
        }
    }


    public void Die()
    {
        //TBD
        Destroy(gameObject);
  
    }

    private int MakeDice(int lowerBound, int upperBound)
    {
        return Random.Range(lowerBound,upperBound);
    }


    void Update()
    {
        stateCDTimer += Time.deltaTime;
        if (stateCDTimer >= stateCD)
        {
            UpdateState(monsterState);
            stateCDTimer = 0f;
        }


        if (attackBox.canAttack)
        {
            monsterState = MonsterState.Attack;
            attackBox.canAttack = false;
        }

        if (monsterState == MonsterState.Roaming || monsterState == MonsterState.Idle)
        {
            if (alarmBox.ifFoundTarget)
            {
                monsterState = MonsterState.Chasing;
            }
        }
    }

    protected override void OnCantMove<T>(T component)
    {
        return;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
