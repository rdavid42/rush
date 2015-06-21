using UnityEngine;
using System.Collections;

public class shootTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "character")
		{
			gameObject.GetComponentsInParent<enemy>()[0].SendMessage("shoot", collider.gameObject.transform.position);
		}
	}

	void OnTriggerStay2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "character")
		{
			gameObject.GetComponentsInParent<enemy>()[0].SendMessage("shoot", collider.gameObject.transform.position);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
