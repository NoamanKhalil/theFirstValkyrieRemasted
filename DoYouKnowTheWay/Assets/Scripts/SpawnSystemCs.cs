using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField]
    private GameObject myWaveCount;

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
    private bool arePlayersConnected;
    [SerializeField]
    private int maxEnemyCount;
    private GameManager gm;
    private PhotonNetworkManagerCs pm;
    private int playerCount;
    #endregion

    // Use this for initialization
    void Start ()
    {
		gm = GetComponent<GameManager>();
        pm = GetComponent<PhotonNetworkManagerCs>();
        maxEnemyCount = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMultiplayer)
        {
            myWaveCount.GetComponent<Text>().text = "Wave :" + waveCount + "/5";
        }
        else if (!isMultiplayer)
        {
            myWaveCount.GetComponent<Text>().text = "Wave :" + waveCount + "/5";
        }
        
        if (canStart)
        {
            enemyDelay -= Time.deltaTime;
            if (enemyDelay <= 0 )
            {
                //&& playerCount == 2
                if (isMultiplayer && enemyCount <= maxEnemyCount && playerCount == 2 && enemyCount <= 2)
                {
                   /* Instantiate(enemyPrefabs[0], spawnPoints[4].position, Quaternion.Euler (0,0,-90));
                    Instantiate(enemyPrefabs[1], spawnPoints[3].position , Quaternion.Euler (0,0,90));
                    enemyCount++;

                    if (enemyCount >= maxEnemyCount)
                    {
                        //waveCount++;
                        maxEnemyCount += 10;
                        Debug.Log("WaveOver");
                    }
                    enemyDelay = 4f;
                    //go.GetComponent<EnemyBehaviourCs>().SetWaveCount ()*/
                }
                else if (!isMultiplayer)
                {
                    //10 enemies 
                    if (waveCount == 1)
                    {
                        if (enemyCount >= maxEnemyCount)
                        {
                            return;
                        }

                       if ( enemyCount <= maxEnemyCount )
                        { 
                            GameObject go;
                            go = Instantiate(enemyPrefabs[2], spawnPoints[Random.Range(0, spawnPoints.Length - 2)]);
                            go.GetComponent<EnemyBehaviourCs>().waveCount = 1;
                            enemyCount++;
                            enemyDelay = 7.5f;
                        }

                        if (enemyCount >= maxEnemyCount)
                        {
                            waveCount++;
                            maxEnemyCount += 5;
                           // Debug.Log("WaveOver");
                        }

                    }
                    else if (waveCount ==2 )
                    {
                        //Debug.Log("Wave 2 reached ");
                        if (enemyCount >= maxEnemyCount)
                        {
                            return;
                        }

                        if (enemyCount <= maxEnemyCount)
                        {
                            GameObject go;
                            go = Instantiate(enemyPrefabs[3], spawnPoints[Random.Range(0, spawnPoints.Length - 2)]);
                            go.GetComponent<EnemyBehaviourCs>().waveCount = 2;
                            enemyCount++;
                            enemyDelay = 7f;
                        }
                        if (enemyCount >= maxEnemyCount)
                        {
                            waveCount++;
                            maxEnemyCount += 10;
                            //Debug.Log("WaveOver");
                        }
                    }
                    else if (waveCount==3)
                    {
                        //Debug.Log("Wave 3 reached ");
                        if (enemyCount >= maxEnemyCount)
                        {
                            return;
                        }

                        if (enemyCount <= maxEnemyCount)
                        {
                            GameObject go;
                            go = Instantiate(enemyPrefabs[4], spawnPoints[Random.Range(0, spawnPoints.Length - 2)]);
                            go.GetComponent<EnemyBehaviourCs>().waveCount = 3;
                            enemyCount++;
                            enemyDelay = 6.5f;
                        }
                        if (enemyCount >= maxEnemyCount)
                        {
                            waveCount++;
                            maxEnemyCount += 5;
                            //Debug.Log("WaveOver");
                        }
                    }
                    else if (waveCount == 4)
                    {
                        //Debug.Log("Wave 4 reached ");
                        if (enemyCount >= maxEnemyCount)
                        {
                            return;
                        }

                        if (enemyCount <= maxEnemyCount)
                        {
                            GameObject go;
                            go = Instantiate(enemyPrefabs[5], spawnPoints[Random.Range(0, spawnPoints.Length - 2)]);
                            go.GetComponent<EnemyBehaviourCs>().waveCount = 3;
                            enemyCount++;
                            enemyDelay = 6f;
                        }
                        if (enemyCount >= maxEnemyCount)
                        {
                            waveCount++;
                            maxEnemyCount -=5;
                            //Debug.Log("WaveOver");
                        }
                    }
                    else if (waveCount == 5)
                    {
                        Debug.Log("Wave 5 reached ");
                        if (enemyCount >= maxEnemyCount)
                        {
                            return;
                        }

                        if (enemyCount <= maxEnemyCount)
                        {
                            GameObject go;
                            go = Instantiate(enemyPrefabs[6], spawnPoints[Random.Range(0, spawnPoints.Length - 2)]);
                            go.GetComponent<EnemyBehaviourCs>().waveCount = 3;
                            enemyCount++;
                            enemyDelay = 6f;
                        }
                        if (enemyCount >= maxEnemyCount)
                        {
                            gm.OnplayerWin();
                            //Debug.Log("WaveOver");
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
        //Debug.Log("Multplayer set in spawn");
    }
    public void isNotMultplayerSet()
    {
        isMultiplayer = false;
        canStart = true;
        //Debug.Log("!Multplayer set in spawn");
    }
    public void PlayerCountIncrease ()
    {
        playerCount++;
    }
}