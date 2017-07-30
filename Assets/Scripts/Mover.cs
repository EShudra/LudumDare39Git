using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	[Header("Set X position to set speed")]
	public Vector3 moveVector;

	void Update () {
		this.transform.Translate (moveVector*Time.deltaTime);
	}
}
