using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement: MonoBehaviour {
	
	public float movementSpeed = 10f;                                   // The speed the player can travel in the x axis.
	public float maximumMovementSpeed = 12f;
	public float minimumMovementSpeed = 0f;

	public float jumpForce = 500f;                                      // Amount of force added when the player jumps.
	public float maximumJumpForce = 1500f;
	public float minimumJumpForce = 0f;
	public Vector3 jumpVector = Vector3.zero;

	[SerializeField] private bool airControl = false;                 // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask whatIsGround;                  // A mask determining what is ground to the character

	private GameObject groundCheck;    // A position marking where to check if the player is grounded.
	private BoxCollider2D grCheckBoxCollider;
	private const float groundCheckRadius = 0.25f; // Radius of the overlap circle to determine if grounded
	private bool grounded;            // Whether or not the player is grounded.
	[HideInInspector] public Rigidbody2D rb2D;		//Reference to the rigidbody2D

	[HideInInspector] public bool jumping; // For determining when the player is jumping.
	[HideInInspector] public bool facingRight = false;  // For determining which way the player is currently facing.
	[HideInInspector] public bool moving = false;       // For determining if the player is currently moving
	[HideInInspector] public bool jumpPreparation = false;  // For determining if the player is preparing to jump.
	[HideInInspector] public bool turnAround = false;

	[SerializeField] private Transform jumpParticlesPrefab;

	//use to detect when is jump mode wat changed
	public bool jumpModeIsChanged = false;

	/*
	private Vector3 lastPos;//use to check player offset from last frame
	private Vector3 horizontalMovingThreshold = new Vector3 (0.2f*Time.fixedDeltaTime,0,0);

	bool isMovingVertical = false;*/

	private void Awake() {
		
		// Setting up references.
		groundCheck = GameObject.Find("GroundCheck");
		rb2D = GetComponent<Rigidbody2D>();

		if (groundCheck == null) {
			Debug.LogError("No GroundCheck object found! [PLAYER_MOVEMENT.CS]");
		}

		if (rb2D == null) {
			Debug.LogError("No Rigidbody2D component found! [PLAYER_MOVEMENT.CS]");
		}

		grCheckBoxCollider = groundCheck.GetComponent<BoxCollider2D> ();

		if (grCheckBoxCollider == null) {
			Debug.LogError("No BoxCollider2D component found! [PLAYER_MOVEMENT.CS]");
		}

		//lastPos = transform.position;
	}


	private void FixedUpdate() {																										//FIXED UPDATE
		// Turn around if the correct key was pressed
		if (turnAround) {
			Flip();
		}
		turnAround = false;

		/* The player is grounded if a boxcast of the groundcheck boxCollider2D hits anything designated as ground
		   This can be done using layers instead but Sample Assets will not overwrite your project settings. */
		grounded = false;
		RaycastHit2D[] hits = Physics2D.BoxCastAll(groundCheck.transform.position, grCheckBoxCollider.size, 0f, new Vector2(0f,0f), 0f, whatIsGround);

		for (int i = 0; i < hits.Length; i++) {
			if (hits[i].transform.gameObject != gameObject)
				grounded = true;
		}

		if (jumpPreparation) {
																													//TODO: jump prepataion animation
		}

		Move(jumping, turnAround);
		jumping = false;

		grounded = false;

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
			
		// Jump, if the correct key was pressed
		if (grounded && jumping) {
			grounded = false;
			rb2D.AddForce(jumpVector * jumpForce);
			if (jumpVector.x != 0f && jumpVector.y != 0f && jumpForce != 0f) {
				Instantiate(jumpParticlesPrefab, transform.position, transform.rotation);
			}
				
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

	//use to check if jump mode changed
	public bool jumpModeChanged(){
		bool state;
		state = jumpModeIsChanged;
		jumpModeIsChanged = false;
		return state;
	}
}
