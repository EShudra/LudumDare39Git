using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableWall : MonoBehaviour {

	public float HP = 100;
	public Sprite[] states;
	public GameObject wallExplosionPrefab;

	private float currentHP;
	private SpriteRenderer rend;
	private int lastStateNum;

	// Use this for initialization
	void Start () {
		currentHP = HP;
		rend = this.GetComponent<SpriteRenderer> ();
		lastStateNum = states.Length - 1;
	}

	/*void OnCollisionEnter2D (Collision2D other){
		float damage = 0;
		Damage getDamage = other.collider.gameObject.GetComponent<Damage>();
		if (getDamage != null) {
			damage = getDamage.damage;
		}
		currentHP -= damage;
		UpdateState ();
	}

	void OnTriggerEnter2D (Collider2D other){
		float damage = 0;
		Damage getDamage = other.gameObject.GetComponent<Damage>();
		if (getDamage != null) {
			damage = getDamage.damage;
		}
		currentHP -= damage;
		UpdateState ();
	}*/

	void OnTriggerStay2D (Collider2D other){
		float damage = 0;
		if (other.CompareTag ("Drill")) {
			damage = other.gameObject.GetComponent<Drill> ().drillDPS*Time.fixedDeltaTime;
		}
		currentHP -= damage;
		UpdateState ();
	}


	void UpdateState (){
		if (currentHP <= 0) {
			Instantiate (wallExplosionPrefab, this.transform.position, wallExplosionPrefab.transform.rotation);
			Instantiate (wallExplosionPrefab, this.transform.position, wallExplosionPrefab.transform.rotation);
			Instantiate (wallExplosionPrefab, this.transform.position, wallExplosionPrefab.transform.rotation);
			Destroy (this.gameObject);
		} else {

			if (states.Length > 0) {
				int stateNum = states.Length - 1;
				stateNum = Mathf.FloorToInt (currentHP / HP * states.Length * 0.9999f);
				Debug.Log (stateNum);
				if (states [stateNum] != null) {
					rend.sprite = states [stateNum];
				} else {
					Debug.Log ("Wall state sprite is not assigned, but all still works!");
				}
				if (lastStateNum != stateNum) {
					Instantiate (wallExplosionPrefab, this.transform.position, wallExplosionPrefab.transform.rotation);
				}
				lastStateNum = stateNum;
			}
		}
	}


}
