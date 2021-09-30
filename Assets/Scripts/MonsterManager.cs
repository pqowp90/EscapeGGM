using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MonsterManager : MonoBehaviour
{
    [SerializeField]
    private Transform myTransform;
    [SerializeField]
    private float speed;
    private SaveImage saveImage;
    private Sprite monsterSprite;
    void Start()
    {
        saveImage = FindObjectOfType<SaveImage>();
        myTransform = GetComponent<Transform>();
    }
    void Update()
    {
        
    }
    private Transform GetMon(){
        Transform hiTransform=null;
        for (int i=0;i<transform.childCount;i++)
        {
            hiTransform = transform.GetChild(i);
            Debug.Log("dd");
            if(!hiTransform.gameObject.activeSelf){
                hiTransform.gameObject.SetActive(true);
                return hiTransform;
            }
        }
        return hiTransform;
    }
    public void ComeOnBaby(){
        StartCoroutine(ComeOn());
    }
    private IEnumerator ComeOn(){
        
        myTransform=GetMon();
        myTransform.GetComponent<SpriteRenderer>().sprite = 
            saveImage.monsterSprites[Random.Range(0,saveImage.monsterSprites.Length)];
        myTransform.DOMoveX(2.5f,speed);
        yield return new WaitForSeconds(speed/1.5f);
        GameManager.Instance.Move(false);
    }
    public void HitBaby(){
        StartCoroutine(Hit());
    }
    public IEnumerator Hit(){
        //myTransform.DOMoveX(2.5f,0f);
        yield return new WaitForSeconds(0.05f);
        myTransform.DOMoveX(myTransform.position.x+0.5f,0.05f);
        yield return new WaitForSeconds(0.05f);
        myTransform.DOMoveX(2.5f,0.1f);
    }
}