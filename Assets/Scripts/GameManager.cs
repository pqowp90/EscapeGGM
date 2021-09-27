using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

using CAH.GameSystem.BigNumber;
    

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
    public BigInteger money,ePC;
    public SaveImage saveImage;
    private void Awake()
    {
        saveImage = GetComponent<SaveImage>();
        //Debug.Log(BigInteger.Pow(1000, 3).ToString());
        SAVE_PATH = Application.dataPath+"/Save";//persistentDataPath
        if(!Directory.Exists(SAVE_PATH)){
            Directory.CreateDirectory(SAVE_PATH);
        }
        InvokeRepeating("SaveToJson",1f,5f);
        InvokeRepeating("EarnEnergyPerSecond",0f,1f);
        //SaveToJson();
        LoadFromJsom();
        uiManager = GetComponent<UIManager>();
        
    }
    public void Move(bool isMove){
        isRun = isMove;
        playerAnimator.SetBool("stop",!isMove);
    }


    private void EarnEnergyPerSecond(){
        
        foreach(Soldier soldier in user.solderList){
            money += BigInteger.Parse(soldier.ePs) * (BigInteger)soldier.upgrade;
        }
        UI.UpdateEnergyPanel();
    }
    private void LoadFromJsom(){
        string json = "";
        if(File.Exists(SAVE_PATH+SAVE_FILENAME)){
            json = File.ReadAllText(SAVE_PATH+SAVE_FILENAME);
            user = JsonUtility.FromJson<User>(json);
            
        }
        ePC=BigInteger.Parse(user.ePC);
        money=BigInteger.Parse(user.money);
    }
    private void SaveToJson(){
        user.money = money.ToString();
        user.ePC = ePC.ToString();
        string json = JsonUtility.ToJson(user, true);
        File.WriteAllText(SAVE_PATH+SAVE_FILENAME,json, System.Text.Encoding.UTF8);
    }
    private void OnApplicationQuit(){
        SaveToJson();
    }
    


    
}
