using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissileIndicater : MonoBehaviour
{
    [SerializeField]
    private int redCount = 3;

    private MissileController missileController;
    private MissileController PlayerMissileController
    {
        get
        {
            if (missileController == null) missileController = GameManager._instance.Player.GetComponentInChildren<MissileController>();
            return missileController;
        }
    }

    private Text missileCount;

    private Color normalColor;

    private void Start()
    {
        missileCount = GetComponent<Text>();
        normalColor = missileCount.color;
    }

    private void Update()
    {
        missileCount.text = PlayerMissileController.CurrentMissleCount + "";
        if (PlayerMissileController.CurrentMissleCount <= redCount) missileCount.color = Color.red;
        else missileCount.color = normalColor;
    }
}
