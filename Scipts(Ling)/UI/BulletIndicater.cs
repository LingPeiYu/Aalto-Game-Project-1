using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletIndicater : MonoBehaviour
{
    [SerializeField]
    private int redCount = 10;

    private Color normalColor;

    private Gun gun;
    private Gun PlayerGun
    {
        get
        {
            if (gun == null) gun = GameManager._instance.Player.GetComponentInChildren<Gun>();
            return gun;
        }
    }

    private Text bulletCount;

    private void Start()
    {
        bulletCount = GetComponent<Text>();
        normalColor = bulletCount.color;
    }

    private void Update()
    {
        bulletCount.text = PlayerGun.CurrentBulletCount+"";
        if (PlayerGun.CurrentBulletCount <= redCount) bulletCount.color = Color.red;
        else bulletCount.color = normalColor;
    }
}
