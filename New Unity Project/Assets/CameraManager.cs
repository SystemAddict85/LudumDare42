using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager> {

    public static Cinemachine.CinemachineBrain Main { get; set; }
    
    [SerializeField]    
    private Cinemachine.CinemachineBrain mainCamera;

    public Cinemachine.CinemachineVirtualCamera playerFollow;    


    public override void Awake()
    {
        base.Awake();        

        if(Instance.mainCamera == null)
        {
            mainCamera = FindObjectOfType<Cinemachine.CinemachineBrain>();
        }
        Main = Instance.mainCamera;
    }

    
}
