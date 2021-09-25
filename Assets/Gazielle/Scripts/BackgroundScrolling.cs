
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    private float length,startpos;
    public float parallaxeffect;
    void Start()
    {
        startpos = transform.position.x;
        length = 60f;

    }

    void FixedUpdate()
    {

        if(GameManager.Instance.isRun){
            transform.position-=new Vector3(parallaxeffect*GameManager.Instance.backgroundSpeed, 0f, 0f);
            if (transform.localPosition.x <=  -length) {transform.position = new Vector3(startpos, transform.position.y, transform.position.z);}
        }
    }
}