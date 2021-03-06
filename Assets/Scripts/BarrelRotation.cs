﻿using UnityEngine;
using System.Collections;

public class BarrelRotation : MonoBehaviour {

	[System.Serializable]
	public enum TurretType {
		Top, Left, Bottom, Right
	}
	
	[Header ("Turret location")]
	[SerializeField] private TurretType turretType = TurretType.Bottom;             //Pick turret type, so the barrel will rotate correctly
	[Header("Shooting parameters")]
	[SerializeField] private string tagToShoot;                                     //Choose the tag, which the turret will be looking for
	[SerializeField] private LayerMask whatToHit;
	[SerializeField] private Transform firePoint;
	[SerializeField] private float turretFollowXConstraint = 5f;
	[SerializeField] private float turretFollowYConstraint = 5f;

	[SerializeField] private float healthPoints;

	private Transform target;
	private TurretBulletSpawn tbs;

	private float minRotationConstraint = 0f;
	private float maxRotationConstraint = 180f;

	void Awake () {
		target = GameObject.FindGameObjectWithTag(tagToShoot).transform;
		tbs = GetComponent<TurretBulletSpawn>();

		if (target == null) {
			Debug.LogError("Object with tag '"+tagToShoot+"' was not found! [BARREL_ROTATION.CS]");
		}

		if (firePoint == null) {
			Debug.LogError("No FirePoint found attached to this object! [BARREL_ROTATION.CS]");
		}

		if (tbs == null) {
			Debug.LogError("No TurretBulletSpawn component attached to the barrel! [BARREL_ROTATION.CS]");
		}

		SetConstraints(turretType);
	}
	
	void Update () {
		if (target != null) {
			RotateBarrel();
		}
	}

	private void SetConstraints(TurretType tt) {
		if (tt == TurretType.Top) {
			minRotationConstraint = -180f;
			maxRotationConstraint = 0f;
		} else if (tt == TurretType.Left) {
			minRotationConstraint = -90f;
			maxRotationConstraint = 90f;
		} else if (tt == TurretType.Bottom) {
			minRotationConstraint = 0f;
			maxRotationConstraint = 180f;
		} else {
			minRotationConstraint = 90f;
			maxRotationConstraint = -90f;
		}
	}

	private void RotateBarrel () {

		Vector3 difference = target.position - transform.position;
		DrawFollowBox();
		
		if (difference.x > turretFollowXConstraint || difference.x < -turretFollowXConstraint || difference.y > turretFollowYConstraint || difference.y < -turretFollowYConstraint) {
			tbs.SetFireState(false);
			return;
		}

		difference.Normalize(); //normalizing the vector

		float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; //find the angle in degrees

		if (minRotationConstraint == -90f && maxRotationConstraint == 90f) {                                                                                            //LEFT
			if (rotationZ < minRotationConstraint) {
				rotationZ = minRotationConstraint;
			} else if (rotationZ > maxRotationConstraint) {
				rotationZ = maxRotationConstraint;
			} else {
				CastToThePlayer();
			}
		} else if ((minRotationConstraint == -180f && maxRotationConstraint == 0f) || (minRotationConstraint == 0f && maxRotationConstraint == 180f)) {                 //TOP OR BOTTOM
			if (rotationZ < minRotationConstraint) {
				rotationZ = -minRotationConstraint;
			} else if (rotationZ > maxRotationConstraint) {
				rotationZ = maxRotationConstraint;
			} else {
				CastToThePlayer();
			}
		} else if (minRotationConstraint == 90f && maxRotationConstraint == -90f) {                                                                                     //RIGHT
			if (rotationZ < minRotationConstraint && rotationZ >= 0f) {
				rotationZ = minRotationConstraint;
			} else if (rotationZ > maxRotationConstraint && rotationZ < 0f) {
				rotationZ = maxRotationConstraint;
			} else {
				CastToThePlayer();
			}
		}

		transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
	}

	private void CastToThePlayer() {
		RaycastHit2D hit = Physics2D.Raycast(firePoint.position, (target.position - firePoint.position).normalized, (target.position - firePoint.position).magnitude, whatToHit);
		Debug.DrawRay(firePoint.position, target.position - firePoint.position, Color.green);

		if (hit.collider.tag == tagToShoot) {
			tbs.SetFireState(true);
			Debug.DrawRay(firePoint.position, target.position - firePoint.position, Color.green);
		} else {
			tbs.SetFireState(false);
			Debug.DrawRay(firePoint.position, new Vector3(hit.point.x, hit.point.y, 0f) - firePoint.position, Color.red);
		}
	}

	private void DrawFollowBox() {
		Debug.DrawLine(new Vector2(transform.position.x + turretFollowXConstraint, transform.position.y + turretFollowYConstraint), new Vector2(transform.position.x - turretFollowXConstraint, transform.position.y + turretFollowYConstraint), Color.cyan);
		Debug.DrawLine(new Vector2(transform.position.x + turretFollowXConstraint, transform.position.y + turretFollowYConstraint), new Vector2(transform.position.x + turretFollowXConstraint, transform.position.y - turretFollowYConstraint), Color.cyan);
		Debug.DrawLine(new Vector2(transform.position.x + turretFollowXConstraint, transform.position.y - turretFollowYConstraint), new Vector2(transform.position.x - turretFollowXConstraint, transform.position.y - turretFollowYConstraint), Color.cyan);
		Debug.DrawLine(new Vector2(transform.position.x - turretFollowXConstraint, transform.position.y + turretFollowYConstraint), new Vector2(transform.position.x - turretFollowXConstraint, transform.position.y - turretFollowYConstraint), Color.cyan);

	}
}
