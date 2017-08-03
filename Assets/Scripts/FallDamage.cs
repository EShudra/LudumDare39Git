using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour {

	//[SerializeField] private float rigidbodyDamageBound = -10f;
	[SerializeField] private float raiseFallDamageSpeed = 10f;
	[SerializeField] private float fallDamageMul = 3f;

	[SerializeField] private float fallDamageThreshold = 0f;

	private float fallDamage;
	private bool gotDamage = false;

	private Player player;
	private Rigidbody2D rb2D;

	// Use this for initialization
	void Start () {
		player = GetComponentInParent<Player>();
		rb2D = GetComponentInParent<Rigidbody2D>();

		if (player == null) {
			Debug.LogError("No player component attached to the parent object found! [FALL_DAMAGE.CS]");
		}

		if (rb2D == null) {
			Debug.LogError("No Rigidbody2D component attached to the parent object found! [FALL_DAMAGE.CS]");
		}

		lastY = player.transform.position.y;
	}

	private float lastY;

	/*
	// Update is called once per frame
	void Update () {
		if (rb2D.velocity.y < rigidbodyDamageBound) {
			gotDamage = true;
			fallDamage = player.fallDamage + (-rb2D.velocity.y - rigidbodyDamageBound) * fallDamageMultiplier;
		}
	}

	/*
	private void OnTriggerEnter2D(Collider2D collision) {
		if (gotDamage) {
			Debug.Log("You got hit by " + fallDamage + ". Are you still alive, bruh?");
			player.Hit(fallDamage);
			gotDamage = false;
		}
	}*/

	void FixedUpdate(){
		if ((player.transform.position.y >= lastY) && (fallDamage > fallDamageThreshold)) {
			float correctDamage = fallDamageThreshold + (fallDamage - fallDamageThreshold)*fallDamageMul;
			Debug.Log("You have fallen. Damage: " + correctDamage);
			player.Hit(correctDamage);
		}

		fallDamage = (lastY - player.transform.position.y) * raiseFallDamageSpeed;

		lastY = player.transform.position.y;
	}

}
