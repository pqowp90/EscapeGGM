using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text energyText = null;
    [SerializeField]
    private Animator beakerAnimator = null;
    [SerializeField]
    private GameObject upgreadPanelTemplate=null;
    private List<UpgreadePanel> upgreadePanals = new List<UpgreadePanel>();
    void Start()
    {
        UpdateEnergyPanel();
        CreatPanels();
    }
    private void CreatPanels(){
        GameObject newPanel = null;
        UpgreadePanel newPanelComponent = null;
        foreach(Soldier soldier in GameManager.Instance.CurrentUser.solderList){
            newPanel = Instantiate(upgreadPanelTemplate,upgreadPanelTemplate.transform.parent);
            newPanelComponent = newPanel.GetComponent<UpgreadePanel>();
            newPanelComponent.SetValue(soldier);
            newPanel.SetActive(true);
        }
    }
    public void OnClickBeaker(){
        GameManager.Instance.CurrentUser.energy += GameManager.Instance.CurrentUser.ePC;
        UpdateEnergyPanel();
        beakerAnimator.SetTrigger("Click");
    }
    public void UpdateEnergyPanel(){
        energyText.text = string.Format("{0} 애너지",GameManager.Instance.CurrentUser.energy);
    }

}
