using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Numerics;
using CAH.GameSystem.BigNumber;

public class MonsterManager : MonoBehaviour
{
    private AudioSource audioSource;
    public Transform myTransform;
    public float speed;
    private SaveImage saveImage;
    private Sprite monsterSprite;
    private BigInteger maxHp;
    private BigInteger hp;
    [SerializeField]
    private RectTransform hpBar;
    private Image hpBarImage;
    private Text hpBarText;
    private bool showing;
    private int randomInt;
    [SerializeField]
    private EnergyText energyTextTemplate=null;
    [SerializeField]
    private Transform pool = null;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        hpBarImage = hpBar.GetChild(0).GetChild(0).GetComponent<Image>();
        hpBarText = hpBar.GetChild(1).GetComponent<Text>();
        saveImage = FindObjectOfType<SaveImage>();
        ComeOnBaby();
    }
    void Update()
    {
        if(showing){
            hpBar.position = myTransform.position;
            hpBarImage.fillAmount = ((int)BigInteger.Divide(BigInteger.Multiply(hp,100),maxHp))/100f;
            string money1 = BigIntegerManager.GetUnit(maxHp);
            string money2 = BigIntegerManager.GetUnit(hp);
            hpBarText.text = string.Format("{0}/{1}{2}",(money1.Substring(0,money1.Length-3)),money2.Substring(0,money2.Length-3),money2.Substring(money2.Length-1));
        }
        
    }
    private Transform GetMon(){
        Transform hiTransform=null;
        for (int i=0;i<transform.childCount;i++)
        {
            hiTransform = transform.GetChild(i);
            if(!hiTransform.gameObject.activeSelf){
                hiTransform.DOKill();
                return hiTransform;
            }
        }
        return hiTransform;
    }
    public void ComeOnBaby(){
        StartCoroutine(ComeOn());
    }
    private IEnumerator ComeOn(){
        speed = 2-GameManager.Instance.CurrentUser.myStat[1].upgrade*0.01f;
        showing = true;
        myTransform=GetMon();
        randomInt = Random.Range((GameManager.Instance.CurrentUser.progress>80)?11:0,(GameManager.Instance.CurrentUser.progress>80)?saveImage.monsterSprites.Length:11);
        maxHp = BigInteger.Parse(GameManager.Instance.CurrentUser.Monster[randomInt].hp);
        GameManager.Instance.CurrentUser.Monster[randomInt].met = true;
        hp = maxHp;
        myTransform.GetComponent<SpriteRenderer>().sprite = 
            saveImage.monsterSprites[randomInt];
        myTransform.DOKill();
        myTransform.GetComponent<SpriteRenderer>().DOFade(1f,0f);
        myTransform.rotation = UnityEngine.Quaternion.identity;
        myTransform.DOMoveX(12.58f,0f);
        myTransform.gameObject.SetActive(true);
        myTransform.DOMoveX(2.5f,speed);
        yield return new WaitForSeconds(speed/1.5f);
        GameManager.Instance.Move(false);
    }
    public void HitBaby(BigInteger damage){
        StartCoroutine(Hit(myTransform,damage));
    }
    public IEnumerator Hit(Transform hihihi ,BigInteger damage){
        if(showing){
            AllPoolManager.Instance.GetObjPos(0,hihihi.position+new UnityEngine.Vector3(0f,-1f,0f)).gameObject.SetActive(true);
            hihihi.DOKill();
            hp-=damage;
            if(hp<=0){
                StartCoroutine(Die(hihihi));
            }else{
                //myTransform.DOMoveX(2.5f,0f);
                yield return new WaitForSeconds(0.05f);
                if(showing)
                    hihihi.DOMoveX(hihihi.position.x+0.5f,0.05f);
                yield return new WaitForSeconds(0.05f);
                if(showing)
                hihihi.DOMoveX(2.5f,0.1f);
            }
        }
    }
    private void Bbok(UnityEngine.Vector2 here){
        // AllPoolManager.Instance.GetObjPos(0,finger.position).gameObject.SetActive(true);
        EnergyText newText = null;
        if(pool.childCount>0){
            newText = pool.GetChild(0).GetComponent<EnergyText>();
            
        }else{
            newText = Instantiate(energyTextTemplate, energyTextTemplate.transform.parent);
        }
        newText.Show(here,BigInteger.Parse(GameManager.Instance.CurrentUser.Monster[randomInt].gold)+(BigInteger.Parse(GameManager.Instance.CurrentUser.Monster[randomInt].gold))/100*GameManager.Instance.CurrentUser.myStat[2].upgrade);

    }
    public IEnumerator Die(Transform hihihi){
        
        showing = false;
        GameManager.Instance.CurrentUser.progress++;
        if(GameManager.Instance.CurrentUser.progress%10==0){
            foreach (Monster monster in GameManager.Instance.CurrentUser.Monster)
            {
                monster.hp=(BigInteger.Parse(monster.hp)*2).ToString();
                monster.gold=(BigInteger.Parse(monster.gold)*2).ToString();
            }
        }
        Bbok(hihihi.position);
        GameManager.Instance.money+=BigInteger.Parse(GameManager.Instance.CurrentUser.Monster[randomInt].gold)+(BigInteger.Parse(GameManager.Instance.CurrentUser.Monster[randomInt].gold))/100*GameManager.Instance.CurrentUser.myStat[2].upgrade;
        StartCoroutine(ComeOn());
        hihihi.DOKill();
        hihihi.DOMoveX(-12f,speed);
        hihihi.DORotate(new UnityEngine.Vector3(0f,0f,-90),speed/2f);
        hihihi.GetComponent<SpriteRenderer>().DOFade(0f,speed/2f);
        GameManager.Instance.Move(true);
        yield return new WaitForSeconds(speed);
        hihihi.gameObject.SetActive(false);
    }
}