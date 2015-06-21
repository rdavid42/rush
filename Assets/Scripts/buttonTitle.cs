using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class buttonTitle : MonoBehaviour {
	
	public Text		name1;
	public Text		name2;
	public Text		name3;
	public Text 	name4;
	private int		selected;
	private bool	minus;
	// Use this for initialization
	void Start () {
		selected = 0;
		StartCoroutine(move());
	}
	
	//
	IEnumerator	move()
	{
		while (true)
		{
			if (name1.fontSize < 60 && !minus)
			{
				name1.fontSize += 1;
				name2.fontSize += 1;
				name3.fontSize += 1;
				name4.fontSize += 1;
				if (name1.fontSize == 60)
					minus = true;
			}
			if 	(name1.fontSize > 40 && minus)
			{
				name1.fontSize -= 1;
				name2.fontSize -= 1;
				name3.fontSize -= 1;
				name4.fontSize -= 1;
				if (name1.fontSize == 40)
					minus = false;
			}
			yield return new WaitForSeconds(0.02f);
		}
	}

	public void		StartGame()
	{
		Application.LoadLevel("level00");
	}

	public void		ExitGame()
	{
		Application.Quit();
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.UpArrow))
		{
			selected += 1;
			selected %= 2;
			if (selected == 0)
			{
				name1.text = "P L A Y";
				name3.text = "P L A Y";
				name3.fontStyle = FontStyle.Bold;
				name1.fontStyle = FontStyle.Bold;
				name2.fontStyle = FontStyle.Normal;
				name4.fontStyle = FontStyle.Normal;
				name2.text = "EXIT";
				name4.text = "EXIT";
			}
			else 
			{	
				name1.text = "PLAY";
				name3.text = "PLAY";
				name1.fontStyle = FontStyle.Normal;
				name3.fontStyle = FontStyle.Normal;
				name2.fontStyle = FontStyle.Bold;
				name4.fontStyle = FontStyle.Bold;
				name2.text = "E X I T";
				name4.text = "E X I T";
			}
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (selected == 0)
				StartGame ();
			else
				ExitGame();
		}
	}
}