using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeManager : MonoBehaviour
{
    public GameObject player;

    public void setArcticTundra()
    {
        Debug.Log("Arctic Tundra");
        player.GetComponent<CharacterController2D>().slipperyFeet = true;
    }

    public void setBeach()
    {
        Debug.Log("Beach");
        player.GetComponent<CharacterController2D>().slipperyFeet = false;
    }
}
