using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	[SerializeField] private KeyCode turnKey;

	private PlayerMovement pm;

	private void Awake() {
		pm = GetComponent<PlayerMovement>();

		if (pm == null) {
			Debug.LogError("No PlayerMovement script found on the player! [PLAYER_CONTROL.CS]");
		}
	
	}

	private void Update() {
		//Read the input
		if (!pm.turnAround) {
			pm.turnAround = Input.GetKeyDown (turnKey);
		}

	}
}
