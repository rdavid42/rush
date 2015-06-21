using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour
{
	public AudioSource		aDeath;
	public bool				dead;
	private Rigidbody2D		rbody;
	public bool				followPlayer;
	public weapon			currentWeapon;
	public AudioSource		aWeaponSound;
	public playerGestion	player;
	private float			elapsedTime;
	// Use this for initialization
	void Start()
	{
		rbody = gameObject.GetComponent<Rigidbody2D>();
	}

	public void die()
	{
		aDeath.Play();
		StartCoroutine(blink());
		dead = true;
		followPlayer = false;
		elapsedTime = 0.0f;
	}

	IEnumerator blink()
	{
		int				i;
		
		for (i = 0; i < 10; ++i)
		{
			yield return new WaitForSeconds(0.1f);
			foreach (Transform tr in gameObject.GetComponentInChildren<Transform>())
				tr.gameObject.SetActive(!tr.gameObject.activeSelf);
		}
		GameObject.Destroy(gameObject);
	}
	
	private Vector3 getDirection(Vector3 pos)
	{
		pos.x = pos.x - transform.position.x;
		pos.y = pos.y - transform.position.y;
		return (pos);
	}
	
	private float getAngle(Vector3 pos)
	{
		Vector3 dir = getDirection(pos);
		float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) + 90;
		return (angle);
	}
	
	public void rotate(Vector3 pos)
	{
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, getAngle(pos)));
	}

	public void follow(Vector3 pos)
	{
		rotate(pos);
		followPlayer = true;
		StartCoroutine(gotoPlayer());
	}

	public void shoot(Vector3 pos)
	{
		follow(pos);
		if (elapsedTime > currentWeapon.attackInterval)
		{
			aWeaponSound.Play();
			Quaternion rot = Quaternion.Euler(new Vector3(0, 0, getAngle(player.transform.position) - 90));
			GameObject bullet = (GameObject)Instantiate(currentWeapon.ammoType, transform.position, rot);
			Vector2 dir = getDirection(player.transform.position);
			dir.Normalize();
			bullet.GetComponent<Rigidbody2D>().AddForce(dir * 800.0f);
			elapsedTime = 0;
		}
	}
	
	IEnumerator gotoPlayer()
	{
		yield return new WaitForSeconds(2.0f);
		followPlayer = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (!dead)
		{
			if (followPlayer && Vector2.Distance(player.transform.position, transform.position) > 2.0f)
			{
				rbody.velocity = new Vector2(0.0f, 0.0f);
				Vector2 dir = getDirection(player.transform.position);
				dir.Normalize();
				rbody.AddForce(new Vector2(dir.x * 500.0f, dir.y * 500.0f));
			}
			elapsedTime += Time.deltaTime;
		}
	}
}
