/*using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WaveEnumerator))]
public class WaveDesigner : Editor
{

    public bool[] WaveCount;
    WaveEnumerator myLevelEnumerator;
    // Use this for initialization
    void OnEnable()
    {
        myLevelEnumerator = (WaveEnumerator)target;
        WaveCount = new bool[1];
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Enter the level's wave info below:");

        //how many waves
        myLevelEnumerator.numOfWaves = EditorGUILayout.IntField("Number of Waves:", myLevelEnumerator.numOfWaves);

        //lets adjust all our sizes to legal options
        AdjustListSizes();

        for (int i = 0; i < myLevelEnumerator.WaveInfo.Count; i++)
        {
            //lets create a foldout for each wave
            WaveCount[i] = EditorGUILayout.Foldout(WaveCount[i], "Wave #" + i.ToString());
            if (WaveCount[i])
            {
                var listRef = myLevelEnumerator.WaveInfo[i]; //too lazy to type this each time

                //each waves info is below
                listRef.cName = EditorGUILayout.TextField("Name", listRef.cName);
                listRef.cEnemy = (GameObject)EditorGUILayout.ObjectField("Prefab Enemy: ", listRef.cEnemy, typeof(GameObject), true);
                listRef.cSpawn = (GameObject)EditorGUILayout.ObjectField("Spawn Point: ", listRef.cSpawn, typeof(GameObject), true);
                listRef.cEnemyCount = EditorGUILayout.IntField("Amount to Spawn: ", listRef.cEnemyCount);
                listRef.cCadence = EditorGUILayout.FloatField("Delay between spawns :", listRef.cCadence);
                listRef.cDelay = EditorGUILayout.FloatField("Delay until spawning starts", listRef.cDelay);
            }
        }
    }

    //lets make sure all our lists are the proper sizes
    private void AdjustListSizes()
    {
        //leve must contain at least one wave
        if (myLevelEnumerator.numOfWaves <= 0) { myLevelEnumerator.numOfWaves = 1; }

        //is our container bool large enough?
        if (myLevelEnumerator.numOfWaves != WaveCount.Length)
        { WaveCount = new bool[myLevelEnumerator.numOfWaves]; }

        //has a list been constructed? Avoid null error first time script attachment
        if (myLevelEnumerator.WaveInfo == null)
        { myLevelEnumerator.WaveInfo = new List<WaveStruct>(); }

        //is our list not large enough?
        while (myLevelEnumerator.numOfWaves > myLevelEnumerator.WaveInfo.Count)
        { myLevelEnumerator.WaveInfo.Add(new WaveStruct()); }

        //is our list too large? as items have been removed
        if (myLevelEnumerator.WaveInfo.Count > myLevelEnumerator.numOfWaves)
        {
            myLevelEnumerator.WaveInfo.RemoveRange((myLevelEnumerator.numOfWaves - 1), ((myLevelEnumerator.WaveInfo.Count - 1) - (myLevelEnumerator.numOfWaves - 1)));
        }
    }
}*/