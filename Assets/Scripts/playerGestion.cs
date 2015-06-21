using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerGestion : MonoBehaviour
{
	public Rigidbody2D						rbody;
	public GameObject						attachedWeapon;
	public weapon							currentWeapon;
	public GameObject						pickedUpWeapon;
	private Animator						anim;
	public Camera							orthographicCamera;
	public Camera							perspectiveCamera;
	private float							elapsedTime;
	public AudioSource						aWeaponPick;
	public AudioSource						aWeaponThrow;
	public AudioSource						aWeaponDry;
	public AudioSource						aLose;
	public AudioSource[]					aWeaponsSounds;
	public AudioSource						aDeath;
	public bool								dead;

	// Use this for initialization
	void Start ()
	{
		pickedUpWeapon = null;
		anim = gameObject.GetComponent<Animator>();
		elapsedTime = 0.0f;
		dead = false;
	}

	void OnTriggerEnter2D(Collider2D collider)
	{

	}

	void OnTriggerStay2D(Collider2D collider)
	{
		if (!dead)
		{
			if (Input.GetKeyDown(KeyCode.E) && collider.gameObject.name == "weapon_trigger")
			{
				throwWeapon();
				GameObject weapon = collider.gameObject.transform.parent.gameObject;
				pickedUpWeapon = weapon;
				currentWeapon = weapon.GetComponent<weapon>();
				if (currentWeapon.type == "distance")
					aWeaponPick.Play();
				else
					aWeaponThrow.Play();
				attachedWeapon.GetComponent<SpriteRenderer>().sprite = currentWeapon.attach;
				weapon.SetActive(false);
			}
			if (Input.GetMouseButton(0) && currentWeapon != null && currentWeapon.type == "distance" && currentWeapon.ammoCount > 0)
				collider.gameObject.GetComponentsInParent<enemy>()[0].SendMessage("follow", transform.position);
		}
	}

	public void die()
	{
		aDeath.Play();
		StartCoroutine(blink());
		dead = true;
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
		Application.LoadLevel(Application.loadedLevel);
	}
	
	public void shoot()
	{
		if (currentWeapon != null)
		{
			if (currentWeapon.type == "distance")
			{
				if (currentWeapon.ammoCount > 0)
				{
					if (elapsedTime > currentWeapon.attackInterval)
					{
						aWeaponsSounds[currentWeapon.id].Play();
						Quaternion rot = Quaternion.Euler(new Vector3(0, 0, getAngle() - 90));
						GameObject bullet = (GameObject)Instantiate(currentWeapon.ammoType, attachedWeapon.transform.position, rot);
						Vector2 dir = getDirection();
						dir.Normalize();
						bullet.GetComponent<Rigidbody2D>().AddForce(dir * 800.0f);
						currentWeapon.ammoCount--;
						elapsedTime = 0;
					}
				}
				else
					aWeaponDry.Play();
			}
			else if (currentWeapon.type == "melee")
			{
				if (elapsedTime > currentWeapon.attackInterval)
				{
					aWeaponsSounds[currentWeapon.id].Play();
					GameObject bullet = (GameObject)Instantiate(currentWeapon.ammoType, attachedWeapon.transform.position, transform.rotation);
					Vector2 dir = getDirection();
					dir.Normalize();
					bullet.GetComponent<Rigidbody2D>().AddForce(dir * 800.0f);
					elapsedTime = 0;
				}
			}
		}
	}

	public void throwWeapon()
	{
		if (pickedUpWeapon != null)
		{
			aWeaponThrow.Play();
			Vector3 dir = getDirection();
			dir.Normalize();
			pickedUpWeapon.SetActive(true);
			pickedUpWeapon.transform.position = gameObject.transform.position;
			pickedUpWeapon.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir.x * 100.0f, dir.y * 100.0f));
			pickedUpWeapon.GetComponent<Rigidbody2D>().AddTorque(10.0f);
			pickedUpWeapon = null;
			attachedWeapon.GetComponent<SpriteRenderer>().sprite = null;
			currentWeapon = null;
		}
	}

	private Vector3 getDirection()
	{
		Vector3 mousePos = orthographicCamera.ScreenToWorldPoint(Input.mousePosition);
		mousePos.x = mousePos.x - transform.position.x;
		mousePos.y = mousePos.y - transform.position.y;
		return (mousePos);
	}

	private float getAngle()
	{
		Vector3 dir = getDirection();
		float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) + 90;
		return (angle);
	}

	public void rotate()
	{
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, getAngle()));
		perspectiveCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10.0f);
		orthographicCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10.0f);
	}
	// Update is called once per frame
	void Update ()
	{
		if (!dead)
		{
			rotate();
			if (Input.GetMouseButtonDown(1))
				throwWeapon();
			if (Input.GetMouseButton(0))
				shoot();
			rbody.velocity = new Vector2(0.0f, 0.0f);
			if (Input.GetKey(KeyCode.W))
			{
				anim.SetBool("walk", true);
				rbody.AddForce(new Vector2(0.0f, 500.0f));
			}
			if (Input.GetKey(KeyCode.S))
			{
				anim.SetBool("walk", true);
				rbody.AddForce(new Vector2(0.0f, -500.0f));
			}
			if (Input.GetKey(KeyCode.A))
			{
				anim.SetBool("walk", true);
				rbody.AddForce(new Vector2(-500.0f, 0.0f));
			}
			if (Input.GetKey(KeyCode.D))
			{
				anim.SetBool("walk", true);
				rbody.AddForce(new Vector2(500.0f, 0.0f));			
			}
			if (Input.GetKeyUp(KeyCode.W) ||
			    Input.GetKeyUp(KeyCode.A) ||
			    Input.GetKeyUp(KeyCode.S) ||
			    Input.GetKeyUp(KeyCode.D))
				rbody.velocity = new Vector2(0.0f, 0.0f);
			if (!Input.GetKey(KeyCode.W)
			    && !Input.GetKey(KeyCode.S)
			    && !Input.GetKey(KeyCode.A)
			    && !Input.GetKey(KeyCode.D))
				anim.SetBool("walk", false);
			elapsedTime += Time.deltaTime;
		}
		else
			rbody.velocity = new Vector2(0.0f, 0.0f);
	}
}
