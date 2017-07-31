using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogButton : MonoBehaviour {


	//Drag&drop elements
	public GameObject currentPanel;
	public GameObject nextPanel;
	public Text btnText;

	public GameObject[] whatToEnable; //game object to enable sliders
	public GameObject[] whatToDisable;

	//audio clip
	private AudioSource audio;

	//audio length in seconds
	public float audioTime = 2f;
	private float startTime = 0f;

	//if last pane
	private bool lastPanel = false;

	void Start () {
		//init start time
		startTime = Time.time;

		//init interactable
		this.GetComponent<Button> ().interactable = false;

		//init audio clip
		audio = GetComponent<AudioSource>();

		//play sound or set to play auto
		//=========

		//check if panel is last
		lastPanel = (nextPanel == null);


		StartCoroutine (makeButtonInteractable ());


	}

	IEnumerator makeButtonInteractable(){
		yield return new WaitForSeconds (audioTime);
		this.GetComponent<Button> ().interactable = true;
		Color colorTxt = btnText.color;
		colorTxt.a = 1f;
		btnText.color = colorTxt;

	}

	void goToNextPanel(){
		//audio.Stop ();
		currentPanel.SetActive (false);
		if (!lastPanel) {
			nextPanel.SetActive (true);
		} else {
			//PlayerMovement pm = GameObject.FindWithTag ("Player").GetComponent<PlayerMovement> ();

			/*if (pm != null) {
				pm.enabled = true;
			} else {
				Debug.LogError ("Player object is not found [DIALOG_BUTTON.CS]");
			}*/

			//GameObject slidersAll = GameObject.FindWithTag ("SlidersRoot");

			foreach (var item in whatToDisable) {
				item.SetActive (false);
			}

			foreach (var item in whatToEnable) {
				item.SetActive (true);
			}

		}
	}

	public void OnClick(){
		goToNextPanel ();
	}

}
