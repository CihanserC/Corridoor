using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour
{
    // ADD [ReadOnly] to IsAlive parameter.
    public bool IsAlive;

    private void Awake()
    {
        if (transform.GetComponent<MeshRenderer>().enabled)
        {
            IsAlive = true;
        }
        else
        {
            IsAlive = false;
        }
    }

    
}
