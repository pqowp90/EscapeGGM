using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Numerics;
using CAH.GameSystem.BigNumber;



public class UpgreadePanel : MonoBehaviour
{
    [SerializeField]
    private Text soldierNameText=null;
    [SerializeField]
    private Text priceText=null;
    [SerializeField]
    private Text amountText=null;
    [SerializeField]
    private Button purchaseButton=null;
    private Soldier soldier = null;
    [SerializeField]
    private Image soldierImage = null;
    [SerializeField]
    private Image blackImage = null;
    [SerializeField]
    private Sprite[] soldierSpeite;
    private void Awake(){
        InvokeRepeating("CanUpgrade",0f,1f);
    }
    public void SetValue(Soldier soldier){

        this.soldier = soldier;
        UpdateUI();
    }
    public void UpdateUI(){
        blackImage.gameObject.SetActive(soldier.upgrade==0);

        soldierNameText.text = (soldier.upgrade==0)?"???":soldier.solderName;
        priceText.text = string.Format("{0}",BigIntegerManager.GetUnit(BigInteger.Parse(soldier.price)));
        amountText.text = string.Format("LV.{0}",soldier.upgrade);
        soldierImage.sprite = soldierSpeite[soldier.soldierNumber];
    }
    private void CanUpgrade(){
        purchaseButton.interactable = BigInteger.Parse(soldier.price)<=GameManager.Instance.money;
    }
    public void OnclickPurchase(){
        if(GameManager.Instance.money < BigInteger.Parse(soldier.price)){
            return;
        }
        GameManager.Instance.money -= BigInteger.Parse(soldier.price);
        
        soldier.price = BigInteger.Parse(soldier.price).ToString();
        soldier.upgrade++;
        GameManager.Instance.UI.UpdateEnergyPanel();
        UpdateUI();
        CanUpgrade();
    }
}
