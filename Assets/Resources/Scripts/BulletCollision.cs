using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public Transform particles;

    private void OnCollisionEnter(Collision other)
    {
        var p = Instantiate(particles);
        p.position = transform.position;
    }
}
