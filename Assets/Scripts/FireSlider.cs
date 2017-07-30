using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSlider : ControlSlider {

	//gun object to control
	SpawnBullets gun;

	//fire key code
	public KeyCode fireKey;

	//call base Awake() and init gun
	public override void Awake ()
	{
		//call parent awake
		base.Awake ();

		//find object with tag "gun"
		gun = GameObject.FindGameObjectWithTag ("Gun").gameObject.GetComponent<SpawnBullets> ();

		//check if gun in null
		if (gun == null) {
			Debug.LogError ("No gun object found, or SpawnBullets component is not attached to gun [FIRE_SLIDER.CS]");
		}
	}

	//turn on/off fire
	public override void EnablePlayerProperty (bool state)
	{
		gun.setFireState (state);
	}

	//change fire rate by slider value
	public override void SetPlayerProperty (float value)
	{
		gun.setFireRateFromSlider (value);
	}
		
	//when true property and slider set to ON
	public override bool GetInputAxis ()
	{
		return Input.GetKeyDown(fireKey);
	}
}
