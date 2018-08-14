using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public bool destroyOnImpact = false;
    public float damage = 1f;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit: " + collision.gameObject.name);
        if (destroyOnImpact)
            Destroy(gameObject);
    }
}
