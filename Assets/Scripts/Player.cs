using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Hydration")]
    public float moveHydrationPenalty = 0.05f;
    public float hydration = 100f;

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

    bool jump = false;
    float xAxis = 0f;

    BiomeManager biomeManager;
    CharacterController2D characterController2D;
    Rigidbody2D rigidBody2D;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        biomeManager = FindObjectOfType<BiomeManager>().GetComponent<BiomeManager>();
        characterController2D = GetComponent<CharacterController2D>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        timeRemainingTilReset = resetTime;
    }

    private void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

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
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        // Debug.Log(transform.position.x);
    }

    private void FixedUpdate()
    {
        characterController2D.Move(xAxis * Time.deltaTime * speed, false, jump);
        if (xAxis != 0)
            hydration -= Time.deltaTime * moveHydrationPenalty;

        jump = false;
    }
}
