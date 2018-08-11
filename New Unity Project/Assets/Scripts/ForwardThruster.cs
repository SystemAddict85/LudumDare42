using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class ForwardThruster : MonoBehaviour
{

    private Movement move;
    public bool isEngineOn = true;

    [SerializeField]
    [Range(-1f, 1f)]
    private float thrustAmount = 1f;
    [SerializeField]
    private float boostAmount = 1f;
    [SerializeField]
    private float boostMax = 2f;
    [SerializeField]
    private float brakeAmount = 1f;
    [SerializeField]
    private float brakeMin = .5f;
    [SerializeField]
    private float boostCameraDist = -3.5f;
    [SerializeField]
    private float brakeCameraDist = -1.5f;
    private float followCameraDist;

    [SerializeField]
    private bool returnCamera = true;

    
    private float currentFollowDist;

    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera followCamera;

    [SerializeField]
    private float cameraReturnSpeed;

    private void Awake()
    {
        move = GetComponent<Movement>();
        followCameraDist = followCamera.GetCinemachineComponent<Cinemachine.CinemachineTransposer>().m_FollowOffset.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEngineOn)
        {
            move.Move(transform.forward, thrustAmount * boostAmount * brakeAmount);
        }
        if (returnCamera)
        {
            currentFollowDist = followCamera.GetCinemachineComponent<Cinemachine.CinemachineTransposer>().m_FollowOffset.z;
            followCamera.GetCinemachineComponent<Cinemachine.CinemachineTransposer>().m_FollowOffset.z = Mathf.Lerp(currentFollowDist, followCameraDist, cameraReturnSpeed);
            if (Mathf.Abs(currentFollowDist - followCameraDist) < 0.1f)
            {
                returnCamera = false;
            }
            
        }

    }

    public void Boost(float amount)
    {
        returnCamera = false;
        brakeAmount = 1f;
        boostAmount = Mathf.Clamp(boostAmount + amount, 1f, boostMax);
        Debug.Log("boosting at: " + boostAmount);
        currentFollowDist = followCamera.GetCinemachineComponent<Cinemachine.CinemachineTransposer>().m_FollowOffset.z;
        followCamera.GetCinemachineComponent<Cinemachine.CinemachineTransposer>().m_FollowOffset.z = Mathf.Clamp(currentFollowDist - amount, boostCameraDist, followCameraDist);
    }

    public void NormalizeSpeed()
    {
        returnCamera = true;
    }

    public void Brake(float amount)
    {
        returnCamera = false;
        boostAmount = 1f;
        brakeAmount = Mathf.Clamp(brakeAmount - amount, brakeMin, 1f);
        Debug.Log("braking at: " + brakeAmount);

        currentFollowDist = followCamera.GetCinemachineComponent<Cinemachine.CinemachineTransposer>().m_FollowOffset.z;
        followCamera.GetCinemachineComponent<Cinemachine.CinemachineTransposer>().m_FollowOffset.z = Mathf.Clamp(currentFollowDist + amount, followCameraDist, brakeCameraDist);
    }
}
