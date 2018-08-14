using UnityEngine;

[RequireComponent(typeof(ForwardThruster))]
public class ThrusterCameraControl : MonoBehaviour
{
    [SerializeField]
    private float followCameraDistance = -2.75f;
    [SerializeField]
    private float boostCameraDistance = -3.5f;
    [SerializeField]
    private float brakeCameraDistance = -1.5f;

    [SerializeField]
    private bool returnCamera = true;

    private float cameraStep = 0.5f;


    private float currentFollowDist;

    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera followCamera;

    [SerializeField]
    private float cameraReturnSpeed = 0.1f;

    private void Awake()
    {
        var thruster = GetComponent<ForwardThruster>();
        thruster.booster.OnBoost += CameraBoost;
        thruster.booster.OnBrake += CameraBrake;
        thruster.booster.OnNormalThrust += ReturnCamera;

        followCamera = CameraManager.Instance.playerFollow;
    }

    private void Update()
    {
        if (returnCamera)
        {
            currentFollowDist = followCamera.GetCinemachineComponent<Cinemachine.CinemachineTransposer>().m_FollowOffset.z;
            followCamera.GetCinemachineComponent<Cinemachine.CinemachineTransposer>().m_FollowOffset.z = Mathf.Lerp(currentFollowDist, followCameraDistance, cameraReturnSpeed);
            if (Mathf.Abs(currentFollowDist - followCameraDistance) < 0.01f)
            {
                returnCamera = false;
            }
        }    
    }

    private void CameraBoost()
    {
        returnCamera = false;
        currentFollowDist = followCamera.GetCinemachineComponent<Cinemachine.CinemachineTransposer>().m_FollowOffset.z;
        followCamera.GetCinemachineComponent<Cinemachine.CinemachineTransposer>().m_FollowOffset.z = Mathf.Clamp(currentFollowDist - cameraStep, boostCameraDistance, followCameraDistance);
    }
    private void CameraBrake()
    {
        returnCamera = false;
        currentFollowDist = followCamera.GetCinemachineComponent<Cinemachine.CinemachineTransposer>().m_FollowOffset.z;
        followCamera.GetCinemachineComponent<Cinemachine.CinemachineTransposer>().m_FollowOffset.z = Mathf.Clamp(currentFollowDist + cameraStep, followCameraDistance, brakeCameraDistance);
    }
    private void ReturnCamera()
    {
        returnCamera = true;
    }

}