using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour {

    private Vector3 startingPosition;
    private bool readyToLook;

    private void Awake()
    {
        startingPosition = transform.position;
    }

    
	
	// Update is called once per frame
	void Update () {
        if (readyToLook)
        {
            StartCoroutine(WaitToLook());
            RaycastHit hit;

            
        }	
	}

    private IEnumerator WaitToLook()
    {
        readyToLook = false;
        yield return new WaitForSeconds(.2f);
        readyToLook = true;
    }
}
