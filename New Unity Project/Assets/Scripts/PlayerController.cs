using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
           
    ThrusterBooster booster;

    [SerializeField]
    private float noTurn = 0.1f; // Extent of the no-turn zone as a fraction of Screen.height;
    [SerializeField]
    private float factor = 150.0f;
    private Vector3 center;

    public Transform billboardTarget;
    [SerializeField]
    private float rollSpeed = 1f;

    private void Awake()
    {
        booster = GetComponent<ThrusterBooster>();
        center = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        if (!billboardTarget) { Debug.LogError("Need to assign billboard"); }
    }
    void SetCursorState()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;        
    }
    
    // Update is called once per frame
    void Update () {
        CheckForInput();
	}

    private void CheckForInput()
    {        
        CheckForMovement();
    }

    private void CheckForMovement()
    {
        //FlyingDirection();
        if (Input.GetButton("Boost") && booster.CanBoost)
        {
            booster.OnBoost();
        }
        else if (Input.GetButton("Brake"))
        {
            booster.OnBrake();
        }
        else
        {
            booster.OnNormalThrust();
        }

        if (Input.GetButton("FirePrimary"))
        {
            GetComponentInChildren<WeaponLoadout>().PrimaryFire();
        }

        CameraLook();
    }
    private void CameraLook()
    {
        var delta = (Input.mousePosition - center) / Screen.height;

        var roll = 0f;

        if (Input.GetButton("RollRight"))
        {
            roll =  -rollSpeed * Time.deltaTime;
        }
        else if (Input.GetButton("RollLeft"))
        {
            roll = rollSpeed * Time.deltaTime;
        }

        transform.Rotate(0, 0, roll);

        if (delta.y > noTurn)
            transform.Rotate(-(delta.y - noTurn) * Time.deltaTime * factor, 0, roll);
        if (delta.y < -noTurn)
            transform.Rotate(-(delta.y + noTurn) * Time.deltaTime * factor, 0, roll);
        if (delta.x > noTurn)
            transform.Rotate(0, (delta.x - noTurn) * Time.deltaTime * factor, roll);
        if (delta.x < -noTurn)
            transform.Rotate(0, (delta.x + noTurn) * Time.deltaTime * factor, roll);
        

        
    }
}
