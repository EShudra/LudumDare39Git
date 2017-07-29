using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour {

	private float chargeAmount = 50f;

	private void OnTriggerEnter2D(Collider2D col) {
		if (col.CompareTag("Player")) {
			Player player = col.GetComponent<Player>();

			if (player != null) {
				player.Charge(chargeAmount);
				Destroy(gameObject);
			} else {
				Debug.LogError("No Player component found attached to the player! [BATTERY.CS]");
			}
		}
	}
}
