using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [HideInInspector]
    public ObjectPoolUnit target;
    [SerializeField]
    private float speed;
    [SerializeField]
    private ObjectPoolUnit unit;
    [SerializeField]
    private LayerMask layers;

    private void FixedUpdate()
    {
        if (target != null && target.Active)
        {
            transform.LookAt(target.transform);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else
            transform.position += transform.forward * speed * Time.deltaTime;
    }
    private void LateUpdate()
    {
        if (target != null && !target.Active) target = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if((layers.value&(int)Mathf.Pow(2,other.gameObject.layer))==(int)Mathf.Pow(2,other.gameObject.layer))
        {
            unit.Deactivate();
            this.enabled = false;
        }
    }

}
