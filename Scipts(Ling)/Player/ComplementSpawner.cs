using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplementSpawner : MonoBehaviour
{

    [SerializeField]
    private string ObjectPoolName;
    [SerializeField]
    private float randomWidthRange;
    [SerializeField]
    private float randomLengthRange;


    public void Spawn()
    {
        ObjectPool pool = GameManager._instance.GetObjectPool(ObjectPoolName);
        Vector3 spPos = transform.position + Random.Range(-randomWidthRange / 2, randomWidthRange / 2) * transform.forward + Random.Range(-randomLengthRange / 2, randomLengthRange / 2) * transform.right;
        pool.InitiateFromObjectPool(spPos, transform.rotation);
    }
}
