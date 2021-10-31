using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public static BGM _instance;

    [SerializeField]
    private bool playerExist;

    private FMODUnity.StudioEventEmitter emitter;

    private Health playerHealth;

    private void Start()
    {
        emitter = GetComponent<FMODUnity.StudioEventEmitter>();

        if (BGM._instance == null||emitter.Event!=BGM._instance.emitter.Event)
        {
            if (BGM._instance != null) Destroy(BGM._instance.gameObject);
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (!playerExist) return;
        if (playerHealth == null) playerHealth = GameManager._instance.PlayerHealth;
        else
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("PlayerHealth", playerHealth.CurrentHealthPoint);
        }
    }
}
