using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] negativeDrops;

    public GameObject[] positiveDrops;

    public GameObject inverter;

    public int bottomHeight = 0;
    public bool spawn;
    public int rows = 10;
    public int columns;

    public int itemsSpawned;

    public int badItemsSpawned;

    private float negative_initialInterval = 10f; // Initial interval between debug logs
    private float negative_finalInterval = 1f; // Final interval between debug logs after 1 minute
    private float negative_timePassed = 0f; // Total time passed
    private float negative_currentInterval; // Current interval between debug logs

    private float positive_initialInterval = 10f; // Initial interval between debug logs
    private float positive_finalInterval = 2f; // Final interval between debug logs after 1 minute
    private float positive_timePassed = 0f; // Total time passed
    private float positive_currentInterval; // Current interval between debug logs


    public float positiveSpawnTime;

    public float goldSpawnTime;

    public float spawnVal;

    public float LastTop;

    public float LastInverter;

    public float goldTop;

    public float LastBadTop;

    public int spaceBetweenGoodSpawn;

    public int spaceBetweenInverters;
    public int spaceBetweenBadSpawn;
    public int spaceBetweenGoldSpawn;

    public float spawnerType;

    public MasterObject master;

    public bool spawnedInverter = false;






    //public Tuple<Transform, bool>[,] grid = new Tuple<Transform, bool>[10, 40];

    void Start()
    {
        master = GameObject.Find("master").GetComponent<MasterObject>();
        positiveSpawnTime = 0;
        goldSpawnTime = 0;
        LastTop = 0;
        LastBadTop = 0;
        goldTop = 0;
        StartCoroutine(negativeInterval());

        StartCoroutine(positiveInterval());
    }

    IEnumerator negativeInterval()
    {
        while (true) // Loop indefinitely
        {
            yield return new WaitForSeconds(negative_currentInterval);
            if (spawnerType == master.getInversion()){
                spawnNegative();
            }
            negative_timePassed += negative_currentInterval;
            if (negative_timePassed >= 120f) // Check if 60 seconds have passed
            {
                negative_timePassed = 0f; // Reset timePassed
                negative_currentInterval = negative_initialInterval; // Reset currentInterval
            }
            else
            {
                negative_currentInterval = Mathf.Lerp(negative_initialInterval, negative_finalInterval, negative_timePassed / 60f); // Update currentInterval
            }
        }
    }

    IEnumerator positiveInterval()
    {
        while (true) // Loop indefinitely
        {
            yield return new WaitForSeconds(positive_currentInterval);
            if (spawnerType == master.getInversion()){
                spawnGoldCoin();
            }
            positive_timePassed += positive_currentInterval;
            if (positive_timePassed >= 120f) // Check if 60 seconds have passed
            {
                positive_timePassed = 0f; // Reset timePassed
                positive_currentInterval = positive_initialInterval; // Reset currentInterval
            }
            else
            {
                positive_currentInterval = Mathf.Lerp(positive_initialInterval, positive_finalInterval, positive_timePassed / 60f); // Update currentInterval
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (spawnerType == master.getInversion()){
            if (!spawnedInverter){
                spawnInverter();
                spawnedInverter = true;
            }
        }
        else{
            spawnedInverter = false;
        }

    }


    void spawnGoldCoin(){
        Vector3 spawnLocation = new Vector3 (UnityEngine.Random.Range(0,rows), Mathf.RoundToInt(transform.position.y), transform.position.z);
        GameObject New = Instantiate(positiveDrops[0], spawnLocation, Quaternion.identity);
    }

    void spawnInverter(){
        Vector3 spawnLocation = new Vector3 (4.5f, Mathf.RoundToInt(transform.position.y), transform.position.z);
        GameObject New = Instantiate(inverter, spawnLocation, Quaternion.identity);
    }

    void spawnPositive(){
        Vector3 spawnLocation = new Vector3 (UnityEngine.Random.Range(0,rows), Mathf.RoundToInt(transform.position.y), transform.position.z);
        GameObject New = Instantiate(positiveDrops[UnityEngine.Random.Range(0,positiveDrops.Length)], spawnLocation, Quaternion.identity);
    }

    void spawnNegative(){
        Vector3 spawnLocation = new Vector3 (UnityEngine.Random.Range(0,rows), Mathf.RoundToInt(transform.position.y), transform.position.z);
        GameObject New = Instantiate(negativeDrops[UnityEngine.Random.Range(0,negativeDrops.Length)], spawnLocation, Quaternion.identity);
    }

}