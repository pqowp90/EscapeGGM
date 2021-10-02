using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Numerics;
using CAH.GameSystem.BigNumber;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private bool isNew;
    [SerializeField]
    private Transform noyes;
    [SerializeField]
    private Text energyText = null;
    [SerializeField]
    private Animator beakerAnimator = null;
    [SerializeField]
    private Transform finger;
    [SerializeField]
    private GameObject upgreadPanelTemplate=null;
    [SerializeField]
    private GameObject upgreadPanelTemplate2=null;
    
    [SerializeField]
    private GameObject upgreadPanelTemplate3=null;
    [SerializeField]
    private GameObject upgreadPanelTemplate4=null;
    private List<UpgreadePanel> upgreadePanals = new List<UpgreadePanel>();
    
    [SerializeField]
    private GameObject[] uis;
    [SerializeField]
    private ScrollManager scrollManager;
    [SerializeField]
    private ScrollManager scrollManager2;
    [SerializeField]
    private MonsterManager monsterManager;
    
    void Start()
    {
        
        monsterManager = FindObjectOfType<MonsterManager>();
        UpdateEnergyPanel();
        CreatPanels();
        StartCoroutine(autoClick());
        StartCoroutine(Barssa());
    }
    
    private void CreatPanels(){
        int i=0;
        GameObject newPanel = null;
        UpgreadePanel newPanelComponent = null;
        foreach(Soldier soldier in GameManager.Instance.CurrentUser.solderList){
            newPanel = Instantiate(upgreadPanelTemplate,upgreadPanelTemplate.transform.parent);
            newPanelComponent = newPanel.GetComponent<UpgreadePanel>();
            if(isNew){
                soldier.soldierNumber = i;
                soldier.upgrade = 0;
            }
            if(soldier.upgrade>0){
                noyes.GetChild(i).gameObject.SetActive(true);
            }
            newPanelComponent.SetValue(soldier);
            newPanel.SetActive(true);
            i++;
        }
        i=0;
        Dogam newPanelComponent4 = null;
        foreach(Monster monster in GameManager.Instance.CurrentUser.Monster){
            newPanel = Instantiate(upgreadPanelTemplate4,upgreadPanelTemplate4.transform.parent);
            newPanelComponent4 = newPanel.GetComponent<Dogam>();
            monster.monsterNumber = i;
            
            newPanelComponent4.SetValue(monster);
            newPanel.SetActive(true);
            i++;
        }
        i=0;
        WeaponPanel newPanelComponent2 = null;
        foreach(Weapon weapon in GameManager.Instance.CurrentUser.Weapon){
            
            newPanel = Instantiate(upgreadPanelTemplate2,upgreadPanelTemplate2.transform.parent);
            newPanelComponent2 = newPanel.GetComponent<WeaponPanel>();
            if(isNew){
                weapon.weaponNumber = i;
                weapon.upgrade = (i==0)?1:0;
                weapon.damage = ((BigInteger)100*(BigInteger)Mathf.Pow(3,i)).ToString();
                weapon.price = ((BigInteger)1000*(BigInteger)Mathf.Pow(3,i)).ToString();
            }
            newPanelComponent2.SetValue(weapon);
            newPanel.SetActive(true);
            i++;
        }
        i=0;
        StatPanel newPanelComponent3 = null;
        foreach(myStat myStat in GameManager.Instance.CurrentUser.myStat){
            
            newPanel = Instantiate(upgreadPanelTemplate3,upgreadPanelTemplate3.transform.parent);
            newPanelComponent3 = newPanel.GetComponent<StatPanel>();
            newPanelComponent3.SetValue(myStat);
            myStat.numStat = i;
            newPanel.SetActive(true);
            i++;
        }
        if(scrollManager!=null)
            scrollManager.GetUi();
        if(scrollManager2!=null)
            scrollManager2.GetUi();
        if(isNew){
            foreach(Monster monster in GameManager.Instance.CurrentUser.Monster){
                monster.hp = "1000";
                monster.gold = "200";
            }
        }
    }
    public void OnClickBeaker(){
        if(GameManager.Instance.isRun)return;
        GameManager.Instance.money +=  GameManager.Instance.ePC;
        UpdateEnergyPanel();
        beakerAnimator.SetTrigger("atk");
        monsterManager.HitBaby(GameManager.Instance.playerDamage);
        
    }
    
    public void UpdateEnergyPanel(){
        
        energyText.text = string.Format("{0}",BigIntegerManager.GetUnit(GameManager.Instance.money));
    }
    public void Uiswitch(int num){
        for(int i=0;i<uis.Length;i++){
            uis[i].SetActive(i==num);
        }
    }
    private IEnumerator autoClick(){
        while(true){
            if(GameManager.Instance.CurrentUser.myStat[3].upgrade==0)
                yield return new WaitForSeconds(1f);
            else{
                yield return new WaitForSeconds(10f/(GameManager.Instance.CurrentUser.myStat[3].upgrade));
                if(!GameManager.Instance.isRun)
                    OnClickBeaker();
            }
        }
    }
    private IEnumerator Barssa(){
        while(true){
            yield return new WaitForSeconds(3f);
            foreach(Soldier soldier in GameManager.Instance.CurrentUser.solderList){
                if(soldier.upgrade>0&&GameManager.Instance.isRun==false){
                    Transform hihi = AllPoolManager.Instance.GetObj(1).GetComponent<Transform>();
                    hihi.position = noyes.GetChild(soldier.soldierNumber).position;
                    hihi.gameObject.SetActive(true);
                    hihi.GetComponent<SpriteRenderer>().DOFade(1f,0f);
                    hihi.DOMove(monsterManager.myTransform.position+new UnityEngine.Vector3(0f,-1f,0f),0.5f);
                    hihi.GetComponent<SpriteRenderer>().DOFade(0f,0.8f);
                    monsterManager.HitBaby(GameManager.Instance.playerDamage/100*(BigInteger)Mathf.Pow(10,soldier.soldierNumber)*(10+soldier.upgrade));
                    
                }
            }
        }
    }
}
