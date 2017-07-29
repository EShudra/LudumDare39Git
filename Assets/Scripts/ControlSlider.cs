using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Control slider.
/// Abstract class. Controls slider selection highlight.
/// </summary>

public abstract class ControlSlider : MonoBehaviour, IMoveHandler, IDragHandler {//, ISelectHandler, IDeselectHandler {

	public GameObject indicator;
	SpriteRenderer iRend;

	public Sprite powerOnSprite;
	public Sprite powerOffSprite;

	public GameObject[] wiresArr;

	// Use this for initialization
	public virtual void Awake () {
		iRend = indicator.GetComponent<SpriteRenderer> ();
	}

	void Update(){
		if (getInputAxis ()) {
			powerSwitch ();
		}
	}

	public void OnMove(UnityEngine.EventSystems.AxisEventData data){
		//Debug.Log (this.GetComponent<Slider>().value);
		setPlayerProperty (this.GetComponent<Slider>().value);

	}

	public void OnDrag(UnityEngine.EventSystems.PointerEventData data){
		//Debug.Log (this.GetComponent<Slider>().value);
		setPlayerProperty (this.GetComponent<Slider>().value);

	}

	public void powerOn(){
		iRend.sprite = powerOnSprite;
		enablePlayerProperty (true);
		foreach (var item in wiresArr) {
			if (item.GetComponent<WireSwitcher> () != null) {
				item.GetComponent<WireSwitcher> ().switchOn();
			}
		}
	}

	public void powerOff(){
		iRend.sprite = powerOffSprite;
		enablePlayerProperty (false);
		foreach (var item in wiresArr) {
			if (item.GetComponent<WireSwitcher> () != null) {
				item.GetComponent<WireSwitcher> ().switchOff();
			}
		}
	}

	public void powerSwitch(){
		if (iRend.sprite == powerOffSprite) {
			powerOn ();
		} else if (iRend.sprite == powerOnSprite) {
			powerOff ();
		}
	}

	//set player property
	public abstract void setPlayerProperty (float value);

	//enable player property
	public abstract void enablePlayerProperty (bool state);

	//get axis which controls player property
	public abstract bool getInputAxis ();
}
