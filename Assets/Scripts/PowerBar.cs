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
			CheckDeath ();
			UpdateSlider ();
		}
	}

	private Player player;

	// Use this for initialization
	void Awake () {
		powerBarImage = GetComponent<Image> ();

		if (powerBarImage == null) {
			Debug.LogError ("Image component is not assigned to PowerBar Game object! [POWER_BAR.CS]");
		}

		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

		if (player == null) {
			Debug.LogError("No player found, or no Player component is attached to the Player! [POWER_BAR.CS]");
		}

		currPower = maxPower;
		UpdateSlider ();
	}

	public void SubtractPower (float value){
		currPower -= value;
		CheckDeath ();
		UpdateSlider ();
	}

	public void AddPower(float value){
		currPower += value;
		UpdateSlider ();
	}

	private void CheckDeath(){
		if (currPower <= 0) {
			Debug.Log ("GameOver");
			if (player != null) {
				player.DestroyPlayer();
			}
			//call end game here
		}
	}

	private void UpdateSlider(){
		powerBarImage.fillAmount = currPower / maxPower;
	}
}
