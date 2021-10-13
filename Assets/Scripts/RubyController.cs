using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    //movement variables
    Rigidbody2D rigidbody2d;

    float horizontal;
    float vertical;

    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);

    public float speed = 3.0f;

    //health variables
    public int maxHealth = 5;
    public int health { get { return currentHealth; } }
    int currentHealth;

    public float timeInvincible = 2.0f;
    float invincibleTimer;
    bool isInvincible;

    void Start()
    {
            //Framerate Adjustment
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;
            //setting up movement inputs
        rigidbody2d = GetComponent<Rigidbody2D>();
            //setting current health to max health
        currentHealth = maxHealth;

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(0.0f,move.y))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);


        //Debug.Log(vertical);
        //Debug.Log(horizontal);
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        
    }
    void FixedUpdate()
    {
            //Horizontal and Vertical movement through input
        Vector2 position = transform.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }
    public void ChangeHealth(int amount)
    {
        if(amount < 0)
        {
            if(isInvincible)
            {
                return;
            }
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0 , maxHealth);
        animator.SetTrigger("Hit");
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
