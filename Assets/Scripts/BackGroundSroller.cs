using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundSroller : MonoBehaviour
{

    public Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += new Vector3 (0,-.0001f, 0);

    }
}
