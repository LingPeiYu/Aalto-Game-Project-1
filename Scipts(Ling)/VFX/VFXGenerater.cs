using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXGenerater : MonoBehaviour
{
    [SerializeField]
    private string poolName;

    public void GenerateVFX()
    {
        GameManager._instance.GetObjectPool(poolName).InitiateFromObjectPool(transform.position, transform.rotation);
    }
}
