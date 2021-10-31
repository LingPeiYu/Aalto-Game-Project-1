using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float time;

    private float timer;
    public float CurrentTimer { get => timer; }

    [SerializeField]
    private bool activateOnAwake;
    [SerializeField]
    private bool autoRestart;

    [SerializeField]
    private UnityEvent countZeroEvents;

    private void Awake()
    {
        if (activateOnAwake) timer = time;
        else timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        { 
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                countZeroEvents.Invoke();
                if (autoRestart) Restart();
            }
        }
    }

    public void Restart()
    {
        timer = time;
    }

    public void ActivateTimer(float time)
    {
        this.time = time;
        timer = time;
    }

    public bool IsZero()
    {
        return timer <= 0;
    }
}
