using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scores : MonoBehaviour
{
    public float score;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
