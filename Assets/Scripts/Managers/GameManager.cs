using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform Player { get; private set; }
    [SerializeField] private string playerTag;
    public ObjectPool ObjectPool { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;

        Player = GameObject.FindGameObjectWithTag(playerTag).transform;
        ObjectPool = GameObject.FindAnyObjectByType<ObjectPool>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
