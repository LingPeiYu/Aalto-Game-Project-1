using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterFlySystem : BasicFlySystem
{
    [SerializeField]
    private float verticalMoveSpeed;

    private float waveDegree = 0f;

    public void WaveMove(float addDegree)
    {
        waveDegree += addDegree;
        transform.position += transform.up * Mathf.Sin(waveDegree) * verticalMoveSpeed * Time.deltaTime;
    }

    public void VerticalMove(bool up)
    {
        if(up)
            transform.position += transform.up * verticalMoveSpeed * Time.deltaTime;
        else
            transform.position -= transform.up * verticalMoveSpeed * Time.deltaTime;
    }
}
