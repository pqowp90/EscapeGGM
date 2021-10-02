using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Numerics;
using CAH.GameSystem.BigNumber;
public class EnergyText : MonoBehaviour
{
    private Text energyText = null;
    [SerializeField]
    private Canvas canvas = null;
    [SerializeField]
    private Transform pool = null;
    public void Show(UnityEngine.Vector2 mousePos,BigInteger money){
        mousePos = Camera.main.WorldToScreenPoint(mousePos);
        energyText = GetComponent<Text>();
        energyText.text = string.Format("+{0}",BigIntegerManager.GetUnit(money));

        transform.SetParent(canvas.transform);
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = new UnityEngine.Vector3(transform.position.x,transform.position.y,0f);
        gameObject.SetActive(true);


        RectTransform rectTransform = GetComponent<RectTransform>();
        float targetPosY = rectTransform.anchoredPosition.y + 50f;

        energyText.DOFade(0f,0.5f).OnComplete(()=>Despawn());
        rectTransform.DOAnchorPosY(targetPosY,0.5f);
        
        
        //transform.SetParent(canvas.transform);
    }
    public void Despawn(){
        energyText.DOFade(1f,0f);
        transform.SetParent(pool);
        gameObject.SetActive(false);
    }
}
