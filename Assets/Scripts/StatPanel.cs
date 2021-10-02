using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Numerics;
using CAH.GameSystem.BigNumber;



public class StatPanel : MonoBehaviour
{
    [SerializeField]
    private Text statNameText=null;
    [SerializeField]
    private Text priceText=null;
    [SerializeField]
    private Text amountText=null;
    [SerializeField]
    private Text damageText=null;
    [SerializeField]
    private Button purchaseButton=null;
    private myStat myStat = null;
    [SerializeField]
    private Image myStatImage = null;
    
    [SerializeField]
    private Sprite[] myStatSpeite;
    private AudioSource audioSource;
    private void Awake(){
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("CanUpgrade",0f,1f);
    }
    public void SetValue(myStat myStat){

        this.myStat = myStat;
        UpdateUI();
    }
    public void UpdateUI(){
        switch(myStat.numStat){
            case 0:
            damageText.text = "공격력 +"+(GameManager.Instance.CurrentUser.myStat[0].upgrade*3).ToString()+"%";
            GameManager.Instance.SetPlayerDamage();
            break;
            case 1:
            damageText.text = "이동속도 +"+(GameManager.Instance.CurrentUser.myStat[1].upgrade).ToString()+"%";
            break;
            case 2:
            damageText.text = "처치당 돈 +"+(GameManager.Instance.CurrentUser.myStat[2].upgrade).ToString()+"%";
            break;
            case 3:
            damageText.text = "10초에 "+(GameManager.Instance.CurrentUser.myStat[3].upgrade).ToString()+"번 클릭";
            break;
        }
        
        statNameText.text = myStat.Name;
        priceText.text = string.Format("{0}",BigIntegerManager.GetUnit(BigInteger.Parse(myStat.price)));
        amountText.text = string.Format("LV.{0}",myStat.upgrade);
        myStatImage.sprite = myStatSpeite[myStat.numStat];
    }
    private void CanUpgrade(){
        purchaseButton.interactable = BigInteger.Parse(myStat.price)<=GameManager.Instance.money;
        if(myStat.numStat==1&&myStat.upgrade>=66){purchaseButton.interactable = false;
        priceText.text = "MAX";}
    }
    public void OnclickPurchase(){
        audioSource.Play();
        if(myStat.numStat==1&&myStat.upgrade>=66)return;
        if(GameManager.Instance.money < BigInteger.Parse(myStat.price)){
            return;
        }
        GameManager.Instance.money -= BigInteger.Parse(myStat.price);
        
        myStat.price = BigInteger.Multiply(BigInteger.Divide(BigInteger.Parse(myStat.price),8),10).ToString();
        myStat.upgrade++;
        GameManager.Instance.UI.UpdateEnergyPanel();
        UpdateUI();
        CanUpgrade();
        
        
    }
}