using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecayObject : MonoBehaviour {
        
    public float decayTime = 2f;
    public bool decayOnStart = false;
    public bool destroyParent = false;

    private void Start()
    {
        if (decayOnStart)
        {
            var go = destroyParent ? transform.parent.gameObject : gameObject;
            Destroy(go, decayTime);
        }
    }

    public void StartDecay()
    {
        var go = destroyParent ? transform.parent.gameObject : gameObject;
        Destroy(go, decayTime);
    }
}
