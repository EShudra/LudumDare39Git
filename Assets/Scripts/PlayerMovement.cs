using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement: MonoBehaviour {
	
	public float movementSpeed = 10f;                                   // The speed the player can travel in the x axis.
	public float maximumMovementSpeed = 12f;
	public float minimumMovementSpeed = 0f;

	public float jumpForce = 400f;                                      // Amount of force added when the player jumps.
	public float maximumJumpForce = 1000f;
	public float minimumJumpForce = 200f;
	public Vector3 jumpVector = Vector3.up;

	[SerializeField] private bool airControl = false;                 // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask whatIsGround;                  // A mask determining what is ground to the character

	private Transform groundCheck;    // A position marking where to check if the player is grounded.
	private const float groundCheckRadius = 0.25f; // Radius of the overlap circle to determine if grounded
	private bool grounded;            // Whether or not the player is grounded.
	private Rigidbody2D rb2D;		//Reference to the rigidbody2D

	[HideInInspector] public bool jumping; // For determining when the player is jumping.
	[HideInInspector] public bool facingRight = false;  // For determining which way the player is currently facing.
	[HideInInspector] public bool moving = false;       // For determining if the player is currently moving
	[HideInInspector] public bool jumpPreparation = false;  // For determining if the player is preparing to jump.
	[HideInInspector] public bool turnAround = false;

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
		/* The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		   This can be done using layers instead but Sample Assets will not overwrite your project settings. */
		grounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, whatIsGround);
		Debug.DrawLine(new Vector2(groundCheck.position.x + groundCheckRadius, groundCheck.position.y), new Vector2(groundCheck.position.x - groundCheckRadius, groundCheck.position.y));
		Debug.DrawLine(new Vector2(groundCheck.position.x, groundCheck.position.y + groundCheckRadius), new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckRadius));
		for (int i = 0; i < colliders.Length; i++) {
			if (colliders[i].gameObject != gameObject)
				grounded = true;
		}

		if (jumpPreparation) {
																													//TODO: jump prepataion animation
		}

		Move(jumping, turnAround);
		jumping = false;

		grounded = false;
		turnAround = false;
	}

	public void Move(bool jumping, bool turnAround) {																		//MOVE

		//only control the player  if grounded or airControl is turned on
		if ((grounded || airControl) && moving && !jumpPreparation) {
			
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
			grounded = false;
			rb2D.AddForce(jumpVector * jumpForce);
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
