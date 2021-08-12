using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerShoot : MonoBehaviour
{
    public Weapon weapon;
    public float shootingDelay;
    [Space] public PostProcessVolume postProcessVolume;

    [Space] public EnemyApproximator enemy;
    
    
    private float lastShootTime;

    
    void Start()
    {
        lastShootTime = -shootingDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.unscaledTime - lastShootTime > shootingDelay)
            if (Input.GetMouseButtonDown(0))
            {
                lastShootTime = Time.unscaledTime;
                var b = weapon.Shoot();
                enemy.ApplyTarget(b);
            }

        //var axis = Input.GetAxis("Mouse ScrollWheel");
        //if (!(Math.Abs(axis) > 0)) return;
        
        //Time.timeScale = Mathf.Clamp(Time.timeScale + 0.4f * Mathf.Sign(axis), 0.075f, 1f);
        //postProcessVolume.weight = 1 - (Time.timeScale - 0.075f) / (1 - 0.075f);
    }

}
