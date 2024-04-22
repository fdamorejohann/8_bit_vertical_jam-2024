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

    public float negative_initialInterval = 10f; // Initial interval between debug logs
    public float negative_finalInterval = 2f; // Final interval between debug logs after 1 minute
    public float negative_timePassed = 0f; // Total time passed
    public float negative_currentInterval; // Current interval between debug logs

    public float positive_initialInterval = 5; // Initial interval between debug logs
    public float positive_finalInterval = 2f; // Final interval between debug logs after 1 minute
    public float positive_timePassed = 0f; // Total time passed
    public float positive_currentInterval; // Current interval between debug logs


    public float inverter_initialInterval = 10f; // Initial interval between debug logs
    public float inverter_finalInterval = 5f; // Final interval between debug logs after 1 minute
    public float inverter_timePassed = 0f; // Total time passed
    public float inverter_currentInterval; // Current interval between debug logs


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

        StartCoroutine(inverterInterval());
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
            if (negative_timePassed >= 60f) // Check if 60 seconds have passed
            {
                negative_currentInterval = negative_finalInterval; // Reset currentInterval
            }
            else
            {
                negative_currentInterval = Mathf.Lerp(negative_initialInterval, negative_finalInterval, negative_timePassed / 60f); // Update currentInterval
            }
        }
    }

    IEnumerator inverterInterval()
    {
        while (true) // Loop indefinitely
        {
            yield return new WaitForSeconds(inverter_currentInterval);
            if (spawnerType == master.getInversion() && !spawnedInverter){
                spawnInverter();
                spawnedInverter = true;
            }
            inverter_timePassed += inverter_currentInterval;
            if (inverter_timePassed >= 120f) // Check if 60 seconds have passed
            {
                inverter_timePassed = 0f; // Reset timePassed
                inverter_currentInterval = inverter_finalInterval; // Reset currentInterval
            }
            else
            {
                inverter_currentInterval = Mathf.Lerp(inverter_initialInterval, inverter_finalInterval, inverter_timePassed / 120f); // Update currentInterval
            }
            if (spawnerType != master.getInversion()){
                spawnedInverter = false;
                yield return new WaitForSeconds(1);
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
            if (positive_timePassed >= 60f) // Check if 60 seconds have passed
            {
                positive_timePassed = 0f; // Reset timePassed
                positive_currentInterval = positive_finalInterval; // Reset currentInterval
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

        // if (spawnerType == master.getInversion()){
        //     if (!spawnedInverter){
        //         spawnInverter();
        //         spawnedInverter = true;
        //     }
        // }
        // else{
        //     spawnedInverter = false;
        // }

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