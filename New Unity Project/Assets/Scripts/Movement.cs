using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float baseSpeed = 1f;
    public bool canMove = true;    

    public void Move ( Vector3 direction, float speedMultiplier = 1f )
    {
        if (canMove)
        {
            transform.position += direction * speedMultiplier * baseSpeed * Time.deltaTime;
        }
    }
}
