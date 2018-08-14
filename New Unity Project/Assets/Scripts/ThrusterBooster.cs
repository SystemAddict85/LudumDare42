using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(ForwardThruster))]
public class ThrusterBooster : MonoBehaviour
{       
    public float boostAmount = 1f;
    [SerializeField]
    private float boostMax = 2f;

    public float brakeAmount = 1f;
    [SerializeField]
    private float brakeMin = .5f;

    public System.Action OnBoost = delegate { };
    public System.Action OnBrake = delegate { };
    public System.Action OnNormalThrust = delegate { };

    private AudioSource boostAudio;
    [SerializeField]
    private AudioClip boostLoop;
    [SerializeField]
    private AudioClip brakeLoop;

    private float currentBoostRemaining;
    [SerializeField]
    private float maxBoostQty = 5f;
    private float boostDrain = 1f;

    private float currentTime = 0f;
    private bool isWaitingToCharge = false;
    private bool isRecharging = false;
    [SerializeField]
    private float chargingStep = 1f;
    private bool isBoosting;
    private bool isBoostDepleted;

    public bool CanBoost { get; private set; }

    private void Awake()
    {        
        boostAudio = GetComponent<AudioSource>();
        currentBoostRemaining = maxBoostQty;
    }

    private void Start()
    {
        OnBoost += Boost;
        OnBrake += Brake;
        OnNormalThrust += NormalizeSpeed;
        CanBoost = true;
    }

    private void Update()
    {
        if (isBoosting)
        {
            currentBoostRemaining -= boostDrain * Time.deltaTime;
            if(currentBoostRemaining <= .25f * maxBoostQty)
            {
                isBoostDepleted = true;
                ChangeBarColor(new Color32(255, 0, 0, 255));
            }
            if(currentBoostRemaining <= 0)
            {
                currentBoostRemaining = 0f;
                CanBoost = false;
            }
            UpdateUI();
        } else if (currentBoostRemaining < maxBoostQty)
        {
            if (isBoostDepleted)
                CanBoost = false;

            currentBoostRemaining += chargingStep * Time.deltaTime;
            if(currentBoostRemaining >= maxBoostQty)
            {
                currentBoostRemaining = maxBoostQty;
                if (isBoostDepleted)
                {
                    isBoostDepleted = false;
                    CanBoost = true;
                    ChangeBarColor(new Color32(255, 255, 255, 255));
                }
            }
            UpdateUI();
        }
    }



    private void Boost()
    {
        isBoosting = true;
        if (!boostAudio.isPlaying || boostAudio.clip != boostLoop)
        {
            boostAudio.Stop();
            boostAudio.clip = boostLoop;
            boostAudio.Play();
        }

        var amount = .05f;
        brakeAmount = 1f;
        boostAmount = Mathf.Clamp(boostAmount + amount, 1f, boostMax);
    }

    private void Brake()
    {
        isBoosting = false;
        if (!boostAudio.isPlaying || boostAudio.clip != brakeLoop)
        {
            boostAudio.Stop();
            boostAudio.clip = brakeLoop;
            boostAudio.Play();
        }

        boostAudio.Stop();
        var amount = .05f;
        boostAmount = 1f;
        brakeAmount = Mathf.Clamp(brakeAmount - amount, brakeMin, 1f);
    }

    private void NormalizeSpeed()
    {
        isBoosting = false;
        boostAudio.Stop();
        boostAmount = 1f;
        brakeAmount = 1f;
    }

    private void UpdateUI()
    {
        UIManager.Instance.boostBar.fillAmount = currentBoostRemaining / maxBoostQty;
    }

    private void ChangeBarColor(Color32 color)
    {
        UIManager.Instance.boostBar.color = color;
    }

}