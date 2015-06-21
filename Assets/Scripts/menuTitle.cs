using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class menuTitle : MonoBehaviour {

	public Text		name1;
	public Text		name2;
	private bool	minus;
	// Use this for initialization
	void Start () {
	StartCoroutine(move());
	}

//
	IEnumerator	move()
	{
		while (true)
		{
			if (name1.fontSize < 60 && !minus)
			{
				name1.transform.rotation= new Quaternion(name1.transform.localRotation.x, name1.transform.localRotation.y, name1.transform.localRotation.z + 0.005f, name1.transform.localRotation.w);
				name2.transform.rotation = new Quaternion(name2.transform.localRotation.x, name2.transform.localRotation.y, name2.transform.localRotation.z + 0.005f, name2.transform.localRotation.w);
				name1.fontSize += 1;
				name2.fontSize += 1;
				if (name1.fontSize == 60)
					minus = true;
			}
			if 	(name1.fontSize > 40 && minus)
			{
				name1.transform.rotation = new Quaternion(name1.transform.localRotation.x, name1.transform.localRotation.y, name1.transform.localRotation.z - 0.005f,name1.transform.localRotation.w);
				name2.transform.rotation = new Quaternion(name2.transform.localRotation.x, name2.transform.localRotation.y, name2.transform.localRotation.z - 0.005f,name2.transform.localRotation.w);

				name1.fontSize -= 1;
				name2.fontSize -= 1;
				if (name1.fontSize == 40)
					minus = false;
			}
			yield return new WaitForSeconds(0.04f);
		}
	}
////		while(true)
////		{
////
////			name.transform.Rotate (Vector3.forward * -10);
////			yield return new WaitForSeconds(0.1f);
////	
	/// // Update is called once per frame
	void Update ()
	{
	}
}
