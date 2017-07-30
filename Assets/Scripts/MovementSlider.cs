using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementSlider : ControlSlider {

	//player object to control
	Player player;
	//player movement object
	PlayerMovement pm;
	//move key
	public KeyCode moveKey;

	//call base Awake() and init gun
	public override void Awake (){
		//call parent awake
		base.Awake ();

		//find object with tag "player"
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		pm = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ();

		//check if player in null
		if (player == null) {
			Debug.LogError ("No Player object found, or no Player component is attached to the Player! [MOVEMENT_SLIDER.CS]");
		}

		//check if playerMovement in null
		if (pm == null) {
			Debug.LogError ("No Player object found, or no PlayerMovement component is attached to the Player! [MOVEMENT_SLIDER.CS]");
		}

	}

	//set player movement speen from according to player value
	public override void SetPlayerProperty (float sliderValue)
	{
		player.SetSpeedFromSlider (sliderValue);
	}

	//on/off player movement
	public override void EnablePlayerProperty (bool state)
	{
		pm.moving = state;
	}


	//when true property and slider set to ON
	public override bool GetInputAxis ()
	{
		return Input.GetKeyDown(moveKey);
	}

	override public void Update(){
		base.Update ();
		if (pm.moving) {
			float costMul = getSliderValue();
			//Debug.Log (powerCost * Time.deltaTime*costMul);
			pBar.subtractPower (powerCost*Time.deltaTime*costMul);
		}
	}

}
