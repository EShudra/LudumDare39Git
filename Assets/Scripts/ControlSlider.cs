using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Control slider.
/// Abstract class. Controls slider selection highlight.
/// Slider On/Off state, including visualisation
/// </summary>

public abstract class ControlSlider : MonoBehaviour, IMoveHandler, IDragHandler {//, ISelectHandler, IDeselectHandler {

	//indiacator game object
	public GameObject indicator;
	//indicator sprite renderer
	SpriteRenderer iRend;

	//indicator on/off sprites
	public Sprite powerOnSprite;
	public Sprite powerOffSprite;

	//wires which connects power bank with sliner
	//use switchOn() switchOff() for visualisation (WireSwitcher component)
	public GameObject[] wiresArr;

	// Use this for initialization
	public virtual void Awake () {
		iRend = indicator.GetComponent<SpriteRenderer> ();
	}

	//update property on start from slider
	void Start(){
		SetPlayerProperty (this.GetComponent<Slider>().value);
	}

	//listen inoput axis. Switch slider power state when "true"
	public virtual void Update(){
		if (GetInputAxis ()) {
			PowerSwitch ();
		}
	}

	//update property when slider moved with arrow keys
	public void OnMove(UnityEngine.EventSystems.AxisEventData data){
		//Debug.Log (this.GetComponent<Slider>().value);
		SetPlayerProperty (this.GetComponent<Slider>().value);

	}

	//update property when slider dragged with mouse
	public void OnDrag(UnityEngine.EventSystems.PointerEventData data){
		//Debug.Log (this.GetComponent<Slider>().value);
		SetPlayerProperty (this.GetComponent<Slider>().value);

	}

	//turn slider state to ON
	public void PowerOn(){
		iRend.sprite = powerOnSprite;
		EnablePlayerProperty (true);
		foreach (var item in wiresArr) {
			if (item.GetComponent<WireSwitcher> () != null) {
				item.GetComponent<WireSwitcher> ().switchOn();
			}
		}
	}

	//turn slider state to OFF
	public void PowerOff(){
		iRend.sprite = powerOffSprite;
		EnablePlayerProperty (false);
		foreach (var item in wiresArr) {
			if (item.GetComponent<WireSwitcher> () != null) {
				item.GetComponent<WireSwitcher> ().switchOff();
			}
		}
	}

	//switch slider state ON/OFF
	public void PowerSwitch(){
		if (iRend.sprite == powerOffSprite) {
			PowerOn ();
		} else if (iRend.sprite == powerOnSprite) {
			PowerOff ();
		}
	}

	//set player property
	public abstract void SetPlayerProperty (float value);

	//enable player property
	public abstract void EnablePlayerProperty (bool state);

	//get axis which controls player property
	public abstract bool GetInputAxis ();
}
