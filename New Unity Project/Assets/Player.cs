using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Shield shield;

    public static System.Action<float> OnDamageReceived = delegate { };
    private bool isInvulnerable = false;
    [SerializeField]
    private float invulnerableTime = 1f;

    public void Awake()
    {
        OnDamageReceived += TakeDamage;
        shield = GetComponent<Shield>();
    }

    void TakeDamage(float damage)
    {
        if (!isInvulnerable)
        {
            StartCoroutine(Invulnerability());
            shield.TakeShieldDamage(damage);
            AudioManager.Instance.GlobalSounds.PlaySound("PlayerHurt");
            FlashScreenUI.Instance.FlashScreen(FlashScreenUI.FlashColor.RED, 0.5f);
        }
    }

    internal void Death()
    {
        Debug.Log("You have died");
    }

    private IEnumerator Invulnerability()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerableTime);
        isInvulnerable = false;
    }
}
