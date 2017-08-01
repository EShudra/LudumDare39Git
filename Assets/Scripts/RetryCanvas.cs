using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryCanvas : MonoBehaviour {

	public GameObject[] whatToShow;
	public GameObject[] whatToHide;

	// Use this for initialization
	void Start () {
		StartCoroutine (searchForPlayer ());
	}

	IEnumerator searchForPlayer(){
		while (true) {
			yield return new WaitForSeconds (1);
			GameObject pl = GameObject.FindGameObjectWithTag ("Player");
			Debug.Log (pl);
			if (pl == null) {
				foreach (var item in whatToShow) {
					item.SetActive (true);
				}
				foreach (var item in whatToHide) {
					item.SetActive (false);
				}
			}
		}
	}

}
