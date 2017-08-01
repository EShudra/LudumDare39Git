using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour {

	[SerializeField] private float chargeAmount = 1000f;

	private AudioSource batteryEngine;
	[SerializeField] private AudioClip batteryPickup;

	private void Awake() {
		batteryEngine = GetComponent<AudioSource>();

		if (batteryEngine == null) {
			Debug.LogError("No AudioSource found! [BATTERY.CS]");
		}
	}

	private void OnTriggerEnter2D(Collider2D col) {
		if (col.CompareTag("Player")) {
			Player player = col.GetComponent<Player>();

			if (player != null) {
				batteryEngine.PlayOneShot(batteryPickup);
				player.Charge(chargeAmount);
				GetComponent<SpriteRenderer>().enabled = false;
				GetComponent<Collider2D>().enabled = false;
				Destroy(gameObject, 0.3f);
			} else {
				Debug.LogError("No Player component found attached to the player! [BATTERY.CS]");
			}
		}
	}
}
