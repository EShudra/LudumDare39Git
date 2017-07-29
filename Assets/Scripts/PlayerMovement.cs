using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement: MonoBehaviour {
	
	public float movementSpeed = 10f;                                   // The speed the player can travel in the x axis.
	public float maximumMovementSpeed = 12f;
	public float minimumMovementSpeed = 0f;																	// TODO: currentMS = minMS + (maxMS-minMS) * MSsliderAmplifier; AND do max value check
	public float jumpForce = 400f;                                      // Amount of force added when the player jumps.
	public float maximumJumpForce = 800f;
	public float minimumJumpForce = 200f;																	// TODO: currentJF = minJF + (maxJF-minJF) * JFsliderAmplifier; AND do max value check
	[SerializeField] private bool airControl = false;                 // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask whatIsGround;                  // A mask determining what is ground to the character

	private Transform groundCheck;    // A position marking where to check if the player is grounded.
	private const float groundCheckRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool grounded;            // Whether or not the player is grounded.
	private Rigidbody2D rb2D;		//Reference to the rigidbody2D
	[HideInInspector] public bool facingRight = false;  // For determining which way the player is currently facing.

	private void Awake() {
		
		// Setting up references.
		groundCheck = transform.Find("GroundCheck");
		rb2D = GetComponent<Rigidbody2D>();

		if (groundCheck == null) {
			Debug.LogError("No GroundCheck object found! [PLAYER_MOVEMENT.CS]");
		}

		if (rb2D == null) {
			Debug.LogError("No Rigidbody2D component found! [PLAYER_MOVEMENT.CS]");
		}
	}


	private void FixedUpdate() {																										//FIXED UPDATE
		grounded = false;

		/* The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		   This can be done using layers instead but Sample Assets will not overwrite your project settings. */
		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, whatIsGround);
		for (int i = 0; i < colliders.Length; i++) {
			if (colliders[i].gameObject != gameObject)
				grounded = true;
		}
	}

	public void Move(bool moving, bool jumping, bool turnAround) {																		//MOVE

		//only control the player if grounded or airControl is turned on
		if ((grounded || airControl) && moving) {
			// Move the character if the correct key was pressed
			if (facingRight) {
				transform.Translate(new Vector2(movementSpeed * Time.fixedDeltaTime, 0f));
			} else {
				transform.Translate(new Vector2(-movementSpeed * Time.fixedDeltaTime, 0f));
			}
		}

		// Turn around if the correct key was pressed
		if (turnAround) {
			Flip();
		}
		// Jump, if the correct key was pressed
		if (grounded && jumping) {
			// Add a vertical force to the player.
			grounded = false;
			rb2D.AddForce(new Vector2(0f, jumpForce));
		}
	}

	private void Flip() {																												//FLIP
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
