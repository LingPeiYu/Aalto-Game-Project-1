using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ComplementBox : MonoBehaviour
{
    [System.Serializable]
    private enum ComplementType
    {
        Bullet,
        Missile,
        Health
    }
    [SerializeField]
    private ComplementType complementType;

    [SerializeField]
    private int addedBulletNum;
    [SerializeField]
    private int addedMissileNum;
    [SerializeField]
    private int addedHealthNum;

    [SerializeField]
    private UnityEvent complementEvent;

    public void Complement()
    {
        Debug.Log("complement");
        switch(complementType)
        {
            case ComplementType.Bullet:
                GameManager._instance.Player.GetComponentInChildren<Gun>().AddBullet(addedBulletNum);
                break;
            case ComplementType.Missile:
                GameManager._instance.Player.GetComponentInChildren<MissileController>().AddMissile(addedMissileNum);
                break;
            case ComplementType.Health:
                GameManager._instance.Player.GetComponent<Health>().AddHealthPoint(addedHealthNum);
                break;
        }
        complementEvent.Invoke();
    }
}
