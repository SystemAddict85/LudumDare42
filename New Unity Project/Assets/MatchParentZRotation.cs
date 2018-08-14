using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used to match the parents rotation to prevent spinning billboards
public class MatchParentZRotation : MonoBehaviour {

    private Quaternion originalRotation;

    void Awake()
    {
        originalRotation = transform.parent.rotation;
    }
	
	// Update is called once per frame
	void LateUpdate () {

        var rotation = transform.rotation;
        rotation.z = originalRotation.z;
        //rotation.w = originalRotation.w;

        transform.rotation = rotation;
	}
}
