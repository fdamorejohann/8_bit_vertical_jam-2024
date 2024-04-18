using UnityEngine;
public class SideWall : MonoBehaviour
{
	public Transform playerTransform;
	void Update()
	{
		transform.position = new Vector3(transform.position.x, playerTransform.position.y, transform.position.z);
	}
}