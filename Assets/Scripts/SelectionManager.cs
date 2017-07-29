using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionManager : MonoBehaviour {

	public GameObject selection;
	public Vector3 posOffset = new Vector3(0,0,0);

	EventSystem eSys;
	GameObject currGObj;

	// Use this for initialization
	void Awake () {
		eSys = this.GetComponent<EventSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (eSys.currentSelectedGameObject);
		if (eSys.currentSelectedGameObject == null) {
			//Debug.Log ("Selection is empty");
			if (currGObj != null) {
				eSys.SetSelectedGameObject (currGObj);
			}
		} else {
			currGObj = eSys.currentSelectedGameObject;
		}
		selection.transform.position = eSys.currentSelectedGameObject.transform.position;
		selection.transform.position += posOffset;
	}
		
}
