
using UnityEngine;

public class AllPoolManager : MonoSingleton<AllPoolManager>
{
    [SerializeField]
    private GameObject[] objects;
    [SerializeField]
    private const int count=0;
    [SerializeField]
    private Transform[] mothersOfObjects;
    
    void Awake()
    {
        for(int index=0;index<objects.Length;index++){
            mothersOfObjects[index] = new GameObject(objects[index].name+"'s Mother").transform;
            mothersOfObjects[index].transform.SetParent(transform);
        }
    }

    public Transform GetObj(int index){
        Transform obj;
        if(mothersOfObjects[index].transform.childCount>0){
            
            obj = mothersOfObjects[index].GetChild(0);
            obj.SetParent(null);
            
        }else {
            obj = Instantiate(objects[index]).transform;
        }
        obj.gameObject.SetActive(false);
        return obj;
    }
    public Transform GetObjPos(int index, Vector2 Pos){
        Transform obj;
        obj = GetObj(index);
        obj.position = Pos;
        return obj;
    }
    public void PoolObj(Transform obj,int index){
        obj.gameObject.SetActive(false);
        obj.SetParent(mothersOfObjects[index]);
    }
}
