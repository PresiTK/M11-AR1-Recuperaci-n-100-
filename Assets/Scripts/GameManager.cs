using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    private float EnemyAmount = 10;
    private int EnemyCount = 0;
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private Transform[] SpawnPoints;
    public List<GameObject> EnemiesList = new List<GameObject>();
    private void Awake()
    {
        //Lo hago instanciable
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (EnemiesList.Count <= 0)
        {

            for (int i = 0; i <= EnemyCount; i++)
            {
                Transform spawnPos = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
                GameObject enemy = Instantiate(EnemyPrefab, spawnPos);
                EnemiesList.Add(enemy);
            }
            
            EnemyAmount = EnemyAmount * 1.2f;
            EnemyCount = (int) EnemyAmount;
        }
    }

}
