using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEquipmentSlot : MonoBehaviour {
        
    public enum FireMode { PRIMARY, SECONDARY };
    public Weapon weapon;

    private void Awake()
    {
        weapon = GetComponentInChildren<Weapon>();
    }

    public void Fire(FireMode mode)
    {
        if (weapon != null)
        {
            //Debug.Log(name + " attempted to fire with mode: " + mode.ToString());
            switch (mode)
            {               
                case FireMode.SECONDARY:
                    break;
                case FireMode.PRIMARY:
                default:
                    weapon.PrimaryFire();
                    break;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (GetComponentInParent<WeaponLoadout>().reticle != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, GetComponentInParent<WeaponLoadout>().reticle.position);           
        }
    }


}
