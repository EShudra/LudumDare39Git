using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSlider : ControlSlider {

	//player object to control
	Player player;
	//player movement component
	PlayerMovement pm;

	//key used to jump
	public KeyCode jumpKey;

	//angle array
	public float[] jumpAngleArr;
	float currJumpAngle = 90;
	//current array element
	int jumpAngleArrPos = 1;

	//use to make slider green on a
	//time after jump
	bool isJumpingCycle = false;//true if slider is green
	float sliderGreenDelay = 0.5f;//after this time slider turns to red (in seconds)
	float sliderGreenTime = 0f;

	//when jump key pressed you can change
	//jump mode with this key
	public KeyCode jumpMode;

	//call parent awake and init player object
	public override void Awake ()
	{
		//call parent awake
		base.Awake ();

		//find object with tag "player"
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();

		//get player movement component
		pm = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ();

		//check if player in null
		if (player == null) {
			Debug.LogError ("No Player object found, or Player component is not attached to plyaer [JUMP_SLIFER.CS]");
		}

		//check if player movement is null
		if (pm == null) {
			Debug.LogError ("No Player object found, or Player component is not attached to plyaer [JUMP_SLIFER.CS]");
		}
	}

	//use this method to make player jump
	public override void EnablePlayerProperty (bool state)
	{	
		if (isJumpingCycle) {
			Quaternion rotation = Quaternion.Euler (0, 0, currJumpAngle);
			player.SetJumpAngleFromSlider (rotation);
		}
	}


	//set player jump power according to slider value
	public override void SetPlayerProperty (float value)
	{
			player.SetJumpForceFromSlider (value);
	}

	//when true property and slider set to ON
	public override bool GetInputAxis ()
	{
		return Input.GetKeyUp (jumpKey);
	}



	public override void Update () {
		base.Update ();
		//sliderIsGreen = false;
		if (Input.GetKeyDown (jumpKey)) {
			jumpAngleArrPos = 0;
			currJumpAngle = GetJumpAngle ();
			isJumpingCycle = true;
		}

		if (Input.GetKey (jumpKey)) {
			if (Input.GetKeyDown (jumpMode)) {
				currJumpAngle = GetJumpAngle ();
			}
		}

		if (Input.GetKeyUp (jumpKey)) {
			sliderGreenTime = Time.time;
			StartCoroutine (jumpOffWithDelay(sliderGreenDelay));
		}

		/*if (Time.time - sliderGreenTime > sliderGreenDelay) {

		}*/
	}

	IEnumerator jumpOffWithDelay(float seconds){
		yield return new WaitForSeconds (seconds);
		isJumpingCycle = false;
		PowerOff ();		
	}

	//get current angle from array and set pointer on next
	//reset pointer if it out of range
	float GetJumpAngle(){
		float angle = jumpAngleArr [jumpAngleArrPos];
		jumpAngleArrPos++;
		if (jumpAngleArrPos >= jumpAngleArr.Length) {
			jumpAngleArrPos = 0;
		}

		if (!pm.facingRight) {
			angle = 180f - angle;
		}
		return angle;
	}

}
