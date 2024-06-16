using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum UpgradeOption
{
    MaxHealth = 0,
    AttackPower,
    Speed,
    Knockback,
    AttackDelay,
    NumberOfProjectiles,
    COUNT 
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject Player { get; private set; }
    public ObjectPool ObjectPool { get; private set; }

    [Header("Wave")]
    [SerializeField] private int currentWaveIndex = 0;
    public int CurrentWaveIndex => currentWaveIndex;
    public event Action OnWaveChanged;

    [Header("CoinController")]
    [SerializeField] private CoinController coin;
    public CoinController Coin => coin;


    [SerializeField] private CharacterStat defaultStats;
    [SerializeField] private CharacterStat rangedStats;

    public ParticleSystem EffectParticle;
    private int currentSpawnCount = 0;
    private int waveSpawnCount = 0;
    private int waveSpawnPosCount = 0;

    public float spawnInterval = .5f;
    public List<GameObject> enemyPrefebs = new List<GameObject>();

    [SerializeField] private Transform spawnPositionsRoot;
    private List<Transform> spawnPositions = new List<Transform>();

    [SerializeField] private List<GameObject> rewards = new List<GameObject>();


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;

        Player = GameObject.FindGameObjectWithTag(Define.playerTag);

        ObjectPool = GameObject.FindAnyObjectByType<ObjectPool>();

        for (int i = 0; i < spawnPositionsRoot.childCount; i++)
        {
            spawnPositions.Add(spawnPositionsRoot.GetChild(i));
        }
    }

    private void Start()
    {
        UpgradeStatInit();
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        while (true)
        {
            if (currentSpawnCount == 0)
            {
                OnWaveChanged?.Invoke();
                yield return new WaitForSeconds(2f);

                ProcessWaveConditions();

                yield return StartCoroutine(SpawnEnemiesInWave());

                currentWaveIndex++;
            }

            yield return null;
        }
    }

    void ProcessWaveConditions()
    {
        if (currentWaveIndex % 20 == 0)
        {
            RandomUpgrade();
        }

        if (currentWaveIndex % 10 == 0)
        {
            IncreaseSpawnPositions();
        }

        if (currentWaveIndex % 5 == 0)
        {
            CreateReward();
        }

        if (currentWaveIndex % 3 == 0)
        {
            IncreaseWaveSpawnCount();
        }
    }

    IEnumerator SpawnEnemiesInWave()
    {
        for (int i = 0; i < waveSpawnPosCount; i++)
        {
            int posIdx = UnityEngine.Random.Range(0, spawnPositions.Count);
            for (int j = 0; j < waveSpawnCount; j++)
            {
                SpawnEnemyAtPosition(posIdx);
                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }

    void SpawnEnemyAtPosition(int posIdx)
    {
        int prefabIdx = UnityEngine.Random.Range(0, enemyPrefebs.Count);
        GameObject enemy = Instantiate(enemyPrefebs[prefabIdx], spawnPositions[posIdx].position, Quaternion.identity);

        enemy.GetComponent<CharacterStatHandler>().AddStatModifier(defaultStats);
        enemy.GetComponent<CharacterStatHandler>().AddStatModifier(rangedStats);

        enemy.GetComponent<HealthSystem>().OnDeath += OnEnemyDeath;
        currentSpawnCount++;
    }

    void IncreaseSpawnPositions()
    {
        waveSpawnPosCount = waveSpawnPosCount + 1 > spawnPositions.Count ? waveSpawnPosCount : waveSpawnPosCount + 1;
        waveSpawnCount = 0;
    }

    void IncreaseWaveSpawnCount()
    {
        waveSpawnCount += 1;
    }

    void UpgradeStatInit()
    {
        defaultStats.statsChangeType = StatsChangeType.Add;
        defaultStats.attackSO = Instantiate(defaultStats.attackSO);

        rangedStats.statsChangeType = StatsChangeType.Add;
        rangedStats.attackSO = Instantiate(rangedStats.attackSO);
    }

    private void RandomUpgrade()
    {
        UpgradeOption option = (UpgradeOption)UnityEngine.Random.Range(0, (int)UpgradeOption.COUNT);
        switch (option)
        {
            case UpgradeOption.MaxHealth:
                defaultStats.maxHealth += 2;
                break;

            case UpgradeOption.AttackPower:
                defaultStats.attackSO.power += 1;
                break;

            case UpgradeOption.Speed:
                defaultStats.speed += 0.1f;
                break;

            case UpgradeOption.Knockback:
                defaultStats.attackSO.isOnKnockBack = true;
                defaultStats.attackSO.knockbackPower += 1;
                defaultStats.attackSO.knockbackTime = 0.1f;
                break;

            case UpgradeOption.AttackDelay:
                defaultStats.attackSO.delay -= 0.05f;
                break;

            case UpgradeOption.NumberOfProjectiles:
                RangedAttackSO rangedAttackData = rangedStats.attackSO as RangedAttackSO;
                if (rangedAttackData != null) rangedAttackData.numberOfProjectilesPerShot += 1;
                break;

            default:
                break;
        }
    }

    private void CreateReward()
    {
        // 5단계마다 리워드 얻음
        int selectedRewardIndex = UnityEngine.Random.Range(0, rewards.Count);
        int randomPositionIndex = UnityEngine.Random.Range(0, spawnPositions.Count);

        GameObject obj = rewards[selectedRewardIndex];
        Instantiate(obj, spawnPositions[randomPositionIndex].position, Quaternion.identity);
    }

    private void OnEnemyDeath()
    {
        currentSpawnCount--;
    }
}
