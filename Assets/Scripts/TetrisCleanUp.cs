using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisCleanUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Check if the object has no children
        if (transform.childCount == 0)
        {
            // If it has no children, destroy the object
            Destroy(gameObject);
        }

    }
}
