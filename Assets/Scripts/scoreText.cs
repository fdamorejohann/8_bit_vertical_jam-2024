using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreText : MonoBehaviour
{

    public GameObject [] scores;

    public GameObject currentScoreObject;

    public int currentScore = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void incrementScore(int n){
        if (currentScoreObject != null){
            Destroy(currentScoreObject);
        }
        currentScore = n;
        currentScoreObject = Instantiate(scores[n], transform.position,Quaternion.identity);;
        currentScoreObject.transform.parent = transform;
        currentScoreObject.transform.localPosition = new Vector3 (0,0,0);

    }
}
