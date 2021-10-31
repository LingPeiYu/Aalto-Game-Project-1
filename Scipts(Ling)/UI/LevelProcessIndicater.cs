using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProcessIndicater : MonoBehaviour
{
    private Image LevelProcess;

    private void Start()
    {
        LevelProcess = GetComponent<Image>();
    }

    private void Update()
    {
        LevelProcess.fillAmount = 1 - (float)GameManager._instance.DeadEnemyCount() / (float)GameManager._instance.EnemyCount();
        //Debug.Log(GameManager._instance.EnemyCount());
    }
}
