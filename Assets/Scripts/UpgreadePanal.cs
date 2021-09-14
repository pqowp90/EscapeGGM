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
    public void SetValue(Soldier soldier){

        this.soldier = soldier;
        UpdateUI();
    }
    public void UpdateUI(){
        soldierNameText.text = soldier.solderName;
        priceText.text = string.Format("{0} 애너지",soldier.price);
        amountText.text = string.Format("{0}",soldier.amount);
    }
    public void OnclickPurchase(){
        if(GameManager.Instance.CurrentUser.energy < soldier.price){
            return;
        }
        GameManager.Instance.CurrentUser.energy -= soldier.price;
        soldier.price = (long)(soldier.price*1.25f);
        soldier.amount++;
        GameManager.Instance.UI.UpdateEnergyPanel();
        UpdateUI();
    }
}
