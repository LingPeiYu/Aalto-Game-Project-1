using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyIndicater : MonoBehaviour
{
    [SerializeField]
    private float highlightScaleFactor;
    [SerializeField]
    private string enemyPoolName;
    [SerializeField]
    private Text countText;

    private ObjectPool pool;
    private Vector3 normalScale;

    private void Start()
    {
        normalScale = transform.localScale;
    }

    private void Update()
    {
        if (pool == null) pool = GameManager._instance.GetObjectPool(enemyPoolName);

        int activeUnitNumber = 0;
        foreach(ObjectPoolUnit unit in pool.Units)
        {
            if (unit.Active) activeUnitNumber++;
        }
        if (activeUnitNumber > 0) transform.localScale = normalScale * highlightScaleFactor;
        else transform.localScale = normalScale;
        countText.text = activeUnitNumber + "";
    }
}
