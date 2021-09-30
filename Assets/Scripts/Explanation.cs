using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Explanation : MonoBehaviour
{
    private bool showing;
    private Image image;
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text explanationText;
    private RectTransform rectTransform;
    private void Start(){
        image = GetComponent<Image>();
        rectTransform=GetComponent<RectTransform>();
    }
    private void Update(){
        if(showing){
            if(transform.GetSiblingIndex() != transform.parent.childCount-1)
                transform.SetAsLastSibling();
        }
    }
    public IEnumerator ComeonExplanation(Transform parent, Vector3 pos, string name, string explanation){
        showing=true;
        transform.SetParent(parent);
        image.DOKill();
        nameText.DOKill();
        explanationText.DOKill();
        image.DOFade(0.5f,0f);
        nameText.DOFade(1f,0f);
        explanationText.DOFade(1f,0f);
        rectTransform.position = pos;
        nameText.text = name;
        explanationText.text = explanation;
        yield return new WaitForSeconds(2f);
        image.DOFade(0f,1f);
        nameText.DOFade(0f,1f);
        explanationText.DOFade(0f,1f);
        showing=false;
    }
}
