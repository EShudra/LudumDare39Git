using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBulletSpawn : MonoBehaviour {

	[SerializeField] private GameObject BulletPrefab;
	[SerializeField] private Transform firePoint;

	public float damage = 5f;
	[SerializeField] private float fireRate = 0.2f;
	[SerializeField] private float fireMinRate = 0.1f;
	[SerializeField] private float fireMaxRate = 0.4f;
	[SerializeField] private float fireDelay = 0.1f;

	private float lastBulletTime = 0f;
	private float startTime = 0f;

	[HideInInspector] public bool fireIsOn = false;

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
		Bullet bullet = Instantiate(BulletPrefab, firePoint.position, firePoint.rotation).GetComponentInChildren<Bullet>();
		if (bullet != null) {
			bullet.damage = damage;
		} else {
			Debug.LogError("No Bullet component found attached to the bullet prefab! [TURRET_BULLET_SPAWN.CS]");
		}
	}
}
