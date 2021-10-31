using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootline : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private LineRenderer lineRender;
    [SerializeField]
    private float visibleTime;

    private float timer;

    private void Start()
    {
        timer = 0f;
    }

    private void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0) StopVisualRay();
        }
    }
    public void VisualRayToTarget()
    {
        lineRender.SetPosition(1, target.localPosition);
        timer = visibleTime;
    }

    private void StopVisualRay()
    {
        lineRender.SetPosition(1, Vector3.zero);
    }
}
