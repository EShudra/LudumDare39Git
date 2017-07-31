using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitFromLevel : MonoBehaviour {

	public GameObject[] whatToShowOnLevelEnd;
	public float loadDelay = 3;

	public GameObject[] whatToShowOnGameEnd;

	private bool onceLoadFlag = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionStay2D(Collision2D other){
		//Debug.Log ("!!!!!");
		if (other.collider.CompareTag ("Player")) {
			ExitPointReached ();
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.CompareTag ("Player")) {
			ExitPointReached ();
		}

	}

	public void ExitPointReached(){
		if (onceLoadFlag) {
			if (SceneManager.GetActiveScene ().buildIndex < SceneManager.sceneCountInBuildSettings - 1) {
				foreach (var item in whatToShowOnLevelEnd) {
					item.SetActive (true);
				}
			}
			StartCoroutine (loadNextLevelCouroutine ());
		}
		onceLoadFlag = false;
	}

	IEnumerator loadNextLevelCouroutine(){
		yield return new WaitForSeconds (loadDelay);
		Debug.Log (SceneManager.GetActiveScene().buildIndex);
		Debug.Log (SceneManager.sceneCountInBuildSettings - 1);
		if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		} else {
			//end game!!
			foreach (var item in whatToShowOnGameEnd) {
				item.SetActive (true);
			}
		}
	}
}
