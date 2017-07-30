using UnityEngine;
using System.Collections;

public class DuloRotation : MonoBehaviour {

	[System.Serializable]
	public enum TurretType {
		Top, Left, Bottom, Right
	}
	
	[SerializeField] private TurretType turretType = TurretType.Bottom;
	[SerializeField] private string whatToShoot;

	private Transform target;

	private float minRotationConstraint = 0f;
	private float maxRotationConstraint = 180f;

	void Awake () {
		target = GameObject.FindGameObjectWithTag(whatToShoot).transform;

		if (turretType == TurretType.Top) {
			minRotationConstraint = -180f;
			maxRotationConstraint = 0f;
		} else if (turretType == TurretType.Left) {
			minRotationConstraint = -90f;
			maxRotationConstraint = 90f;
		} else if (turretType == TurretType.Bottom) {
			minRotationConstraint = 0f;
			maxRotationConstraint = 180f;
		} else {
			minRotationConstraint = 90f;
			maxRotationConstraint = -90f;
		}
	}

	// Update is called once per frame
	void Update () {
		//subtracting the position  of the player from the mouse position
		Vector3 difference = target.position - transform.position;
		difference.Normalize ();	//normalizing the vector

		float rotationZ = Mathf.Atan2 (difference.y,difference.x) * Mathf.Rad2Deg; //find the angle in degrees


		if (minRotationConstraint == -90f && maxRotationConstraint == 90f) {																							//LEFT
			if (rotationZ < minRotationConstraint) {
				rotationZ = minRotationConstraint;
			} else if (rotationZ > maxRotationConstraint) {
				rotationZ = maxRotationConstraint;
			}
		} else if ((minRotationConstraint == -180f && maxRotationConstraint == 0f) || (minRotationConstraint == 0f && maxRotationConstraint == 180f)) {                 //TOP OR BOTTOM
			if (rotationZ < minRotationConstraint) {
				rotationZ = -minRotationConstraint;
			} else if (rotationZ > maxRotationConstraint) {
				rotationZ = maxRotationConstraint;
			}
		} else if (minRotationConstraint == 90f && maxRotationConstraint == -90f) {																						//RIGHT

		}

		transform.rotation = Quaternion.Euler (0f, 0f, rotationZ);
	}
}
