using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLoadout : MonoBehaviour {

    List<Weapon> weaponLoadout = new List<Weapon>();
    public WeaponEquipmentSlot[] equipmentSlots;

    public Transform reticle;

	void Awake()
    {
        var slots = new List<WeaponEquipmentSlot>(equipmentSlots);
        foreach (var s in slots)
        {
            if(s.weapon != null)
            {
                weaponLoadout.Add(s.weapon);
            }
        }

    }

    public void PrimaryFire()
    {
        foreach(var s in equipmentSlots)
        {
            s.Fire(WeaponEquipmentSlot.FireMode.PRIMARY);
        }
    }    


}
