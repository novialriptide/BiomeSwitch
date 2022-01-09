using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainParticleAdjuster : MonoBehaviour
{
    ParticleSystem rainParticles;

    private void Start()
    {
        rainParticles = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        var s = rainParticles.shape;
        s.scale = new Vector3(256, rainParticles.shape.scale.y, rainParticles.shape.scale.z);
    }
}
