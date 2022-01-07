using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    public float hydration = 100;
    CharacterController2D characterController2D;
    Rigidbody2D rigidBody2D;
    SpriteRenderer spriteRenderer;

    public float resetTime = 3.0f;
    public float timeRemainingTilReset;
    public bool resetTimer = false;

    bool jump = false;
    float xAxis = 0f;

    private void Start()
    {
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

        if (!spriteRenderer.isVisible)
            resetTimer = true;

        if (spriteRenderer.isVisible && resetTimer)
        {
            resetTimer = false;
            timeRemainingTilReset = resetTime;
        }

        if (resetTimer)
            timeRemainingTilReset -= Time.deltaTime;

        if (transform.position.y < -10 || timeRemainingTilReset <= 0)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    private void FixedUpdate()
    {
        characterController2D.Move(xAxis * Time.deltaTime * speed, false, jump);
        jump = false;
        // characterController2D.isUnderGround();
    }
}
