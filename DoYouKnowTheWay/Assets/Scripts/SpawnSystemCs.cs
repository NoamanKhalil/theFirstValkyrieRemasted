using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystemCs : MonoBehaviour
{
    #region Game Settings
    [Header("WaveCount")]
    [SerializeField]
    private int waveCount;
    [SerializeField]
    private float waveDelay;
    [SerializeField]
    private int enemyCount;
    [SerializeField]
    private float enemyDelay;

    [Header("SpawnPoints")]
    [SerializeField]
    private GameObject[] enemyPrefabs;

    [Header("SpawnPoints")]
    [SerializeField]
    private Transform[] spawnPoints;

    [Header("PathToFollow")]
    [SerializeField]
    private GameObject[] PathToFollow;

    [SerializeField]
    private bool isMultiplayer;
    [SerializeField]
    private bool canStart;
    [SerializeField]
    private int maxEnemyCount;
    private GameManager gm;
    #endregion

    // Use this for initialization
    void Start ()
    {
		gm = GetComponent<GameManager>();
        maxEnemyCount = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (canStart)
        {
            enemyDelay -= Time.deltaTime;
            if (enemyDelay>0)
            {
                if (isMultiplayer && enemyCount <= maxEnemyCount)
                {
                    GameObject go;
                    go = Instantiate(enemyPrefabs[0], spawnPoints[spawnPoints.Length-1]);
                    go.GetComponent<EnemyBehaviourCs>().SetWaveCount(0);
                    enemyCount++;

                    if (enemyCount >= maxEnemyCount)
                    {
                        //waveCount++;
                        maxEnemyCount += 10;
                        Debug.Log("WaveOver");
                    }
                   // enemyDelay = 2f;
                    //go.GetComponent<EnemyBehaviourCs>().SetWaveCount ()
                }
                else if (!isMultiplayer)
                {
                    //10 enemies 
                    if (waveCount == 1)
                    {
                        if (enemyCount == maxEnemyCount)
                        {
                            return;
                        }

                        for (int i = 1; i <= enemyCount; i++)
                        {
                            GameObject go;
                            go = Instantiate(enemyPrefabs[0], spawnPoints[Random.Range(0, spawnPoints.Length - 1)]);
                            go.GetComponent<EnemyBehaviourCs>().SetWaveCount(waveCount);
                            enemyCount++;
                        }
                        if (enemyCount == maxEnemyCount)
                        {
                            waveCount++;
                            maxEnemyCount += 10;
                            Debug.Log("WaveOver");
                        }

                    }

                }

            }
        }
    
	}

    public void isMultiplayerSet()
    {
        isMultiplayer = true;
        canStart = true;
        Debug.Log("Multplayer set in spawn");
    }
    public void isNotMultplayerSet()
    {
        isMultiplayer = false;
        canStart = true;
        Debug.Log("!Multplayer set in spawn");
    }
}
