using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private string SAVE_PATH = "";
    private string SAVE_FILENAME = "/SaveFile.txt";
    [SerializeField]
    private User user = null;
    public User CurrentUser {get{return user;}}
    private UIManager uiManager = null;
    public UIManager UI {get{return uiManager;}}
    public bool isRun=false;
    public float backgroundSpeed;
    [SerializeField]
    private Animator playerAnimator;
    private void Awake()
    {
        
        SAVE_PATH = Application.dataPath+"/Save";//persistentDataPath
        if(!Directory.Exists(SAVE_PATH)){
            Directory.CreateDirectory(SAVE_PATH);
        }
        InvokeRepeating("SaveToJson",1f,5f);
        InvokeRepeating("EarnEnergyPerSecond",0f,0.1f);
        LoadFromJsom();
        uiManager = GetComponent<UIManager>();
    }
    public void Move(bool isMove){
        isRun = isMove;
        playerAnimator.SetBool("stop",!isMove);
    }
    private void EarnEnergyPerSecond(){
        
        foreach(Soldier soldier in user.solderList){
            user.energy += soldier.ePs * soldier.upgrade;
        }
        //Debug.Log(user.energy);
        UI.UpdateEnergyPanel();
    }
    private void LoadFromJsom(){
        string json = "";
        if(File.Exists(SAVE_PATH+SAVE_FILENAME)){
            json = File.ReadAllText(SAVE_PATH+SAVE_FILENAME);
            user = JsonUtility.FromJson<User>(json);
            
        }
    }
    private void SaveToJson(){
        string json = JsonUtility.ToJson(user, true);
        File.WriteAllText(SAVE_PATH+SAVE_FILENAME,json, System.Text.Encoding.UTF8);
    }
    private void OnApplicationQuit(){
        SaveToJson();
    }
}
