using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementSlider : ControlSlider {

	Player player;

	public KeyCode key;

	public override void Awake (){
		base.Awake ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();

		if (player == null) {
			Debug.LogError ("No Player object found, or no Player component is attached to the Player! [MOVEMENT_SLIDER.CS]");
		}

	}

	public override void setPlayerProperty (float sliderValue)
	{
		player.SetSpeedFromSlider (sliderValue);
	}

	public override void enablePlayerProperty (bool state)
	{
		//throw new System.NotImplementedException ();
	}

	public override bool getInputAxis ()
	{
		 //return Input.GetAxis ("Horizontal");
		return Input.GetKeyDown(key);
	}

}
