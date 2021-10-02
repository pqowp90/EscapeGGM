using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Numerics;
using CAH.GameSystem.BigNumber;
using DG.Tweening;



public class Dogam : MonoBehaviour
{
    
    [SerializeField]
    private Text soldierNameText=null;
    
    
    
    
    private Monster monster = null;
    [SerializeField]
    private Image monsterImage = null;
    [SerializeField]
    private Image blackImage = null;
    [SerializeField]
    private Text questionMark = null;
    [SerializeField]
    private SaveImage saveImage;
    private Explanation explanation;
        
    
    private void Awake(){
        explanation = FindObjectOfType<Explanation>();
        InvokeRepeating("UpdateUI",0f,1f);
    }
    public void SetValue(Monster monster){

        this.monster = monster;
        UpdateUI();
    }
    public void UpdateUI(){
    
        monsterImage.sprite = saveImage.monsterSprites[monster.monsterNumber];
        blackImage.gameObject.SetActive(!monster.met);
        questionMark.gameObject.SetActive(!monster.met);

        soldierNameText.text = (!monster.met)?"???":monster.Name;
        
        
    }
    public void ShowExplanation(){
        Debug.Log("a");
        StartCoroutine(explanation.ComeonExplanation(soldierNameText.transform.parent.parent,monsterImage.rectTransform.position,(!monster.met)?"???":monster.Name,(!monster.met)?"??? ??? ???":monster.monsterDISC));
        
    }
    

}