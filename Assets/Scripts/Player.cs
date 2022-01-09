using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Hydration")]
    public bool waterCollection = false;
    public float moveHydrationPenalty = 0.05f;
    public float hydration = 100f;
    public float hydrationCollectionValue = 13f;

    [Header("Sunburn")]
    public bool sunburnEnabled = false;
    public float sunburn = 0f;
    public float sunburnIncRate = 3f;
    public float sunburnDecRate = 1f;
    public float sunburnHydrationPenalty = 15f;

    [Header("Sliders")]
    public Slider hydrationBar;
    public Slider sunburnBar;

    [Header("Misc")]
    public float speed;
    public float resetTime = 3.0f;

    [HideInInspector]
    public float timeRemainingTilReset;
    [HideInInspector]
    public bool resetTimer = false;

    static float score;
    bool jump = false;
    float xAxis = 0f;

    AudioManager audioManager;
    BiomeManager biomeManager;
    CharacterController2D characterController2D;
    Rigidbody2D rigidBody2D;
    SpriteRenderer spriteRenderer;
    Scores scores;
    SideScroller sideScroller;
    Animator animator;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        biomeManager = FindObjectOfType<BiomeManager>().GetComponent<BiomeManager>();
        characterController2D = GetComponent<CharacterController2D>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        scores = FindObjectOfType<Scores>().GetComponent<Scores>();
        sideScroller = Camera.main.gameObject.GetComponent<SideScroller>();
        animator = GetComponentInChildren<Animator>();
        timeRemainingTilReset = resetTime;
    }

    private void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            if (characterController2D.m_Grounded)
                audioManager.Play("player_jump");
            jump = true;
        }
        animator.SetBool("player_in_air", !characterController2D.m_Grounded);

        // Update Sliders
        hydrationBar.normalizedValue = hydration / 100;
        sunburnBar.normalizedValue = sunburn / 100;

        // Sunburn Management
        if (biomeManager.biome != 1)
        {
            sunburnEnabled = false;
            sunburn = Mathf.MoveTowards(sunburn, 0, Time.deltaTime * sunburnDecRate);
        }

        if (biomeManager.biome == 1)
            sunburnEnabled = true;

        if (waterCollection && !characterController2D.isUnderGround())
        {
            hydration += Time.deltaTime * hydrationCollectionValue;
        }

        if (sunburnEnabled)
        {
            if (sunburn >= 100f)
            {
                sunburn = 100f;
                hydration -= Time.deltaTime * sunburnHydrationPenalty;
            }

            if (!characterController2D.isUnderGround())
                sunburn = Mathf.MoveTowards(sunburn, 100, Time.deltaTime * sunburnIncRate);

            if (characterController2D.isUnderGround())
                sunburn = Mathf.MoveTowards(sunburn, 0, Time.deltaTime * sunburnDecRate);
        }

        // Reset
        if (!spriteRenderer.isVisible)
            resetTimer = true;

        if (spriteRenderer.isVisible && resetTimer)
        {
            resetTimer = false;
            timeRemainingTilReset = resetTime;
        }

        if (resetTimer)
            timeRemainingTilReset -= Time.deltaTime;

        if (transform.position.y < -10 || timeRemainingTilReset <= 0 || hydration <= 0)
            Reset();

        if (transform.position.x > scores.score && spriteRenderer.isVisible)
            scores.score = transform.position.x;

        biomeManager.biomeChangeTime = biomeManager.biomeStartChangeTime + (scores.score / 1000);
        sideScroller.xAxisSpeed = sideScroller.xAxisStartSpeed + (scores.score / 10000);
    }

    private void FixedUpdate()
    {
        animator.SetFloat("player_speed", Mathf.Abs(xAxis));
        characterController2D.Move(xAxis * Time.deltaTime * speed, false, jump);
        if (xAxis != 0)
            hydration -= Time.deltaTime * moveHydrationPenalty;

        jump = false;
    }

    private void Reset()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        scores.scores.Add(scores.score);
        scores.score = 0;
    }
}
