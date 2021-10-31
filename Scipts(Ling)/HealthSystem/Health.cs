using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField]
    private float maxHealthPoint;
    public float MaxHealthPoint { get => maxHealthPoint; }

    private float currentHealthPoint;
    public float CurrentHealthPoint { get => currentHealthPoint; }

    [Header("Events")]
    [SerializeField]
    private UnityEvent AddHealthPointEvents;
    [SerializeField]
    private UnityEvent ReduceHealthPointEvents;
    [SerializeField]
    private UnityEvent DeathEvents;

    private void Start()
    {
        currentHealthPoint = maxHealthPoint;
    }

    public void AddHealthPoint(float num)
    {
        currentHealthPoint += num;
        AddHealthPointEvents.Invoke();
        if (currentHealthPoint > maxHealthPoint) currentHealthPoint = maxHealthPoint;
    }

    public void ReduceHealthPoint(float num)
    {
        currentHealthPoint -= num;
        ReduceHealthPointEvents.Invoke();
        if (currentHealthPoint <= 0)
        {
            currentHealthPoint = 0f;
            DeathEvents.Invoke();
        }
    }

    public bool IsDead()
    {
        return currentHealthPoint <= 0;
    }
}
