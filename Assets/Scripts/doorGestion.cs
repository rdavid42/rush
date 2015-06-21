using UnityEngine;
using System.Collections;

public class doorGestion : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log (other.tag);
		Debug.Log (name);
		Transform door = gameObject.transform.parent;
		if (other.tag == "character" && name == "rightDoor")
		{
			Debug.Log(door.transform.localRotation.z);
			if (door.transform.localRotation.z <= 0.7)
			{
				Debug.Log (door.transform.localRotation.z);
				door.transform.Rotate(Vector3.forward * 10);
			}
		}
		if (other.tag == "character" && name == "leftDoor")
		{
			if (door.transform.localRotation.z >= -0.7)
				door.transform.Rotate (Vector3.forward * -10);
		}
	}
	
	void OnTriggerStay2D(Collider2D other)
	{
		Debug.Log (other.tag);

		Debug.Log (name);
		Transform door = gameObject.transform.parent;
		if (other.tag == "character" && name == "rightDoor")
		{
			if (door.transform.localRotation.z <= 0.7)
			{
				Debug.Log (other.transform.localRotation.z);
				door.transform.Rotate (Vector3.forward * 10);
			}
		}
		if (other.tag == "character" && name == "leftDoor")
		{
			if (door.transform.localRotation.z >= -0.7)
				door.transform.Rotate (Vector3.forward * -10);
		}
	}


	// Update is called once per frame
	void Update ()
	{
	
	}

}
