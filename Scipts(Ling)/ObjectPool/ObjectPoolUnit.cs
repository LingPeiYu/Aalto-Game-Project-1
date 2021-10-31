using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectPoolUnit : MonoBehaviour
{
    [SerializeField]
    private UnityEvent activateEvents;

    private bool active = true;
    public bool Active { get => active; }

    private void Start()
    {
        Activate();
    }

    public void Activate()
    {
        active = true;
        activateEvents.Invoke();
    }

    public void Deactivate()
    {
        active = false;
    }
}
