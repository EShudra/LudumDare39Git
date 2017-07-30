using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	[SerializeField] private Transform hitParticles;

	private Player player;
	private TurretBulletSpawn tbs;

	private float damage;

	private void Awake() {
		tbs = GetComponentInParent<TurretBulletSpawn>();

		if (tbs == null) {
			Debug.LogError("No TurretBulletSpawn component attached to the barrel! [BULLET.CS]");
		}

		damage = tbs.damage;
	}

	private void OnCollisionEnter2D(Collision2D col) {
		Debug.Log("OnCollision");
		if (col.collider.CompareTag("Player")) {
			player = col.collider.GetComponent<Player>();

			if (player == null) {
				Debug.LogError("No Player component found attached to the player! [BULLET.CS]");
			}

			player.Hit(damage);
			Debug.Log("HIT THE PLAYER!!!!!!");
			DestroyBullet();
		} else if (col.collider.CompareTag("Wall")) {
			Debug.Log("HIT THE WALL!!!!!!");
			DestroyBullet();
		}
	}

	private void OnTriggerEnter2D(Collider2D col) {
		Debug.Log("OnTrigger");
		if (col.CompareTag("Player")) {
			player = col.GetComponent<Player>();

			if (player == null) {
				Debug.LogError("No Player component found attached to the player! [BULLET.CS]");
			}

			player.Hit(damage);
			Debug.Log("HIT THE PLAYER!!!!!!");
			DestroyBullet();
		} else if (col.CompareTag("Wall")) {
			Debug.Log("HIT THE WALL!!!!!!");
			DestroyBullet();
		}
	}

	private void DestroyBullet () {
		Instantiate(hitParticles, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}
