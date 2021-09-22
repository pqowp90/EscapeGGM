using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
    private Sprite[] soldierSpeite;
    public void SetValue(Soldier soldier){

        this.soldier = soldier;
        UpdateUI();
    }
    public void UpdateUI(){
        soldierNameText.text = soldier.solderName;
        priceText.text = string.Format("{0} 원",soldier.price);
        amountText.text = string.Format("LV.{0}",soldier.upgrade);
        soldierImage.sprite = soldierSpeite[soldier.soldierNumber];
    }
    public void OnclickPurchase(){
        if(GameManager.Instance.CurrentUser.energy < soldier.price){
            return;
        }
        GameManager.Instance.CurrentUser.energy -= soldier.price;
        soldier.price = (long)(soldier.price*1.25f);
        soldier.upgrade++;
        GameManager.Instance.UI.UpdateEnergyPanel();
        UpdateUI();
    }
}
