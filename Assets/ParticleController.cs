using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public ParticleSystem ps;
    public GameObject water;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == water)
        {
            var emissionModule = ps.emission;
            emissionModule.enabled = true;
            ps.Emit(35);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == water)
        {
            var emissionModule = ps.emission;
            emissionModule.enabled = false;
            ps.Emit(10);
        }
    }
}
