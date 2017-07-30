using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {

	public float destroyDelay = 10f;

	void Start () {
		Destroy(this.gameObject, destroyDelay);
	}
}