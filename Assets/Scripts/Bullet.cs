using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	[SerializeField] private Transform hitParticles;

	[HideInInspector] public float damage;

	private void OnTriggerEnter2D(Collider2D col) {
		//Debug.Log("OnTrigger");
		if (col.CompareTag("Player")) {
			Player player = col.GetComponent<Player>();

			if (player == null) {
				Debug.LogError("No Player component found attached to the player! [BULLET.CS]");
			}

			player.Hit(damage);
			Debug.Log("HIT THE PLAYER!!!!!!");
			DestroyBullet();
		} else if (col.CompareTag("Wall")) {
			Debug.Log("HIT THE WALL!!!!!!");
			DestroyBullet();
		} else if (col.CompareTag("Enemy")) {
			Enemy enemy = col.GetComponent<Enemy>();

			if (enemy == null) {
				Debug.LogError("No Enemy component found attached to the enemy! [BULLET.CS]");
			}

			enemy.Hit(damage);
		}
	}

	private void OnCollisionEnter2D(Collision2D col) {
		//Debug.Log("OnCollision");
		if (col.collider.CompareTag("Player")) {

			Player player = col.collider.GetComponent<Player>();

			if (player == null) {
				Debug.LogError("No Player component found attached to the player! [BULLET.CS]");
			}

			player.Hit(damage);
			Debug.Log("HIT THE PLAYER!!!!!!");
			DestroyBullet();

		} else if (col.collider.CompareTag("Wall")) {

			Debug.Log("HIT THE WALL!!!!!!");
			DestroyBullet();

		} else if (col.collider.CompareTag("Enemy")) {

			Enemy enemy = col.collider.GetComponent<Enemy>();

			if (enemy == null) {
				Debug.LogError("No Enemy component found attached to the enemy! [BULLET.CS]");
			}

			enemy.Hit(damage);
			Debug.Log("HIT THE ENEMY!!!!!!");
			DestroyBullet();
		}
	}

	private void DestroyBullet () {
		Instantiate(hitParticles, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}
