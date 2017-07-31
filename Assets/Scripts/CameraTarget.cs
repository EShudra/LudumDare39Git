using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour {

	[SerializeField] private float horizontalOffset = 4f;

	private PlayerMovement pm;

	private void Awake() {
		pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

		if (pm == null) {
			Debug.LogError("No Player found, or no PlayerMovement component is attached to the player! [CAMERA_TARGET.CS]");
		}
	}

	private void Update() {
		if (pm != null) {
			transform.position = pm.transform.position + Vector3.right * horizontalOffset;
		}
	}
}
