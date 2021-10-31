using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Timer))]
public class Weapon : MonoBehaviour
{
    [SerializeField]
    protected float coolDownTime;
    [SerializeField]
    protected UnityEvent fireEvents;

    protected Timer timer;

    public virtual void Fire()
    {
        if (timer == null) timer = GetComponent<Timer>();
        timer.ActivateTimer(coolDownTime);

        fireEvents.Invoke();
    }

    public virtual bool IsCool()
    {
        if (timer == null) timer = GetComponent<Timer>();
        return timer.IsZero();
    }
}
