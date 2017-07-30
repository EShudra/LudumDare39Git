using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBulletSpawn : MonoBehaviour {

	[SerializeField] private GameObject BulletPrefab;
	[SerializeField] private Transform firePoint;

	[SerializeField] private float fireRate = 0.2f;
	[SerializeField] private float fireMinRate = 0.1f;
	[SerializeField] private float fireMaxRate = 0.4f;
	[SerializeField] private float fireDelay = 0.1f;
	[SerializeField] private float bulletSpread = 0f; //angular euler spread

	private float lastBulletTime = 0f;
	private float startTime = 0f;

	[HideInInspector] public bool fireIsOn = false;
	[HideInInspector] public float damage = 5f;

	void Awake() {
		fireRate = fireMaxRate;

		if (firePoint == null) {
			Debug.LogError("No FirePoint found! [TURRET_BULLET_SPAWN.CS]");
		}
	}
	
	public void SetFireState(bool state) {
		fireIsOn = state;
	}

	public void SetFireRateFromSlider(float value) {
		fireRate = fireMaxRate - (fireMaxRate - fireMinRate) * value;
		//Debug.Log (value);
		Debug.Log(fireRate);
	}

	void FixedUpdate() {
		//spawn bullet with fireRate after fireDelay
		if (fireIsOn) {
			if (Time.time - startTime > fireDelay) {
				if (Time.time - lastBulletTime > fireRate) {
					lastBulletTime = Time.time;
					SpawnBullet();
				}
			}
		}
	}

	void SpawnBullet() {
		//get spawn point pos
		Vector3 spawnPos = transform.position;

		//calculate fire direction vector
		Vector3 direction = spawnPos;

		//add spread
		float spawnAngle = (Random.value - 0.5f) * 2 * bulletSpread;

		Quaternion newRotation = Quaternion.Euler(new Vector3(0, 0, spawnAngle));
		Instantiate(BulletPrefab, transform.position, firePoint.rotation, transform);
	}
}
