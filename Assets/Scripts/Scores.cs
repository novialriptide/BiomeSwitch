using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scores : MonoBehaviour
{
    public float score;
    public ArrayList scores;

    [HideInInspector]
    public static Scores instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        scores = new ArrayList();
        DontDestroyOnLoad(gameObject);
    }
}
