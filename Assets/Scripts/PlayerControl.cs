using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	[SerializeField] private KeyCode turnKey;
	[SerializeField] private KeyCode moveKey;
	[SerializeField] private KeyCode jumpKey;

	private PlayerMovement pm;
	private bool jumping = false;
	private bool turnAround = false;
	private bool moving = false;

	private void Awake() {
		pm = GetComponent<PlayerMovement>();

		if (pm == null) {
			Debug.LogError("No PlayerMovement script found on the player! [PLAYER_CON]");
		}
	}


	private void Update() {
		//Read the inputs

		if (!jumping) {
			jumping = Input.GetKeyDown(jumpKey);
		}

		turnAround = Input.GetKeyDown(turnKey);
			
		if (Input.GetKeyDown(moveKey)) {						/* Поменять этот if на  moving = Input.GetKey(moveKey); если Дима захочет */
			moving = !moving;                                   /* чтобы персонаж двигался пока игрок жмёт кнопку. */
		}
	}


	private void FixedUpdate() {
		// Pass all parameters to the character control script.
		pm.Move(moving, jumping, turnAround);
		jumping = false;
		turnAround = false;
	}
}
