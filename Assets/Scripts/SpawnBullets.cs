using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBullets : MonoBehaviour {
	//Script to shoot bullets in mouse cursor direction in X-Y dirrections

	//---To use this script you have to prepare yor bullet prefab:
	//---1. Create empty object - BulletRoot
	//---2. Add a child object to BulletRoot with your bullet graphics
	//---3. Bullet graphics must be located in X,Z dimensions(where Z is moving direction)
	//---4. To move your bullet forward use Z axis!!!!

	public GameObject BulletPrefab;
	//---5. Drag & Drop your prefab here

	public float fireRate = 0.2f;
	public float fireMinRate = 0.1f;
	public float fireMaxRate = 0.4f;
	public float fireDelay = 0.1f;

	private float lastBulletTime = 0f;
	private float startTime = 0f;
	private bool fireIsOn = false;
	//private string fireAxis = "Fire1"; // change if you use custom axis to fire

	public GameObject player;

	public float fireEulerAngle; //from 0 to 90
	public bool playerLooksRight;

	void Awake(){
		fireRate = fireMaxRate;
	}
		
	public void setFireState(bool state){
		fireIsOn = state;
	}

	public void setFireRateFromSlider(float value){
		fireRate = fireMaxRate - (fireMaxRate - fireMinRate) * value;
		//Debug.Log (value);
		Debug.Log (fireRate);
	}

	void FixedUpdate () {
		if (fireIsOn) {
			if (Time.time - startTime > fireDelay) {
				if (Time.time - lastBulletTime > fireRate) {
					lastBulletTime = Time.time;
					SpawnBullet ();
				}
			}
		}
	}

	void SpawnBullet(){
		//get camera pos
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePos.z = 0;

		//get spawn point pos
		Vector3 spawnPos = transform.position;

		//calculate fire direction vector
		Vector3 direction = mousePos - spawnPos;

		//make an instance of bullet with correct rotation
		Quaternion newRotation = Quaternion.LookRotation(direction, new Vector3(0,0,1));
		Instantiate (BulletPrefab, spawnPos, newRotation);

	}
}
