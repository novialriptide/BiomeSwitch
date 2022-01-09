using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scores : MonoBehaviour
{
    public float score;
    public ArrayList scores;

    private void Awake()
    {
        scores = new ArrayList();
        DontDestroyOnLoad(gameObject);
    }
}
