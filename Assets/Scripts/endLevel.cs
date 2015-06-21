using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class endLevel : MonoBehaviour {

	public enemy		enemy1;
	public enemy		enemy2;
	public enemy		enemy3;
	public Text			text1;
	public Text			text2;
	public AudioSource	win;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (enemy1.dead == true && enemy2.dead == true && enemy3.dead == true)
		{
			text1.gameObject.SetActive(true);
			text2.gameObject.SetActive(true);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.name == "player")
		{
			win.Play ();
			Application.LoadLevel ("menu");
		}
	}
}
