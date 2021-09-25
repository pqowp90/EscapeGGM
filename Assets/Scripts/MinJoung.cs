using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinJoung : MonoBehaviour
{
    [SerializeField]
    private RectTransform button;
    private float buttonHeight;
    private RectTransform myRectTransform;
    void Start()
    {
        myRectTransform = GetComponent<RectTransform>();
        buttonHeight = button.rect.height;
        myRectTransform.sizeDelta = new Vector2(0f,myRectTransform.rect.height - buttonHeight);
        myRectTransform.localPosition += new Vector3(0f,buttonHeight,0f);
    }

    void Update()
    {
        
    }
}
