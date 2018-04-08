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

    [Header("SpawnPoints")]
    [SerializeField]
    private GameObject[] enemyPrefabs;

    [Header("SpawnPoints")]
    [SerializeField]
    private Transform[] spawnPoints;

    [Header("PathToFollow")]
    [SerializeField]
    private GameObject[] PathToFollow;

    private bool isMultiplayer;
    private bool canStart;
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
            if (isMultiplayer)
            {
                GameObject go;
                go = Instantiate(enemyPrefabs[0], spawnPoints[Random.Range(0, spawnPoints.Length - 1)]);
                //go.GetComponent<EnemyBehaviourCs>().SetWaveCount ()
            }
            else if (!isMultiplayer)
            {
                //10 enemies 
                if (waveCount == 1 )
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
                //20 enemies 
                /*else if (waveCount == 2 )
                {
                    Transform[] tempTransformArray = PathToFollow[1].GetComponentsInChildren<Transform>();
                    GameObject go;
                    go = Instantiate(enemyPrefabs[waveCount], spawnPoints[Random.Range(0, spawnPoints.Length - 1)]);
                    go.GetComponent<EnemyBehaviourCs>().SetWaveCount(waveCount);
                }
                //30 enemies 
                else if (waveCount == 3)
                {
                    Transform[] tempTransformArray = PathToFollow[2].GetComponentsInChildren<Transform>();
                    GameObject go;
                    go = Instantiate(enemyPrefabs[waveCount], spawnPoints[Random.Range(0, spawnPoints.Length - 1)]);
                    go.GetComponent<EnemyBehaviourCs>().SetWaveCount(waveCount);
                }*/
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
