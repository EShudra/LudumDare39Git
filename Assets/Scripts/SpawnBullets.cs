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

	//---5. Drag & Drop your prefab here
	[SerializeField] private GameObject BulletPrefab;

	[SerializeField] private float fireRate = 0.2f;
	[SerializeField] private float fireMinRate = 0.1f;
	[SerializeField] private float fireMaxRate = 0.4f;
	[SerializeField] private float fireDelay = 0.1f;

	private float lastBulletTime = 0f;
	private float startTime = 0f;
	private bool fireIsOn = false;
	//private string fireAxis = "Fire1"; // change if you use custom axis to fire

	[Header("Toggle rotation")]
	[SerializeField] private bool rotationEnabled = false;
	
	private PlayerMovement pm;

	[Header("Rotation keys")]
	[SerializeField] private KeyCode rotateUpKey; // keybord key to ratate gun up
	[SerializeField] private KeyCode rotateDownKey; // keybord key to ratate gun down

	[Header("Rotation angles")]
	[SerializeField] private float fireEulerAngleMin = 0; 
	[SerializeField] private float fireEulerAngleMax = 90; 
	private float fireEulerAngle = 0; // from fireEulerAngleMin to fireEulerAngleMax

	[Header("Rotation speed and bullet spread")]
	[SerializeField] private float angularEulerSpeed = 30; //angular speed per second
	[SerializeField] private float angularEulerSpread = 4; //angular speed per second
	private bool playerLooksRight;

	void Awake(){
		fireRate = fireMaxRate;

		pm = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ();
		
		if (pm == null) {
			Debug.LogError ("No Player object found, or no PlayerMovement component is attached to the Player! [SPAWN_BULLETS.CS]");
		}
	}
		
	public void SetFireState(bool state){
		fireIsOn = state;
	}

	public void SetFireRateFromSlider(float value){
		fireRate = fireMaxRate - (fireMaxRate - fireMinRate) * value;
		//Debug.Log (value);
		Debug.Log (fireRate);
	}

	void FixedUpdate () {
		//spawn bullet with fireRate after fireDelay
		if (fireIsOn) {
			if (Time.time - startTime > fireDelay) {
				if (Time.time - lastBulletTime > fireRate) {
					lastBulletTime = Time.time;
					playerLooksRight = pm.facingRight;
					SpawnBullet ();
				}
			}
		}

		if (rotationEnabled) {
			//rotate gun up if rotate Up key is pressed
			if (Input.GetKey(rotateUpKey)) {
				fireEulerAngle += angularEulerSpeed * Time.deltaTime;
			}

			//rotate gun up if rotate Down key is pressed
			if (Input.GetKey(rotateDownKey)) {
				fireEulerAngle -= angularEulerSpeed * Time.deltaTime;
			}
		}

		Debug.Log (fireEulerAngle);
		//clamp rotation angle
		fireEulerAngle = Mathf.Clamp (fireEulerAngle, fireEulerAngleMin, fireEulerAngleMax);

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
		//flip angle if needed
		float spawnAngle = 180 -fireEulerAngle;
		if (playerLooksRight) {
			spawnAngle = fireEulerAngle;
		}

		//add spread
		spawnAngle += (Random.value-0.5f)*2*angularEulerSpread;

		Quaternion newRotation = Quaternion.Euler(new Vector3(0,0,spawnAngle));
		Instantiate (BulletPrefab, spawnPos, newRotation);

	}
}
