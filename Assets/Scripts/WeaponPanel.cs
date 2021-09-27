
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Numerics;
using CAH.GameSystem.BigNumber;


public class WeaponPanel : MonoBehaviour
{
    [SerializeField]
    private Text weaponNameText=null;
    [SerializeField]
    private Text priceText=null;
    [SerializeField]
    private Text amountText=null;
    [SerializeField]
    private Button purchaseButton=null;
    private Weapon weapon = null;
    [SerializeField]
    private Image weaponImage = null;
    [SerializeField]
    private Image blackImage = null;
    [SerializeField]
    private Text questionMark = null;
    [SerializeField]
    private SaveImage saveImage;
    private void Awake(){
        saveImage = FindObjectOfType<SaveImage>();
        InvokeRepeating("CanUpgrade",0f,1f);
    }
    public void SetValue(Weapon weapon){

        this.weapon = weapon;
        UpdateUI();
    }
    public void UpdateUI(){
        blackImage.gameObject.SetActive(weapon.upgrade==0);
        questionMark.gameObject.SetActive(weapon.upgrade==0);

        weaponNameText.text = (weapon.upgrade==0)?"???":weapon.Name;
        priceText.text = string.Format("{0}",BigIntegerManager.GetUnit(BigInteger.Parse(weapon.price)));
        amountText.text = string.Format("LV.{0}",weapon.upgrade);
        weaponImage.sprite = saveImage.weaponSprites[weapon.weaponNumber];
    }
    private void CanUpgrade(){
        purchaseButton.interactable = BigInteger.Parse(weapon.price)<=GameManager.Instance.money;
    }
    public void OnclickPurchase(){
        if(GameManager.Instance.money < BigInteger.Parse(weapon.price)){
            return;
        }
        GameManager.Instance.money -= BigInteger.Parse(weapon.price);
        
        weapon.price = BigInteger.Parse(weapon.price).ToString();
        weapon.upgrade++;
        GameManager.Instance.UI.UpdateEnergyPanel();
        UpdateUI();
        CanUpgrade();
    }
}