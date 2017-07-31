using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour {

	//playerMovement object
	private PlayerMovement pm;

	//damage per second that caused by drill
	[SerializeField] private float minDrillDPS = 10f;
	[SerializeField] private float maxDrillDPS = 200f;
	[HideInInspector] public float drillDPS;

	//collider
	private BoxCollider2D drillCollider;
	//drill is enabled
	[HideInInspector] public bool drillIsOn;

	//drill shaking
	private Vector3 storePos;
	private float shakePeriod;
	[SerializeField] private float minShakePeriod = 45f;
	[SerializeField] private float maxShakePeriod = 120f;
	[SerializeField] private float shakeAmplitude = 0.032f;

	//drill rotation pivot
	public Transform rotPivot;

	//drill direction switcher
	[HideInInspector] public bool drillIsDown = false;
	public float drillDownAngle = -90f;
	private float drillAngle = 0f;
	public float angularChangeTime = 1; // seconds to change position

	//used to check last player direction
	private bool lastFasing;

	//drill flip perion
	[SerializeField] private float flipPeriod; //seconds

	public void setDrillOnOff(bool state){
		drillCollider.enabled = state;
		drillIsOn = state;
		if (state) {
			//storePos = rotPivot.localPosition;
		} else {
			rotPivot.localPosition = Vector3.zero;
		}
	}

	public void setDrillDpsFromSlider(float value){
		//value = Mathf.Clamp01 (value);
		if (value == 0) {
			drillDPS = 0;
		} else {
			drillDPS = minDrillDPS + (maxDrillDPS-minDrillDPS) * value;
		}
		shakePeriod = minShakePeriod + (maxShakePeriod - minShakePeriod) * value;
	}

	// Use this for initialization
	void Awake () {
		//init player movement
		pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

		if (pm == null) {
			Debug.LogError ("Player is lost or PlayerMovement component is not attached [DRILL.CS]");
		}

		//init drill collider
		drillCollider = GetComponent<BoxCollider2D>();

		if (drillCollider == null) {
			Debug.LogError ("Drill's boxCollider2D is not found [DRILL.CS]");
		}

		//init drill DPS
		drillDPS = minDrillDPS;
		//init shake period
		shakePeriod = minShakePeriod;

		//start drill flipping animation
		//StartCoroutine(flipCycle());
	}
	
	// Update is called once per frame
	void Update () {
		

		if (drillIsOn && (drillDPS != 0)) {
			Vector3 currPos = rotPivot.localPosition;
			Vector3 offset = new Vector3 (0, shakeAmplitude*Mathf.Sin(Time.time*shakePeriod), 0);
			Debug.Log (offset.y);
			rotPivot.localPosition = offset;
		}

		float drillFinalAngle = 0f;
		if (drillIsDown) {
			drillFinalAngle = drillDownAngle;
			if (!pm.facingRight) {
				drillFinalAngle = -drillDownAngle;	
			}

		}

		if (lastFasing != pm.facingRight) {
			drillAngle = -drillAngle;
		}

		drillAngle = Mathf.Lerp (drillAngle, drillFinalAngle, Time.deltaTime*(1/angularChangeTime));

		rotPivot.rotation = Quaternion.Euler (0, 0, drillAngle);

		lastFasing = pm.facingRight;
	}

	public void toggleDrillAngle(){
		drillIsDown = !drillIsDown;
	}

	IEnumerator flipCycle(){
		while(true){
			if (drillIsOn){
				Vector3 drillScale = this.transform.localScale;
				drillScale.y *= -1;
				this.transform.localScale = drillScale;
			}
			yield return new WaitForSeconds(flipPeriod);
		}
	}
}
