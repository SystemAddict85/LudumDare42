using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ForwardThruster), typeof(ThrusterBooster))]
public class ThrusterParticleControl : MonoBehaviour {

    public ParticleSystem engineParticles;

    private float originalParticleSpeed;

    private void Awake()
    {
        originalParticleSpeed = engineParticles.main.simulationSpeed;

        var booster = GetComponent<ThrusterBooster>();

        booster.OnBoost += ParticleBoost;
        booster.OnBrake += ParticleBrake;
        booster.OnNormalThrust += NormalizeParticle;

        var thruster = GetComponent<ForwardThruster>();
        thruster.OnEngineOn += TurnOnParticles;
        thruster.OnEngineOff += TurnOffParticles;
    }
    
    public void ParticleBoost () {
        var main = engineParticles.main;
        main.simulationSpeed = 2f * originalParticleSpeed;
	}
    public void ParticleBrake()
    {
        var main = engineParticles.main;
        main.simulationSpeed = 0.5f * originalParticleSpeed;
    }
    public void NormalizeParticle()
    {
        var main = engineParticles.main;
        main.simulationSpeed = originalParticleSpeed;
    }

    public void TurnOffParticles()
    {
        var emission = engineParticles.emission;
        emission.enabled = false;
    }

    public void TurnOnParticles()
    {
        var emission = engineParticles.emission;
        emission.enabled = true;
    }
}
