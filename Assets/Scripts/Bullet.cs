using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	[SerializeField] private Transform hitParticles;

	private AudioSource bulletEngine;
	[SerializeField] private AudioClip enemyDamage;
	[SerializeField] private AudioClip playerDamage;

	[HideInInspector] public float damage;

	private void Awake() {
		bulletEngine = GetComponent<AudioSource>();

		if (bulletEngine == null) {
			Debug.LogError("No AudioSource found attached to the bullet! [BULLET.CS]");
		}
	}

	private void OnTriggerEnter2D(Collider2D col) {
		//Debug.Log("OnTrigger");
		if (col.CompareTag("Player")) {

			Player player = col.GetComponent<Player>();

			if (player == null) {
				Debug.LogError("No Player component found attached to the player! [BULLET.CS]");
			}

			player.Hit(damage);
			bulletEngine.PlayOneShot(playerDamage);
			Debug.Log("HIT THE PLAYER!!!!!!");
			DestroyBullet();

		} else if (col.CompareTag("Wall") || col.CompareTag("DestroyableWall")) {

			Debug.Log("HIT THE WALL!!!!!!");
			DestroyBullet();

		} else if (col.CompareTag("Enemy")) {

			Enemy enemy = col.GetComponent<Enemy>();

			if (enemy == null) {
				Debug.LogError("No Enemy component found attached to the enemy! [BULLET.CS]");
			}

			enemy.Hit(damage);
			bulletEngine.PlayOneShot(enemyDamage);
			DestroyBullet();
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
			bulletEngine.PlayOneShot(playerDamage);
			Debug.Log("HIT THE PLAYER!!!!!!");
			DestroyBullet();
		} else if (col.collider.CompareTag("Wall") || col.collider.CompareTag("DestroyableWall")) {
			Debug.Log("HIT THE WALL!!!!!!");
			DestroyBullet();
		} else if (col.collider.CompareTag("Enemy")) {
			Enemy enemy = col.collider.GetComponent<Enemy>();

			if (enemy == null) {
				Debug.LogError("No Enemy component found attached to the enemy! [BULLET.CS]");
			}

			enemy.Hit(damage);
			bulletEngine.PlayOneShot(enemyDamage);
			DestroyBullet();
		}
	}

	private void DestroyBullet () {
		Instantiate(hitParticles, transform.position, transform.rotation);
		GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<Collider2D>().enabled = false;
		Destroy(gameObject, 0.5f);
	}
}
