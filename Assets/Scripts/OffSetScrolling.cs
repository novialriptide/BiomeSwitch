using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffSetScrolling : MonoBehaviour
{
    public float scrollSpeed;

    SpriteRenderer spriteRenderer;
    private Vector2 savedOffset;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float x = Mathf.Repeat(Time.time * scrollSpeed, 1);
        Vector2 offset = new Vector2(x, 0);
        spriteRenderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}