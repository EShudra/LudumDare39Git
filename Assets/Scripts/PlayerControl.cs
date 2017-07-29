using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerControl : MonoBehaviour {

	[SerializeField] private KeyCode turnKey;
	[SerializeField] private KeyCode moveKey;
	[SerializeField] private KeyCode jumpKey;

	private PlayerMovement characterMovement;
	private bool jumping = false;
	private bool turnAround = false;
	private bool moving = false;

	private void Awake() {
		characterMovement = GetComponent<PlayerMovement>();
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
		characterMovement.Move(moving, jumping, turnAround);
		jumping = false;
		turnAround = false;
	}
}
