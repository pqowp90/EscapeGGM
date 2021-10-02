
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
    private Text damageText=null;
    [SerializeField]
    private Text amountText=null;
    [SerializeField]
    private Button purchaseButton=null;
    [SerializeField]
    private Button mountingButton=null;
    private Weapon weapon = null;
    [SerializeField]
    private Image weaponImage = null;
    [SerializeField]
    private Image blackImage = null;
    [SerializeField]
    private Text questionMark = null;
    [SerializeField]
    private SaveImage saveImage;
    private Explanation explanation;
    private AudioSource audioSource;
    private void Awake(){
        audioSource = GetComponent<AudioSource>();
        explanation = FindObjectOfType<Explanation>();
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
        damageText.text = string.Format("공격력 : {0}",BigIntegerManager.GetUnit(BigInteger.Parse(weapon.damage)));
        amountText.text = string.Format("LV.{0}",weapon.upgrade);
        weaponImage.sprite = saveImage.weaponSprites[weapon.weaponNumber];

        purchaseButton.transform.GetChild(0).GetComponent<Text>().text = (weapon.upgrade==0)?"구매":"강화";
    }
    private void CanUpgrade(){
        purchaseButton.interactable = BigInteger.Parse(weapon.price)<=GameManager.Instance.money;
        mountingButton.interactable = weapon.upgrade>0;
    }
    public void SetWeapon(){
        GameManager.Instance.CurrentUser.weaponSet = weapon.weaponNumber;
        GameManager.Instance.playerHand.sprite = saveImage.weaponSprites[weapon.weaponNumber];
        GameManager.Instance.SetPlayerDamage();
    }
    public void OnclickPurchase(){
        audioSource.Play();
        if(GameManager.Instance.money < BigInteger.Parse(weapon.price)){
            return;
        }
        GameManager.Instance.money -= BigInteger.Parse(weapon.price);
        weapon.damage = (BigInteger.Parse(weapon.damage)/10*12).ToString();
        
        weapon.price = (BigInteger.Parse(weapon.price)/10*12).ToString();
        
        weapon.upgrade++;
        GameManager.Instance.UI.UpdateEnergyPanel();
        UpdateUI();
        CanUpgrade();
        GameManager.Instance.SetPlayerDamage();
    }
    public void ShowExplanation(){
        StartCoroutine(explanation.ComeonExplanation(weaponNameText.transform.parent.parent,weaponImage.rectTransform.position,(weapon.upgrade==0)?"???":weapon.Name,(weapon.upgrade==0)?"??? ??? ???":weapon.weaponDISC));
        
    }
}