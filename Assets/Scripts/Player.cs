using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public class PlayerStats {
		public float maximumPower = 300f;
		private float powerAmount_ = 0f;

		public float powerAmount {
			get { return powerAmount_; }
			set { powerAmount_ = Mathf.Clamp(value, 0, maximumPower); }
		}

		public float maximumHealth = 100f;
		private float healthAmount_ = 0f;
		
		public float healthAmount {
			get { return healthAmount_; }
			set { healthAmount_ = Mathf.Clamp(value, 0, maximumHealth); }
		}

		public PlayerStats() {
			this.powerAmount = maximumPower;
			this.healthAmount = maximumHealth;
		}

		public PlayerStats(float powerToSet, float healthToSet) {
			this.powerAmount = powerToSet;
			this.healthAmount = healthToSet;
		}
	}

	private PlayerStats stats = new PlayerStats(10f, 100f);
	private PlayerMovement pm;

	private void Awake() {
		pm = GetComponent<PlayerMovement>();

		if (pm == null) {
			Debug.LogError("No PlayerMovement script found on the player! [PLAYER.CS]");
		}
	}

	private void Start() {
		Debug.Log("The game has started! Player now has " + stats.powerAmount + " power and " + stats.healthAmount + " health.");
	}

	public void SetSpeedFromSlider (float sliderMultiplier) {       //Set current speed with slider
		pm.movementSpeed = pm.minimumMovementSpeed + (pm.maximumMovementSpeed - pm.minimumMovementSpeed) * Mathf.Clamp(sliderMultiplier, 0f, 1f);
	}

	public void SetJumpForceFromSlider (float sliderMultiplier) {        //Set jump force with slider
		pm.jumpForce = pm.minimumJumpForce + (pm.maximumJumpForce - pm.minimumJumpForce) * Mathf.Clamp(sliderMultiplier, 0f, 1f);
	}

	public void SetJumpAngleFromSlider (Quaternion rotation) {
		pm.jumpVector = (rotation * Vector3.right).normalized;
		pm.jumping = true;
	}

	public void Charge(float chargeAmount) {			//Charge Player with some batteries
		stats.powerAmount += chargeAmount;
		Debug.Log("You picked up a battery! +" + chargeAmount + " power gained. Current power - " + stats.powerAmount + ".");

		if (stats.powerAmount <= 0) {
			Debug.Log("You died! Zero power reached! Health remaining: " + stats.healthAmount + ".");
			Destroy(gameObject);
		}
	}

	public void Hit (float damage) {					//Hit Player with some damage
		stats.healthAmount -= damage;
		Debug.Log("You got hit with " + damage + " damage. Current health - " + stats.healthAmount + ".");

		if (stats.healthAmount <= 0) {
			Debug.Log("You died! Someone killed you! Power unused: " + stats.powerAmount + ".");
			Destroy(gameObject);
		}
	}
}
