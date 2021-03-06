using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

using CAH.GameSystem.BigNumber;
    

public class GameManager : MonoSingleton<GameManager>
{
    public BigInteger playerDamage=10;
    public SpriteRenderer playerHand;
    private string SAVE_PATH = "";
    private string SAVE_FILENAME = "/SaveFile.txt";
    [SerializeField]
    private User user = null;
    public User CurrentUser {get{return user;}}
    private UIManager uiManager = null;
    public UIManager UI {get{return uiManager;}}
    public bool isRun=true;
    public float backgroundSpeed;
    [SerializeField]
    private Animator playerAnimator;
    public BigInteger money,ePC;
    public SaveImage saveImage;
    private void Awake()
    {
        saveImage = GetComponent<SaveImage>();
        SAVE_PATH = Application.persistentDataPath+"/Save";//persistentDataPath
        if(!Directory.Exists(SAVE_PATH)){
            Directory.CreateDirectory(SAVE_PATH);
        }
        InvokeRepeating("SaveToJson",1f,60f);
        InvokeRepeating("EarnEnergyPerSecond",0f,1f);
        //SaveToJson();
        LoadFromJsom();
        uiManager = GetComponent<UIManager>();
        playerHand.sprite = saveImage.weaponSprites[user.weaponSet];
        SetPlayerDamage();
        
    }
    
    
    public void SetPlayerDamage(){
        playerDamage = (BigInteger.Parse(user.Weapon[user.weaponSet].damage)+(BigInteger.Parse(user.Weapon[user.weaponSet].damage)/100*(user.myStat[0].upgrade*3)));
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
            
        }else{
            SaveToJson();
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
