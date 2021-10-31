using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAim : MonoBehaviour
{
    public bool isAiming;

    [SerializeField]
    private Vector3 fixedRotation;

    private void Update()
    {
        if (isAiming)
        {
            transform.LookAt(GameManager._instance.Player.transform.position);
            //fix rotation
            transform.Rotate(fixedRotation);
        }
        else
        {
            transform.forward = transform.parent.forward;
            //fix rotation
            transform.Rotate(fixedRotation);
        }
    }
}
