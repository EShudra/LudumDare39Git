using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillSlider : ControlSlider {

	//player object to control
	[SerializeField] private Drill drill;

	//move key
	public KeyCode drillOnOffKey;
	public KeyCode drillMoveKey;


	public override void Awake ()
	{
		//call parent awake
		base.Awake ();

		//find object with tag "player"
		//drill = GameObject.FindGameObjectWithTag ("Drill").GetComponent<Drill> ();

		//check if player in null
		if (drill == null) {
			Debug.LogError ("No drill object found, or no Drill component is attached to the drill! [DRILL_SLIDER.CS]");
		}

	}

	public override void EnablePlayerProperty (bool state)
	{
		drill.setDrillOnOff (state);
	}

	public override void SetPlayerProperty (float value)
	{
		drill.setDrillDpsFromSlider (getSliderValue ());
	}

	//when drill hotkeyDown is true drill switches state (On/Off)
	public override bool GetInputAxis ()
	{
		return Input.GetKeyDown (drillOnOffKey);
	}

	public override void Update ()
	{
		base.Update ();
		//spend power
		if (drill.drillIsOn) {
			float costMul = getSliderValue();
			pBar.SubtractPower (powerCost*Time.deltaTime*costMul);
		}

		if (Input.GetKeyDown(drillMoveKey)){
			drill.toggleDrillAngle ();
		}
	}


}
