using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    private float currentShield;
    [SerializeField]
    private float maxShield = 5f;

    [SerializeField]
    private float timeBeforeRecharge;
    private float currentTime = 0f;
    private bool isWaitingToCharge = false;
    private bool isRecharging = false;
    [SerializeField]
    private float chargingStep;
    private bool isShieldBroke;

    private void Awake()
    {
        currentShield = maxShield;
    }

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        if (isWaitingToCharge)
        {
            currentTime += Time.deltaTime;

            if(currentTime >= timeBeforeRecharge)
            {
                isWaitingToCharge = false;
                isRecharging = true;
            }
        }
        if (isRecharging)
        {
            currentShield += Time.deltaTime * chargingStep;
            if(currentShield >= maxShield)
            {
                isRecharging = false;
                isShieldBroke = false;
                currentShield = maxShield;
            }
            UpdateUI();
        }
    }

    public void TakeShieldDamage(float damage)
    {
       
            if (isShieldBroke)
            {
                GetComponent<Player>().Death();
            }
            currentTime = 0f;
            isRecharging = false;
            isWaitingToCharge = true;
            currentShield -= damage;

            if (currentShield <= 0)
            {
                isShieldBroke = true;
            }

            UpdateUI();
    }

    private void UpdateUI()
    {
        UIManager.Instance.shieldBar.fillAmount = currentShield / maxShield;
    }

   

}