using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    ForwardThruster thruster;    

    [SerializeField]
    private float noTurn = 0.1f; // Extent of the no-turn zone as a fraction of Screen.height;
    [SerializeField]
    private float factor = 150.0f;
    private Vector3 center;

    [SerializeField]
    CursorLockMode wantedMode;


    private void Awake()
    {
        thruster = GetComponent<ForwardThruster>();
        center = new Vector3(Screen.width / 2, Screen.height / 2, 0);
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
        if (Input.GetButton("Boost"))
        {
            thruster.Boost(.05f);
        }
        else if (Input.GetButton("Brake"))
        {
            thruster.Brake(.05f);
        }
        else
        {
            thruster.NormalizeSpeed();
        }
        CameraLook();
    }
    private void CameraLook()
    {
        var delta = (Input.mousePosition - center) / Screen.height;
        Debug.Log(delta);
        if (delta.y > noTurn)
            transform.Rotate(-(delta.y - noTurn) * Time.deltaTime * factor, 0, 0);
        if (delta.y < -noTurn)
            transform.Rotate(-(delta.y + noTurn) * Time.deltaTime * factor, 0, 0);
        if (delta.x > noTurn)
            transform.Rotate(0, (delta.x - noTurn) * Time.deltaTime * factor, 0);
        if (delta.x < -noTurn)
            transform.Rotate(0, (delta.x + noTurn) * Time.deltaTime * factor, 0);
    }
}
