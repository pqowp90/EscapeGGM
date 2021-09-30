using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollManager : MonoBehaviour
{
    [SerializeField]
    private int shownUi;
    [SerializeField]
    private RectTransform content;
    [SerializeField]
    private RectTransform realContent;
    [SerializeField]
    private RectTransform ui;
    private float uiHeight=0f,uiWidth=0f;
    [SerializeField]
    private RectTransform[] uisRectTransform;
    private bool on=false;
    private int aaa=-10;
    void Start()
    {
        uiHeight = ui.rect.height;
        uiWidth = ui.rect.width;
            
        //content. = new Vector3(0f,(content.childCount*uiHeight),0f);
    }
    public void GetUi(){
        for(int i=0;i<content.childCount-1;i++){
            uisRectTransform[i] = content.GetChild(i+1).GetComponent<RectTransform>();
            uisRectTransform[i].gameObject.SetActive(false);
        }
        realContent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (uisRectTransform.Length)*uiHeight);
        on = true;
    }

    void Update()
    {
        if(on){
            int a = (int)(realContent.localPosition.y/uiHeight);
            if(aaa!=a){
                aaa=a;
                for(int i=a-3;i<a+shownUi+4;i++){
                    if(i>=0&&i<=uisRectTransform.Length-1){
                        
                        if(i<a||i>a+shownUi){
                            uisRectTransform[i].gameObject.SetActive(false);
                            uisRectTransform[i].SetParent(content);
                        }
                        else if(uisRectTransform[i].parent != realContent){
                            uisRectTransform[i].SetParent(realContent); 
                            uisRectTransform[i].localPosition = new Vector3(uiWidth/2f,-(uiHeight/2f)-(uiHeight*(i)),0f);
                            uisRectTransform[i].gameObject.SetActive(true);
                        }
                    }
                }
                
                    
                
            }
            
        }
        
    }
}
