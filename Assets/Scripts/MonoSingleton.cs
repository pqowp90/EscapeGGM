
using UnityEngine;


public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour  
{
    private static bool shuttingdown=false;
    private static object locker = new object();
    private static T instance = null;
    public static T Instance{
        get{
            if(shuttingdown){
                Debug.Log("[Instance] Instance"+typeof(T)+"is alreay destroyed. Returning null");
            }
            lock(locker){
                if(instance == null){
                    instance = FindObjectOfType<T>();
                    if(instance == null){
                        instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                        DontDestroyOnLoad(instance);
                    }
                }
                return instance;
            }
        }
    }
    private void OnDestroy(){
        shuttingdown = true;
    }
    private void OnApplicatonQuit(){
        shuttingdown = true;
    }
}