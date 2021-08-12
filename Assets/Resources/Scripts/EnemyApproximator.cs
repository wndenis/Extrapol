using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyApproximator : MonoBehaviour
{
    public Weapon weapon;
    public bool parabolicWeapon;
    private Transform observableTarget;
    private LineRenderer lineRenderer;

    private float t_delay = 0.005f;
    private float t_measure = 0.5f;
    private int n = 3;
    private float dt;

    private float bulletSpeed = 30f;
    private bool extrapolating;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void ApplyTarget(Transform target)
    {
        if (extrapolating)
            return;
        observableTarget = target;
        dt = t_measure / n;
        StartCoroutine(Extrapolate());
    }

    private void FixedUpdate()
    {
        if (observableTarget)
        {
            var rotation = Quaternion.LookRotation(observableTarget.position - weapon.tip.position);
            weapon.transform.rotation = Quaternion.Lerp(weapon.transform.rotation, rotation, 0.2f);
        }
    }

    private IEnumerator Extrapolate()
    {
        extrapolating = true;
        var tStart = Time.time;
        var t = 0f;
        var measuredPositions = new List<Vector3>();
        var measuredTime = new List<float>();
        yield return new WaitForSeconds(t_delay);

        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
        for (var ni = 0; ni < n; ni++)
        {
            
            measuredPositions.Add(observableTarget.position);
            measuredTime.Add(t);

            lineRenderer.positionCount = ni+1;
            lineRenderer.SetPosition(ni, observableTarget.position);
            yield return new WaitForSeconds(dt);
            t += dt;
        }
        
        var extrapolation = new Extrapolations.VectorExtrapolation(measuredTime, measuredPositions);
        var extrapolatedCount = n * 3;
        lineRenderer.positionCount = extrapolatedCount;
        for (var x = 0; x < extrapolatedCount; x++)
        {
            var tt = dt * (x + 1);
            var pos = extrapolation.Extrapolate(tt);
            lineRenderer.SetPosition(x, pos);
        }
        lineRenderer.startColor = new Color(0f, 0.23f, 1f);
        lineRenderer.endColor = new Color(1f, 0.25f, 0.24f);

        var tolerance = 0.0005f;
        var solved = false;
        var pointReducer = 0;
        lineRenderer.positionCount = 0;
        for (float tApprox = 0; tApprox < 1; tApprox += 0.0005f)
        {
            var newPos = extrapolation.Extrapolate(tApprox + t);
            if (!solved)
            {
                weapon.transform.rotation = Quaternion.LookRotation(newPos - weapon.tip.position);
                var dist = Vector3.Distance(newPos, weapon.tip.position);
                var timeToReach = dist / bulletSpeed;
                if (Math.Abs(timeToReach - tApprox) < tolerance)
                {
                    if (parabolicWeapon)
                        newPos -= (timeToReach * timeToReach) * Physics.gravity / 2;
                    weapon.transform.rotation = Quaternion.LookRotation(newPos - weapon.tip.position);
                    observableTarget = null;
                    //yield return null;
                    var b = weapon.Shoot();
                    var lr = b.GetComponent<LineRenderer>();
                    lr.positionCount = 2;
                    lr.SetPosition(0, b.position);
                    lr.SetPosition(1, newPos);
                    solved = true;
                }
            }

            pointReducer++;
            if (pointReducer != 20) continue;
            pointReducer = 0;
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, newPos);
        }
        
        
        yield return new WaitForSeconds(0.75f);
        lineRenderer.positionCount = 0;
        extrapolating = false;
    }
    
}
