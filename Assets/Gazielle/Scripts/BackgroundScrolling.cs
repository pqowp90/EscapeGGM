using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    private float length,startpos,camPos;
    public GameObject cam;
    public float parallaxeffect;
    void Start()
    {
        startpos = transform.position.x;
        camPos = 0f;
        length = 26.258f;
        // if(GetComponent<SpriteRenderer>()!=null)
        //     Debug.Log(GetComponent<SpriteRenderer>().bounds.size.x);
    }

    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1- parallaxeffect));
        float dist = (cam.transform.position.x * parallaxeffect);
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);


        if (temp > camPos + length) {startpos -= length; camPos+=length;}
        else if (temp < camPos - length) {startpos += length;camPos-=length;}

    }
}