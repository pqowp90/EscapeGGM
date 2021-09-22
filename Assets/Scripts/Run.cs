using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : MonoBehaviour
{
    void Start()
    {
        Debug.Log(GetComponent<SpriteRenderer>().bounds.size.x);
    }

    void Update()
    {
        
    }
}
