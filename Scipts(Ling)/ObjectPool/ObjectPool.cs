using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject unitObject;
    [SerializeField]
    private int size;
    private List<ObjectPoolUnit> units;
    public List<ObjectPoolUnit> Units { get => units; }

    public ObjectPoolUnit InitiateFromObjectPool(Vector3 position, Quaternion rotation, Transform partent = null)
    {
        if (units.Count >= size) return null;
        if (units.Count > 0)
            foreach (ObjectPoolUnit unit in units)
            {
                if (!unit.Active)
                {
                    unit.transform.position = position;
                    unit.transform.rotation = rotation;
                    unit.transform.parent = partent;
                    unit.Activate();
                    return unit;
                }
            }
        ObjectPoolUnit newUnit = Instantiate(unitObject, position, rotation, partent).GetComponent<ObjectPoolUnit>();
        units.Add(newUnit);
        return newUnit;
    }

    private void Recycle(ObjectPoolUnit deactiveUnit)
    {
        deactiveUnit.transform.parent = transform;
        deactiveUnit.transform.position = transform.position;
    }

    private void Start()
    {
        units = new List<ObjectPoolUnit>();
    }

    private void Update()
    {
        if (units.Count > 0)
            foreach (ObjectPoolUnit unit in units)
            {
                if (!unit.Active) Recycle(unit);
            }
    }
}
