using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {

	//player game object
	private GameObject player;
	private Player pPlayer; //Player component
	private PlayerMovement pPlayerMovement; //PlayerMovement component

	private Animator anim;

	// Use this for initialization
	void Awake () {
		//get player object
		player = GameObject.FindGameObjectWithTag ("Player");

		if (player == null) {
			Debug.LogError ("Player is missed [PLAYER_ANIMATIONS.CS]");
		}
			
		//get player components
		pPlayer = player.GetComponent<Player> ();
		pPlayerMovement = player.GetComponent<PlayerMovement> ();

		//get player graphics animator
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		bool walk = pPlayerMovement.moving;
		walk = walk && (pPlayerMovement.movementSpeed != 0);
		//Debug.Log (pPlayerMovement.movementSpeed);
		anim.SetBool ("Walk", walk);
	}
}
