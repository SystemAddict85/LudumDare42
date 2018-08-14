using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float weaponDamage;
    public int ammoCapacity;
    public int currentAmmo;
    public float fireRate;
    public float projectileDuration;
    public Vector3 weaponSpawnOffset = Vector3.zero;

    [SerializeField]
    protected string firingSoundName = "Laser";

    protected bool readyToFire = true;

    [SerializeField]
    protected GameObject ammoPrefab;

    public virtual Projectile PrimaryFire()
    {
        if (readyToFire)
        {
            StartCoroutine(WaitToFireAgain());
            var spawnPos = transform.TransformPoint(weaponSpawnOffset);
            var ammo = Instantiate(ammoPrefab, spawnPos, transform.rotation).GetComponent<Projectile>();

            var decay = ammo.GetComponent<DecayObject>();
            decay.decayTime = projectileDuration;
            decay.StartDecay();

            ammo.damage = weaponDamage;

            AudioManager.Instance.GlobalSounds.PlaySound(firingSoundName);

            return ammo;
        }
        else
        {
            return null;
        }
    }

    protected IEnumerator WaitToFireAgain()
    {
        readyToFire = false;
        yield return new WaitForSeconds(fireRate);
        readyToFire = true;
    }
}