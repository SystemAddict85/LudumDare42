using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardSprite : MonoBehaviour {
        
    [SerializeField]
    private float damping = 2f;    
	
	// Update is called once per frame
	void Update () {
        //transform.LookAt(PlayerManager.Player.billboardTarget);
        var lookPos = PlayerManager.Player.billboardTarget.position - transform.position;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping );
    }
}
