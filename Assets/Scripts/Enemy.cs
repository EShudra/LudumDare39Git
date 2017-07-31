using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[System.Serializable]
	public class EnemyStats {

		public float maximumHealth = 100f;
		private float healthAmount_ = 0f;

		public float healthAmount {
			get { return healthAmount_; }
			set { healthAmount_ = Mathf.Clamp(value, 0, maximumHealth); }
		}

		public EnemyStats() {
			this.healthAmount = maximumHealth;
		}

		public EnemyStats(float healthToSet) {
			this.healthAmount = healthToSet;
		}
	}

	[SerializeField] private EnemyStats stats;

	public void Hit(float damage) {                 //Hit Enemy with some damage
		stats.healthAmount -= damage;
		Debug.Log("Turret got hit with " + damage + " damage. Current health - " + stats.healthAmount + ".");

		if (stats.healthAmount <= 0) {
			Debug.Log("Turret destroyed!");
			Destroy(gameObject);
		}
	}
}
