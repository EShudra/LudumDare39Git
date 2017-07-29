using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementSlider : ControlSlider {

	GameObject player;

	public KeyCode key;

	public override void Awake (){
		base.Awake ();
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	public override void setPlayerProperty (float sliderValue)
	{
		Debug.Log ("Set players movement according to slider state");
		//player.GetComponent<Player>().setSpeedFromSlider(sliderValue); ????????
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
