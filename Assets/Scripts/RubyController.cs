using UnityEngine;

public class RubyController : MonoBehaviour
{
    //movement variables
    Rigidbody2D rigidbody2d;

    AudioSource audioSource;
    public AudioClip cogClip;
    public AudioClip dmgClip;

    public GameObject projectilePrefab;

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
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

            //Animator 
        Vector2 move = new Vector2(horizontal, vertical);

        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(0.0f,move.y))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
       
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

            //Launch Projectile
        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
        //Debug.Log(vertical);
        //Debug.Log(horizontal);
            if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
            //Raycasting
        if(Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
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
        PlaySound(dmgClip);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);
        PlaySound(cogClip);

        animator.SetTrigger("Launch");
    }
    
       

   
       
}
