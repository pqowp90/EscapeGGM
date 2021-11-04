using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Numerics;
using CAH.GameSystem.BigNumber;
using DG.Tweening;



public class UpgreadePanel : MonoBehaviour
{
    [SerializeField]
    private Transform noyes;
    [SerializeField]
    private Text soldierNameText=null;
    [SerializeField]
    private Text priceText=null;
    [SerializeField]
    private Text amountText=null;
    [SerializeField]
    private Text damageText=null;
    [SerializeField]
    private Button purchaseButton=null;
    private Soldier soldier = null;
    [SerializeField]
    private Image soldierImage = null;
    [SerializeField]
    private Image blackImage = null;
    [SerializeField]
    private Text questionMark = null;
    [SerializeField]
    private Sprite[] soldierSpeite;
    [SerializeField]
    private Transform student;
    private AudioSource audioSource;
    private void Awake(){
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("CanUpgrade",0f,1f);
        
    }
    public void SetValue(Soldier soldier){

        this.soldier = soldier;
        UpdateUI();
    }
    public void UpdateUI(){
        if(soldier.upgrade>0){
            noyes.GetChild(soldier.soldierNumber).gameObject.SetActive(true);
        }
        blackImage.gameObject.SetActive(soldier.upgrade==0);
        questionMark.gameObject.SetActive(soldier.upgrade==0);

        soldierNameText.text = (soldier.upgrade==0)?"???":soldier.solderName;
        priceText.text = string.Format("{0}",BigIntegerManager.GetUnit(BigInteger.Parse(soldier.price)));
        amountText.text = string.Format("LV.{0}",soldier.upgrade);
        soldierImage.sprite = soldierSpeite[soldier.soldierNumber];
        purchaseButton.transform.GetChild(0).GetComponent<Text>().text = (soldier.upgrade==0)?"구매":"강화";
        damageText.text = string.Format("공격력 : 플레이어 공격력 {0}%",Mathf.Pow(10,soldier.soldierNumber)*(10+soldier.upgrade));
    }
    
    private void CanUpgrade(){
        purchaseButton.interactable = BigInteger.Parse(soldier.price)<=GameManager.Instance.money;
    }
    public void OnclickPurchase(){
        audioSource.Play();
        if(GameManager.Instance.money < BigInteger.Parse(soldier.price)){
            return;
        }
        GameManager.Instance.money -= BigInteger.Parse(soldier.price);
        
        soldier.price = BigInteger.Multiply(BigInteger.Divide(BigInteger.Parse(soldier.price),2),10).ToString();
        soldier.upgrade++;
        GameManager.Instance.UI.UpdateEnergyPanel();
        UpdateUI();
        CanUpgrade();
    }
    private IEnumerator Hihello(int num){
        student.GetChild(num).gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        student.GetChild(num).gameObject.SetActive(false);
    }
    private void OnDisable(){
        for(int i=0;i<4;i++){
            student.GetChild(i).gameObject.SetActive(false);
        }   
    }
    public void ShowExplanation(){
        if(soldier.upgrade>0)
            StartCoroutine(Hihello(soldier.soldierNumber));
        
    }
}