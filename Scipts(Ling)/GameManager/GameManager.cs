using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    [Header("Global Single Instances")]

    #region Player
    private PlayerController player;
    public PlayerController Player
    {
        get
        {
            if (player == null) player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            return player;
        }
    }
    public Health PlayerHealth
    {
        get
        {
            if (player == null) player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            return player.GetComponent<Health>();
        }
    }
    #endregion

    #region Object Pool
    [System.Serializable]
    private struct ObjectPoolContainer
    {
        public string name;
        public ObjectPool objectPool;
    }

    [Header("Object Pools")]
    [SerializeField]
    private List<ObjectPoolContainer> objectPoolContainers;

    public ObjectPool GetObjectPool(string poolName)
    {
        foreach (ObjectPoolContainer container in objectPoolContainers)
        {
            if (container.name == poolName) return container.objectPool;
        }
        return null;
    }
    #endregion

    #region Target Selecter
    [System.Serializable]
    private struct TargetSelecterContainer
    {
        public string name;
        public TargetSelecter targetSelecter;
    }

    [Header("Target Selecter")]
    [SerializeField]
    private List<TargetSelecterContainer> targetSelecterContainers;

    public TargetSelecter GetTargetSelecter(string selecterName)
    {
        foreach (TargetSelecterContainer container in targetSelecterContainers)
        {
            if (container.name == selecterName) return container.targetSelecter;
        }
        return null;
    }
    #endregion

    #region level manage
    [Header("Level")]
    [SerializeField]
    private int maxWave;
    private int curWave;
    public int CurWave
    {
        set
        {
            if (value > curWave) curWave = value;
        }
    }

    [SerializeField]
    [Tooltip("if the count of enemies is lower than this number, spawn next wave")]
    private int triggerSpawnCount;

    private List<AircraftSpawner>[] aircraftSpawners;
    private List<ObjectPoolUnit> enemyUnits;
    public List<ObjectPoolUnit> EnemyUnits { get => enemyUnits; }
    public void RecordSpawner(AircraftSpawner aircraftSpawner)
    {
        if (aircraftSpawners == null) aircraftSpawners = new List<AircraftSpawner>[maxWave];
        if (aircraftSpawners[aircraftSpawner.WaveNo - 1] == null) aircraftSpawners[aircraftSpawner.WaveNo - 1] = new List<AircraftSpawner>();
        aircraftSpawners[aircraftSpawner.WaveNo - 1].Add(aircraftSpawner);
    }
    public void RecordEnemy(ObjectPoolUnit enemyUnit)
    {
        if (enemyUnits == null) enemyUnits = new List<ObjectPoolUnit>();
        enemyUnits.Add(enemyUnit);
    }

    [SerializeField]
    private Timer countDown;
    public Timer CountDown { get => countDown; }
    [SerializeField]
    private float levelTime;

    private bool LevelOver()
    {
        bool over = true;
        if (aircraftSpawners != null)
        {
            for (int i = 0; i < aircraftSpawners.Length; i++)
                foreach (AircraftSpawner sp in aircraftSpawners[i])
                    if (sp.Active) over = false;
        }
        else
            over = false;
        if (enemyUnits != null)
        {
            foreach (ObjectPoolUnit enemy in enemyUnits)
                if (enemy.Active) over = false;
        }
        else
            over = false;

        return over;
    }

    [SerializeField]
    private UnityEvent LevelOverEvents;
    public void AddLevelOverListener(UnityAction listener)
    {
        LevelOverEvents.AddListener(listener);
    }

    [SerializeField]
    private UnityEvent PlayerFailEvents;
    public void AddPlayerDeathListener(UnityAction listener)
    {
        PlayerFailEvents.AddListener(listener);
    }

    private bool levelClosed;
    public bool LevelClosed { get => levelClosed; }

    //Level Process
    public int EnemyCount()
    {
        if (aircraftSpawners == null) return 0;
        int sum = 0;
        for (int i = 0; i < aircraftSpawners.Length; i++) sum += aircraftSpawners[i].Count;
        return sum;
    }
    private int ActiveEnemyCount()
    {
        if (enemyUnits == null) return 0;
        int i = 0;
        foreach (ObjectPoolUnit enemy in enemyUnits)
            if (enemy.Active) i++;
        return i;
    }
    public int DeadEnemyCount()
    {
        int i = 0;
        if (enemyUnits != null)
            foreach (ObjectPoolUnit enemy in enemyUnits)
                if (!enemy.Active) i++;
        return i;
    }

    public void KillAllEnemy()
    {
        if (enemyUnits != null)
            foreach (ObjectPoolUnit enemy in enemyUnits)
                if (enemy.Active) enemy.Deactivate();
    }
    #endregion

    private void Awake()
    {
        if (_instance == null) _instance = this;
        levelClosed = false;
    }

    private void Start()
    {
        countDown.ActivateTimer(levelTime);
    }

    private void Update()
    {
        //level manage
        if (ActiveEnemyCount() <= triggerSpawnCount && curWave < maxWave)
        {
            curWave++;
            foreach (AircraftSpawner sp in aircraftSpawners[curWave - 1])
                sp.Spawn();
        }

        if (LevelOver() && !levelClosed)
        {
            levelClosed = true;
            LevelOverEvents.Invoke();
        }
        if ((PlayerHealth.IsDead() || countDown.IsZero()) && !levelClosed)
        {
            levelClosed = true;
            PlayerFailEvents.Invoke();
        }
    }
}
