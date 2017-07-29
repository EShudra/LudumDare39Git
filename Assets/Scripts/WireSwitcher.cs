using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireSwitcher : MonoBehaviour {

	public Sprite wireOn;
	public Sprite wireOff;

	SpriteRenderer sRend;

	void Awake () {
		sRend = this.GetComponent<SpriteRenderer> ();
	}
	
	public void switchOn(){
		Debug.Log ("test1");
		sRend.sprite = wireOn;
	}

	public void switchOff(){
		sRend.sprite = wireOff;
	}

}
