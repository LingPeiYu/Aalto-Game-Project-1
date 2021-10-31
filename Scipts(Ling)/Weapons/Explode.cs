using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField]
    private string explosionPoolName;
    [SerializeField]
    private LayerMask layers;

    private Rigidbody rb;
    private ObjectPoolUnit thisUnit;
    private ObjectPool explosionObjectPool;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        Check();
    }

    private void FixedUpdate()
    {
        Check();
    }

    private void Check()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, rb.velocity.magnitude * Time.deltaTime, layers))
        {
                //Debug.Log(hit.collider.name);
                if (explosionObjectPool == null) explosionObjectPool = GameManager._instance.GetObjectPool(explosionPoolName);
                explosionObjectPool.InitiateFromObjectPool(transform.position, transform.rotation);

                if (thisUnit == null) thisUnit = GetComponent<ObjectPoolUnit>();
                thisUnit.Deactivate();
        }
    }
}
