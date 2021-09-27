using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using CAH.GameSystem.BigNumber;
public class UIManager : MonoBehaviour
{
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
    private List<UpgreadePanel> upgreadePanals = new List<UpgreadePanel>();
    [SerializeField]
    private EnergyText energyTextTemplate=null;
    [SerializeField]
    private Transform pool = null;
    [SerializeField]
    private GameObject[] uis;
    void Start()
    {
        UpdateEnergyPanel();
        CreatPanels();
    }
    private void CreatPanels(){
        int i=0;
        GameObject newPanel = null;
        UpgreadePanel newPanelComponent = null;
        foreach(Soldier soldier in GameManager.Instance.CurrentUser.solderList){
            newPanel = Instantiate(upgreadPanelTemplate,upgreadPanelTemplate.transform.parent);
            newPanelComponent = newPanel.GetComponent<UpgreadePanel>();
            soldier.soldierNumber = i;
            soldier.upgrade = 0;
            newPanelComponent.SetValue(soldier);
            newPanel.SetActive(true);
            i++;
        }
        i=0;
        WeaponPanel newPanelComponent2 = null;
        foreach(Weapon weapon in GameManager.Instance.CurrentUser.Weapon){
            newPanel = Instantiate(upgreadPanelTemplate2,upgreadPanelTemplate2.transform.parent);
            newPanelComponent2 = newPanel.GetComponent<WeaponPanel>();
            weapon.weaponNumber = i;
            weapon.upgrade = 0;
            newPanelComponent2.SetValue(weapon);
            newPanel.SetActive(true);
            i++;
        }
    }
    public void OnClickBeaker(){
        GameManager.Instance.money +=  GameManager.Instance.ePC;
        UpdateEnergyPanel();
        beakerAnimator.SetTrigger("atk");
        //Invoke("Bbok",0.1f);
        
    }
    private void Bbok(){
        AllPoolManager.Instance.GetObjPos(0,finger.position).gameObject.SetActive(true);
        EnergyText newText = null;
        if(pool.childCount>0){
            newText = pool.GetChild(0).GetComponent<EnergyText>();
            
        }else{
            newText = Instantiate(energyTextTemplate, energyTextTemplate.transform.parent);
        }
        newText.Show(Input.mousePosition);

    }
    public void UpdateEnergyPanel(){
        energyText.text = string.Format("{0}",BigIntegerManager.GetUnit(GameManager.Instance.money));
    }
    public void Uiswitch(int num){
        for(int i=0;i<uis.Length;i++){
            uis[i].SetActive(i==num);
        }
    }
}
