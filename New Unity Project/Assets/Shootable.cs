using UnityEngine;

public class Shootable : MonoBehaviour
{
    public float totalLife = 2f;
    public int cashAmount = 10;

    [SerializeField]
    protected string destructionSoundName = "Explosion";
    [SerializeField]
    protected string gotHitSoundName = "ExplodeHit";

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Projectile>())
        {
            totalLife -= collision.gameObject.GetComponent<Projectile>().damage;
            var spawnPos = collision.contacts[0].point;

            var explosion = Instantiate(Resources.Load("VFX/Explosion"), spawnPos, transform.rotation) as GameObject;
            AudioManager.Instance.GlobalSounds.PlaySound(gotHitSoundName);
            explosion.transform.parent = transform;
            CheckForDestruction();
        }
        if (collision.gameObject.GetComponentInParent<PlayerController>())
        {
            Debug.Log("Collided with player");
            Player.OnDamageReceived(1f);
        }
    }

    protected virtual void CheckForDestruction()
    {
        if (totalLife <= 0f)
        {
            SphereSpawn.SpawnCounter--;
            Cash.Instance.UpdateCash(cashAmount);
            GetComponent<Collider>().enabled = false;
            GetComponent<Animator>().SetTrigger("destroy");
            AudioManager.Instance.GlobalSounds.PlaySound(destructionSoundName);
        }
    }
   
}