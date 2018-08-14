using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class ForwardThruster : MonoBehaviour
{

    private Movement move;
    public bool isEngineOn = true;   

    public ThrusterBooster booster;
   
    public System.Action OnEngineOn = delegate { };
    public System.Action OnEngineOff = delegate { };

   

    private void Awake()
    {
        booster = GetComponent<ThrusterBooster>();       
        move = GetComponent<Movement>();
    }

    private void Start()
    {
        OnEngineOff += TurnOffEngine;
        OnEngineOn += TurnOnEngine;
    }    

    // Update is called once per frame
    void Update()
    {
        if (isEngineOn)
        {
            if (booster)
            {
                move.Move(transform.forward, booster.boostAmount * booster.brakeAmount);
            }
            else
            {
                move.Move(transform.forward, 1f);
            }
            
        }
    }

    
    private void TurnOffEngine()
    {
        isEngineOn = false;
    }

    private void TurnOnEngine()
    {
        isEngineOn = true;
    }
}
