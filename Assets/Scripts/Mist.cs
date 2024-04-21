using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirMist : MonoBehaviour
{
	public Sprite mistSprite;
	public int mistCount = 100;
	public float mistSize = 0.1f;
	public float mistSpeed = 0.5f;
	private List<GameObject> mistList;

	void Start()
	{
        Debug.Log("generating mist");
		// Generating mist
		mistList = new List<GameObject>();
		// Generating 100 mist objects
		for (int i = 0; i < mistCount; i++)
		{
			GameObject mist = new GameObject();
			mist.transform.parent = this.transform;
			mist.AddComponent<SpriteRenderer>();
			mist.GetComponent<SpriteRenderer>().sprite = mistSprite;
			mist.transform.position = transform.position + new Vector3(Random.Range(-4.75f, 4.75f), Random.Range(-5, 13), 1f);
			mist.transform.localScale = new Vector3(mistSize, mistSize, 1f);
			mist.transform.Rotate(new Vector3(0, 0, 1), Random.Range(0, 360));
			mist.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.5f);
			mistList.Add(mist);
		}
	}
	void Update()
	{
		// Scale it up and down
		for (int i = 0; i < mistCount; i++)
		{
			mistList[i].transform.localScale = new Vector3(0.1f + Mathf.Sin(Time.time * mistSpeed) * 0.05f, 0.1f + Mathf.Sin(Time.time * mistSpeed) * 0.05f, 1f);
		}
	}
}