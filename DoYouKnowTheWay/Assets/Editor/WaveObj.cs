using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Object for each rounds WaveInfo
/// </summary>
[System.Serializable]
public class WaveObj  {

    public string cName; //what is this enemies name
    public GameObject cEnemy; //what enemy in this wave
    public GameObject cSpawn; //where does this enemy spawn?
    public int cEnemyCount; //how many enemies in this wave
    public float cCadence; //what is the spawn delay between enemies in this wave
    public float cDelay; //delay until next wave

    /// <summary>
    /// Object for holding a waves info
    /// </summary>
    /// <param name="WaveCount">Total waves</param>
    /// <param name="cEnemy">Current Enemy</param>
    /// <param name="cEnemyCount">How many of current enemy will spawn in this wave</param>
    /// <param name="cCadence">What is the delay between spawns</param>
    /// <param name="cDelay">What is the delay after this wave before the next begins</param>
    public WaveObj(string cName, GameObject cEnemy, GameObject cSpawn, int cEnemyCount, float cCadence, float cDelay)
    {
        this.cName = cName;
        this.cEnemy = cEnemy;
        this.cSpawn = cSpawn;
        this.cEnemyCount = cEnemyCount;
        this.cCadence = cCadence;
        this.cDelay = cDelay;
    }
    public WaveObj()
    { }
}
