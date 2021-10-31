using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFlySystem : MonoBehaviour
{
    [SerializeField]
    private float maxMoveSpeed;
    [SerializeField]
    private float maxRotateSpeed;

    private bool rotateLeftFirst;

    public void RandomCycleDirection()
    {
        rotateLeftFirst = Random.Range(-1f, 1f) >= 0f ? false : true;
    }

    public void MoveForward()
    {
        transform.position += transform.forward * maxMoveSpeed * Time.deltaTime;
    }

    public void Rotate(Vector3 targetDirection)
    {
        float factor = Vector3.Dot(targetDirection.normalized, transform.forward.normalized);factor = 0.5f + (factor + 1) / 4;
        int direction;
        if (rotateLeftFirst) direction = (Vector3.Dot(targetDirection.normalized, transform.right.normalized) >= 0.1f) ? 1 : -1;
        else direction = (Vector3.Dot(targetDirection.normalized, transform.right.normalized) >= -0.1f) ? 1 : -1;
        transform.Rotate(transform.up, Time.deltaTime * maxRotateSpeed * factor * direction);
    }
}
