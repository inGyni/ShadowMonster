using UnityEngine;
using System.Collections;
using SpriteGlow;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Variables

    public HealthManager healthManager;
    public ManaManager manaManager;
    public LevelManager levelManager;
    public CharacterController2D controller;
    public SpriteGlowEffect spriteGlowEffect;
    public Animator animator;
    public int maxHealth;
    public int maxMana;
    public int effectSmoothing;
    public float moveSpeed = 30f;
    private float moveValue;
    private bool jump = false;
    private bool use = false;
    [HideInInspector]
    public bool Invisible = false;
    private bool InvEffOnCooldown = false;
    public float InvEffCooldownTime = 5f;
    public float InvEffTime = 10f;

    #endregion

    #region Unity Hooks

    private void Start()
    {
        healthManager.SetMaxHealth(maxHealth);
        manaManager.SetMaxMana(maxMana);
    }


    private void Update()
    {
        manaManager.isUsing = Invisible;
        moveValue = Input.GetAxisRaw("Horizontal") * moveSpeed;
        animator.SetFloat("Speed", Mathf.Abs(moveValue));

        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("isJumping", true);
            jump = true;
        }

        if (Input.GetButtonDown("Use"))
        {
            use = true;
            animator.SetBool("isUsing", use);
        }
        else
        {
            use = false;
            animator.SetBool("isUsing", use);
        }

        if (Input.GetButtonDown("Ability0"))
        {
            if (!Invisible && !InvEffOnCooldown)
            {
                StartCoroutine(InvisibleEffectOn());
            }
            else if (Invisible)
            {
                StopCoroutine(InvisibleEffectOn());
                StartCoroutine(InvisibleEffectOff());
            }
        }
    }

    private void FixedUpdate()
    {
        controller.Move(moveValue * Time.fixedDeltaTime, jump);
    }

    #endregion

    #region Hooks

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Collectable"))
        {
            if (col.gameObject.name == "RedStar")
            {
                healthManager.HealMax();
                Destroy(col.gameObject);
                levelManager.RedCollected = true;
            }
            else if (col.gameObject.name == "GreenStar")
            {
                healthManager.HealMax();
                Destroy(col.gameObject);
                levelManager.GreenCollected = true;
            }
            else if (col.gameObject.name == "BlueStar")
            {
                healthManager.HealMax();
                Destroy(col.gameObject);
                levelManager.BlueCollected = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.StartsWith("Enemy"))
        {
            if (col.gameObject.CompareTag("Enemy_Slime"))
            {
                healthManager.Damage(20);
            }
            //More logic here
        }

        if(col.gameObject.CompareTag("Obstacle") && levelManager.finishedLevel)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    #endregion

    #region Functions

    public void OnLanding()
    {
        jump = false;
        animator.SetBool("isJumping", false);
    }

    #endregion

    #region Coroutines

    public IEnumerator InvisibleEffectOn()
    {
        SpriteRenderer player = GetComponentInParent<SpriteRenderer>();

        float moveTime = 0;
        Color startBrightness = spriteGlowEffect.GlowColor;
        Color targetBrightness = new Color(0f, 0.5f, 1f, 1f);
        Color startColor = player.color;
        Color targetColor = new Color(1f, 1f, 1f, 0.4f);
        spriteGlowEffect.outlineWidth = 1;
        bool done1 = false, done2 = false;

        while (!done1 && !done2)
        {
            moveTime += effectSmoothing * Time.deltaTime;
            spriteGlowEffect.GlowColor = Color.Lerp(startBrightness, targetBrightness, moveTime);
            player.color = Color.Lerp(startColor, targetColor, moveTime);

            if (spriteGlowEffect.GlowColor == targetBrightness)
                done1 = true;
            if (player.color == targetColor)
                done2 = true;

            yield return null;
        }

        Invisible = true;

        yield return null;
    }

    public IEnumerator InvisibleEffectOff()
    {
        SpriteRenderer player = GetComponentInParent<SpriteRenderer>();

        float moveTime = 0;
        Color startBrightness = spriteGlowEffect.GlowColor;
        Color targetBrightness = new Color(0f, 0.5f, 1f, 0f);
        Color startColor = player.color;
        Color targetColor = new Color(1f, 1f, 1f, 1f);
        bool done1 = false, done2 = false;

        while (!done1 && !done2)
        {
            moveTime += effectSmoothing * Time.deltaTime;
            spriteGlowEffect.GlowColor = Color.Lerp(startBrightness, targetBrightness, moveTime);

            player.color = Color.Lerp(startColor, targetColor, moveTime);

            if (spriteGlowEffect.GlowColor == targetBrightness)
                done1 = true;
            if (player.color == targetColor)
                done2 = true;

            yield return null;
        }

        spriteGlowEffect.outlineWidth = 0;

        Invisible = false;

        yield return null;
    }

    #endregion

}