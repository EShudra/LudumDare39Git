using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowItemsOnContact : MonoBehaviour {

	[SerializeField] private GameObject[] objects;

	private void Awake() {
		SetStates(false);
	}

	private void OnTriggerEnter2D(Collider2D col) {
		SetStates(true);
	}

	private void SetStates(bool state) {
		foreach (GameObject go in objects) {
			go.SetActive(state);
		}
	}
}
