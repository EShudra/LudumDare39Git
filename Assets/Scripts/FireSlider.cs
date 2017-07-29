using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSlider : ControlSlider {

	SpawnBullets gun;

	public KeyCode key;

	public override void Awake ()
	{
		base.Awake ();
		gun = GameObject.FindGameObjectWithTag ("Gun").gameObject.GetComponent<SpawnBullets> ();
	}

	public override void enablePlayerProperty (bool state)
	{
		gun.setFireState (state);
	}

	public override void setPlayerProperty (float value)
	{
		gun.setFireRateFromSlider (value);
	}

	public override bool getInputAxis ()
	{
		return Input.GetKeyDown(key);
	}
}
