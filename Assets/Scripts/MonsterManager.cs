using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Numerics;
using CAH.GameSystem.BigNumber;

public class MonsterManager : MonoBehaviour
{
    
    [SerializeField]
    private Transform myTransform;
    [SerializeField]
    private float speed;
    private SaveImage saveImage;
    private Sprite monsterSprite;
    private BigInteger maxHp;
    private BigInteger hp;
    [SerializeField]
    private RectTransform hpBar;
    private Image hpBarImage;
    private bool showing;
    private int randomInt;
    void Start()
    {
        hpBarImage = hpBar.GetChild(0).GetChild(0).GetComponent<Image>();
        saveImage = FindObjectOfType<SaveImage>();
    }
    void Update()
    {
        if(showing){
            Debug.Log(hp);
            hpBar.position = myTransform.position;
            hpBarImage.fillAmount = ((int)BigInteger.Divide(BigInteger.Multiply(hp,100),maxHp))/100f;
        }
        
    }
    private Transform GetMon(){
        Transform hiTransform=null;
        for (int i=0;i<transform.childCount;i++)
        {
            hiTransform = transform.GetChild(i);
            Debug.Log("dd");
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
        showing = true;
        myTransform=GetMon();
        randomInt = Random.Range(0,saveImage.monsterSprites.Length-1);
        maxHp = BigInteger.Parse(GameManager.Instance.CurrentUser.Monster[randomInt].hp);
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
    public void HitBaby(){
        StartCoroutine(Hit(myTransform));
    }
    public IEnumerator Hit(Transform hihihi){
        AllPoolManager.Instance.GetObjPos(0,hihihi.position).gameObject.SetActive(true);
        hihihi.DOKill();
        hp-=GameManager.Instance.playerDamage;
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
        GameManager.Instance.money+=BigInteger.Parse(GameManager.Instance.CurrentUser.Monster[randomInt].gold);
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