
using UnityEngine;

public class BasicParticle : MonoBehaviour
{
    [SerializeField]
    private float lifeTime;
    [SerializeField]
    private int index;
    void Start()
    {
        //Invoke("Pool",lifeTime);
    }
    void OnEnable(){
        Invoke("Pool",lifeTime);
    }
    private void Pool(){
        AllPoolManager.Instance.PoolObj(transform,index);
    }
}
