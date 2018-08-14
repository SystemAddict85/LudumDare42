using UnityEngine;
using System.Collections;

public class Singleton<SingleInstance> : MonoBehaviour where SingleInstance : Singleton<SingleInstance>
{
    public static SingleInstance Instance { get; set; }

    public virtual void Awake()
    {
        if (!Instance)
        {
            Instance = this as SingleInstance;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
