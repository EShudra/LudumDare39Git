using System;
using UnityEngine;

public class CameraFollow: MonoBehaviour {
	[Header("Camera smoothing")]
	public float smoothing = 0.2f;

	[Header("Max/min camera shift")]
	[SerializeField] private float minXOffset = -1000f;
	[SerializeField] private float maxXOffset = 1000f;
	[SerializeField] private float minYOffset = 5.22f;
	[SerializeField] private float maxYOffset = 5.4f;

	private Transform target;

	private float offsetZ;
	private Vector3 lastTargetPosition;
	private Vector3 currentVelocity;
	private Vector3 lookAheadPos;

	private void Awake() {
		target = GameObject.FindGameObjectWithTag("Player").transform;

		if (target == null) {
			Debug.LogError("No target found for the camera!");
		}
	}

	// Use this for initialization
	private void Start() {
		lastTargetPosition = target.position;
		offsetZ = (transform.position - target.position).z;
		transform.parent = null;
	}


	// Update is called once per frame
	private void Update() {
		// only update lookahead pos if accelerating or changed direction
		float xMoveDelta = (target.position - lastTargetPosition).x;

		Vector3 aheadTargetPos = target.position + Vector3.forward * offsetZ;
		Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocity, smoothing);

		transform.position = new Vector3(Mathf.Clamp(newPos.x, minXOffset, maxXOffset), Mathf.Clamp(newPos.y, minYOffset, maxYOffset), newPos.z);

		lastTargetPosition = target.position;
	}
}
