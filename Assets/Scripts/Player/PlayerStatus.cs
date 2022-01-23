using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public ColorBar colorBar;
    public PlayerMovement playerMovement;
    public GameObject warriorImg;
    public GameObject shooterImg;
    public GameObject fireBallPrefab;
    public GameObject iceBallPrefab;
    public AnimationController activeAnimationController;
    public float shield;
    public float health;
    public float maxHealth;
    public bool onShield;
    public bool canAttack;
    public float pressTime;
    public float currentTime;
    public float attackCoolDown;
    public float attackCoolDownCount;
    // Start is called before the first frame update
    void Start()
    {
         pressTime = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckHealth() == 0)
        {
            activeAnimationController.Die();
        }

        Color currentColor = colorBar.currentColor;
        ChangePlayerImage(currentColor);
        onShield = false;
        if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
            return;
        if (canAttack)
        {
            if (Input.GetMouseButtonDown(0))
            {
                canAttack = false;
                Attack(colorBar.currentColor);
            }
        }
        else
         {
           attackCoolDownCount += Time.deltaTime;
            if (attackCoolDownCount >= attackCoolDown)
            {
                attackCoolDownCount = 0;
                canAttack = true;
            }          
        }

        if (Input.GetMouseButton(1))
        {
            if(currentColor == Color.black && shield == 0)
            {
                activeAnimationController.QuitDefence();
            }
            else
            {
                ActiveSkill(colorBar.currentColor);
            }
        }
        else
        {
            if(currentColor == Color.black)
            {
                activeAnimationController.QuitDefence();
            }
            currentTime = 0;
        }

    }

    public float CheckHealth()
    {
        return health;
    }

    public void GetHit(float num,Vector2 sourceDir)
    {
        Debug.Log("monster" + sourceDir);
        Debug.Log("player" + playerMovement.faceTo);
        if(playerMovement.faceTo == -1 * sourceDir)
        {
            Debug.Log("defence success");
            if (onShield && shield > 0)
            {
                Debug.Log("Have Shield");
                shield -= 1;
                return;
            }
            
        }
            Debug.Log("Loose health");
            if (activeAnimationController != null)
            {
                activeAnimationController.Hurt();
            }
            ChangeHealth(-1);
        
    }
    public void ChangeHealth(float num)
    { 
        health += num;
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
        else if (health == 0)
        {
            activeAnimationController.Die();
        }
    }



    void ChangePlayerImage(Color currentColor)
    {
        if (currentColor == Color.black)
        {
            if (colorBar.hasChangeColor)
            {
                shield = 3;
                colorBar.hasChangeColor = false;
            }
            warriorImg.SetActive(true);
            shooterImg.SetActive(false);
            activeAnimationController = warriorImg.GetComponent<AnimationController>();
        }
        else if(currentColor == Color.white)
        {
            shield = 0;
            warriorImg.SetActive(false);
            shooterImg.SetActive(true);
            activeAnimationController = shooterImg.GetComponent<AnimationController>();
        }
    }



    void Attack(Color profession)
    {
        if (profession == Color.black)
        {
            MeleeAttack();

        }
        else if (profession == Color.white)
        {
            GenerateFireBall();
        }
    }

    void ActiveSkill(Color profession)
    {
        if (profession == Color.black)
        {
            activeAnimationController.Defense();
            onShield = true;
        }
        
        else if (profession == Color.white)
        {
            currentTime += Time.deltaTime;
            if(currentTime >= pressTime)
            {
                currentTime = 0;
                GenerateIceBall();
            }
        }
    }
    void MeleeAttack()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerMovement.faceTo, 1, playerMovement.blockingLayer); if (hit)
        {
            Debug.Log("Collider" +  hit.collider.name);
            if (hit.collider.GetComponent<Monster>())
            {
                hit.collider.GetComponent<Monster>().Die();
            }
            
        }
        warriorImg.GetComponent<AnimationController>().Attack();
        Debug.Log("Attack");
    }
    void GenerateFireBall()
    {
        GameObject fireBall = Instantiate(fireBallPrefab,playerMovement.rb2D.position,Quaternion.identity);
        if(playerMovement.faceTo == Vector2.left)
        {
            fireBall.transform.localRotation = Quaternion.Euler(0f, 0f, 180f);
        }
        else if(playerMovement.faceTo == Vector2.right)
        {
            fireBall.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (playerMovement.faceTo == Vector2.up)
        {
            fireBall.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        }
        else if (playerMovement.faceTo == Vector2.down)
        {
            fireBall.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);

        }
        FireBall fireballBehavior = fireBall.GetComponent<FireBall>();
        fireballBehavior.Launch(playerMovement.faceTo, 300);

        

        Debug.Log("FireBall");
    }
    // Low priority
    void GenerateIceBall()
    {
        GameObject iceBall = Instantiate(iceBallPrefab, playerMovement.rb2D.position, Quaternion.identity);
        if (playerMovement.faceTo == Vector2.left)
        {
            iceBall.transform.localRotation = Quaternion.Euler(0f, 0f, 180f);
        }
        else if (playerMovement.faceTo == Vector2.right)
        {
            iceBall.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (playerMovement.faceTo == Vector2.up)
        {
            iceBall.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        }
        else if (playerMovement.faceTo == Vector2.down)
        {
            iceBall.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);

        }
        IceBall fireballBehavior = iceBall.GetComponent<IceBall>();
        fireballBehavior.Launch(playerMovement.faceTo, 300);
        colorBar.IceBallEffect();
        Debug.Log("IceBall");
    }
}
