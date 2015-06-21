using UnityEngine;
using System.Collections;

public class ammo : MonoBehaviour {

	public float			destroyTime;
	// Use this for initialization
	void Start () {
		Destroy(gameObject, destroyTime);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "enemy")
		{
			enemy e = collider.gameObject.GetComponent<enemy>();
			if (!e.dead)
			{
				e.die();
			}
		}
		if (collider.gameObject.tag == "character")
		{
			playerGestion p = collider.gameObject.GetComponent<playerGestion>();
			if (p && !p.dead)
			{
				p.die();
			}
		}
		GameObject.Destroy(gameObject);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
