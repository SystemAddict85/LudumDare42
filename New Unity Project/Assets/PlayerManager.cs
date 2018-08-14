using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public PlayerController player;

    public static PlayerController Player { get; private set; }

    private void Awake()
    {        
        if(player == null)
        {
            player = GameObject.Find("Player").GetComponent<PlayerController>();
        }

        Player = player;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
