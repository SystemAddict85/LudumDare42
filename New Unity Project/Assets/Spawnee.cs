using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnee : MonoBehaviour {

    private float counter;
    private bool readyToLook = true;

    private void Update()
    {
        if (readyToLook)
        {
            StartCoroutine(WaitToLook());
            if (Vector3.Distance(transform.position, PlayerManager.Player.transform.position) > 150f)
            {
                SphereSpawn.SpawnCounter--;
                Destroy(transform.gameObject);
            }

        }
    }

    private IEnumerator WaitToLook()
    {
        readyToLook = false;
        yield return new WaitForSeconds(10f);
        readyToLook = true;
    }
}
