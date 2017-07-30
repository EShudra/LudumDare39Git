using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour {

	//Image component of PowerBar
	private Image powerBarImage;

	//maximum amount of HP
	public float maxPower;

	//current HP
	private float currPower_;

	public float currPower {
		get { return currPower_; }
		set { currPower_ = Mathf.Clamp(value, 0f, maxPower);
			checkDeath ();
			updateSlider ();
		}
	}

	// Use this for initialization
	void Awake () {
		powerBarImage = GetComponent<Image> ();

		if (powerBarImage == null) {
			Debug.LogError ("Image component is not assigned to PowerBar Game object [POWER_BAR.CS]");
		}

		currPower = maxPower;
		updateSlider ();
	}

	public void subtractPower(float value){
		currPower -= value;
		checkDeath ();
		updateSlider ();
	}

	public void addPower(float value){
		currPower += value;
		updateSlider ();
	}

	private void checkDeath(){
		if (currPower <= 0) {
			Debug.Log ("GameOver");
			//call end game here
		}
	}

	private void updateSlider(){
		powerBarImage.fillAmount = currPower / maxPower;
	}

	// Update is called once per frame
	void Update () {
	}
}
