using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform tip;
    public GameObject bullet;
    [Space]

    public float bulletImpulse;

    [Space] 
    public Transform shootParticles;
    public Transform bulletCollisionParticles;
    
    // Start is called before the first frame update
    public Transform Shoot()
    {
        var b = Instantiate(bullet).GetComponent<Rigidbody>();
        b.transform.position = tip.position;
        b.AddForce(tip.forward * bulletImpulse, ForceMode.Impulse);
        Destroy(b.GetComponent<LineRenderer>(), 0.75f);
        Destroy(b.gameObject, 5f);
        if (shootParticles)
        {
            var p = Instantiate(shootParticles);
            p.position = tip.position;
            p.rotation = tip.rotation;
        }

        if (bulletCollisionParticles)
        {
            var c = b.gameObject.AddComponent<BulletCollision>();
            c.particles = bulletCollisionParticles;
        }

        return b.transform;
    }
}
