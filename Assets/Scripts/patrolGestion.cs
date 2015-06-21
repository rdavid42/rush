using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class patrolGestion : MonoBehaviour
{
	public Transform		patrolList;
	private int				patrolCount;
	private int				currentPoint;
	private Transform[]		patrolPoints;
	public enemy			enemy;

	// Use this for initialization
	void Start()
	{
		int		i;

		patrolCount = patrolList.childCount;
		patrolPoints = new Transform[patrolCount];
		i = 0;
		foreach (Transform patrol in patrolList.gameObject.transform.GetComponentInChildren<Transform>())
		{
			patrolPoints[i] = patrol;
			i++;
		}
		currentPoint = 0;
	}

	private Vector3 getDirection()
	{
		Vector3 Pos = patrolPoints[currentPoint].position;
		Pos.x = Pos.x - transform.position.x;
		Pos.y = Pos.y - transform.position.y;
		return (Pos);
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
	}

	void makePatrol()
	{
		if (transform.position == patrolPoints[currentPoint].position)
		{
			currentPoint++;
			if (currentPoint == patrolCount)
				currentPoint = 0;
		}
		if (enemy.followPlayer == false)
		{
			rotate ();
			transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].transform.position,Time.deltaTime );
		}
	}

	// Update is called once per frame
	void Update () {
		if (!enemy.dead)
			makePatrol();
	}
}
