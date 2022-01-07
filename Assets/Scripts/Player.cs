using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    CharacterController2D characterController2D;
    Rigidbody2D rigidBody2D;

    bool jump = false;
    float xAxis = 0f;

    private void Start()
    {
        characterController2D = GetComponent<CharacterController2D>();
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (transform.position.y < -10)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    private void FixedUpdate()
    {
        characterController2D.Move(xAxis * Time.deltaTime * speed, false, jump);
        jump = false;
    }
}
