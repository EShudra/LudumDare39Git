using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public PowerBar pBar;
	public HpBar hpBar;

	[System.Serializable]
	public class PlayerStats {

		public float maximumHealth = 100f;
		private float healthAmount_ = 0f;
		
		public float healthAmount {
			get { return healthAmount_; }
			set { healthAmount_ = Mathf.Clamp(value, 0, maximumHealth); }
		}

		public PlayerStats() {
			this.healthAmount = maximumHealth;
		}

		public PlayerStats(float healthToSet) {
			this.healthAmount = healthToSet;
		}
	}

	public float damage;
	public float fallDamage;

	[SerializeField] private Transform playerExplosionPrefab;
	[SerializeField] private Transform batteryPickupPrefab;
	[SerializeField] private PlayerStats stats = new PlayerStats();

	private PlayerMovement pm;

	private void Awake() {
		pm = GetComponent<PlayerMovement>();

		if (pm == null) {
			Debug.LogError("No PlayerMovement script found on the player! [PLAYER.CS]");
		}
	}

	private void Start() {
		Debug.Log("The game has started! Player now has " + stats.healthAmount + " health.");
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

	public void Charge (float chargeAmount) {
		pBar.AddPower(chargeAmount);
		Instantiate(batteryPickupPrefab, transform.position, transform.rotation, transform);
	}

	public void Hit (float damage) {					//Hit Player with some damage
		stats.healthAmount -= damage;
		hpBar.updateBar (stats.healthAmount, stats.maximumHealth);
		Debug.Log("You got hit with " + damage + " damage. Current health - " + stats.healthAmount + ".");

		if (stats.healthAmount <= 0) {
			Debug.Log("You died! Someone killed you!");
			DestroyPlayer();
		}
	}

	public void DestroyPlayer() {
		Instantiate(playerExplosionPrefab, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}
