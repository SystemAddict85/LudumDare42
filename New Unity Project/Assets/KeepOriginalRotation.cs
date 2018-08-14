using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepOriginalRotation : MonoBehaviour {

    private Quaternion originalRotation;

    void Awake()
    {
        originalRotation = transform.rotation;
    }

    void LateUpdate()
    {
        transform.rotation = originalRotation;
    }
}
