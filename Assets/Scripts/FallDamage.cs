using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour {

	[SerializeField] private float rigidbodyDamageBound = -10f;
	[SerializeField] private float fallDamageMultiplier = 0.1f;

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
	}
	
	// Update is called once per frame
	void Update () {
		if (rb2D.velocity.y < rigidbodyDamageBound) {
			gotDamage = true;
			fallDamage = player.fallDamage + (-rb2D.velocity.y - rigidbodyDamageBound) * fallDamageMultiplier;
		}
	}
	
	private void OnTriggerEnter2D(Collider2D collision) {
		if (gotDamage) {
			Debug.Log("You got hit by " + fallDamage + ". Are you still alive, bruh?");
			player.Hit(fallDamage);
			gotDamage = false;
		}
	}
}
