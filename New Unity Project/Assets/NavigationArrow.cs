using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationArrow : MonoBehaviour {

    [SerializeField]
    private Transform target;    


    public void SetTarget(Transform targ)
    {
        target = targ;
    }
    private void LateUpdate()
    {
        if (target) {
            var dir = target.position - PlayerManager.Player.transform.position;
            
            transform.rotation = Quaternion.LookRotation(dir, Vector3.up);

        }
    }

   
}
