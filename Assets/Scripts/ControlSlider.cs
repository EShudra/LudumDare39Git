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
	[SerializeField] private GameObject indicator = null;
	//indicator sprite renderer
	private SpriteRenderer iRend;
	//indicator on/off sprites
	[SerializeField] private Sprite indicatorOnSprite = null;
	[SerializeField] private Sprite indicatorOffSprite = null;

	//wire game object
	[SerializeField] private GameObject wire = null;

	//slider fill game object
	private RectTransform sliderFill;
	//sprite fill sprite renderer
	private Image fillImg;
	//fill bar transparency when off
	[SerializeField] private float fillAlpha = 0.35f;

	//power bar object
	[SerializeField] private GameObject powerBar = null;
	protected PowerBar pBar;
	//how much power cost per use or per second
	[SerializeField] protected float powerCost;

	// Use this for initialization
	public virtual void Awake () {
		iRend = indicator.GetComponent<SpriteRenderer> ();

		pBar = powerBar.GetComponent<PowerBar> ();

		if (pBar == null) {
			Debug.LogError ("Cannot find Power Bar [CONTROL_SLIDER.CS]");
		}
	}

	//update property on start from slider
	void Start(){
		SetPlayerProperty (getSliderValue());
		sliderFill = GetComponent<Slider> ().fillRect;
		fillImg = sliderFill.GetComponent<Image> ();

		if (sliderFill == null) {
			Debug.LogError ("Slider fill is missing [CONTROL_SLIDER.CS]");
		}

		if (fillImg == null) {
			Debug.LogError ("Cannot find Image of RectTransform [CONTROL_SLIDER.CS]");
		}

		//set img alpha
		SetSliderFillAlpha(fillAlpha);
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
		SetPlayerProperty (getSliderValue());

	}

	//update property when slider dragged with mouse
	public void OnDrag(UnityEngine.EventSystems.PointerEventData data){
		//Debug.Log (this.GetComponent<Slider>().value);
		SetPlayerProperty (getSliderValue());

	}

	public float getSliderValue(){
		return this.GetComponent<Slider> ().value / 10;
	}

	//turn slider state to ON
	public void PowerOn(){
		//set indicator ON
		iRend.sprite = indicatorOnSprite;

		//set img alpha
		SetSliderFillAlpha(1f);

		//on color glow
		wire.gameObject.SetActive(true);

		EnablePlayerProperty (true);

	}

	//turn slider state to OFF
	public void PowerOff(){
		//set indicator OFF
		iRend.sprite = indicatorOffSprite;

		//set img alpha
		SetSliderFillAlpha(fillAlpha);

		//off color glow
		wire.gameObject.SetActive(false);

		EnablePlayerProperty (false);

	}

	//switch slider state ON/OFF
	public void PowerSwitch(){
		if (iRend.sprite == indicatorOffSprite) {
			PowerOn ();
		} else if (iRend.sprite == indicatorOnSprite) {
			PowerOff ();
		}
	}

	void SetSliderFillAlpha(float fillAlpha){
		Color imgColor = fillImg.color;
		imgColor.a = fillAlpha;
		fillImg.color = imgColor;
	}

	//set player property
	public abstract void SetPlayerProperty (float value);

	//enable player property
	public abstract void EnablePlayerProperty (bool state);

	//get axis which controls player property
	public abstract bool GetInputAxis ();
}
