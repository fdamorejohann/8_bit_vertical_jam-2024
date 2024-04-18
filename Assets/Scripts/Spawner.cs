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

    public float negativeSpawnTime;

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





    //public Tuple<Transform, bool>[,] grid = new Tuple<Transform, bool>[10, 40];

    void Start()
    {
        negativeSpawnTime = 0;
        positiveSpawnTime = 0;
        goldSpawnTime = 0;
        LastTop = 0;
        LastBadTop = 0;
        goldTop = 0;
    }


    // Update is called once per frame
    void Update()
    {

        if (transform.position.y / spaceBetweenGoldSpawn > goldTop){
            Debug.Log("SPAWNING GOLD COIN");
            spawnGoldCoin();
            itemsSpawned++;
            goldTop = transform.position.y / spaceBetweenGoldSpawn + 1;
        }

        if (transform.position.y / spaceBetweenGoodSpawn > LastTop){
            Debug.Log("SPAWNING ");
            spawnPositive();
            itemsSpawned++;
            LastTop = transform.position.y / spaceBetweenGoodSpawn  + 1;
        }

        if (transform.position.y / spaceBetweenInverters > LastInverter){
            Debug.Log("SPAWNING ");
            spaceBetweenInverters = spawnInverter();
            itemsSpawned++;
            LastInverter = transform.position.y / spaceBetweenInverters  + 1;
        }

        if (transform.position.y / spaceBetweenBadSpawn > LastBadTop){
            Debug.Log("SPAWNING Negative");
            spawnNegative();
            itemsSpawned++;
            LastBadTop = transform.position.y / spaceBetweenBadSpawn + 1;
        }


    }


    void spawnGoldCoin(){
        Vector3 spawnLocation = new Vector3 (UnityEngine.Random.Range(0,rows), Mathf.RoundToInt(transform.position.y), transform.position.z);
        GameObject New = Instantiate(positiveDrops[0], spawnLocation, Quaternion.identity);
    }

    int spawnInverter(){
        Vector3 spawnLocation = new Vector3 (4.5f, Mathf.RoundToInt(transform.position.y), transform.position.z);
        GameObject New = Instantiate(inverter, spawnLocation, Quaternion.identity);
        return UnityEngine.Random.Range(15,30);
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