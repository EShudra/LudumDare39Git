using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour {

	private Image hpImage;

	// Use this for initialization
	void Awake () {
		hpImage = GetComponent<Image> ();

		if (hpImage == null) {
			Debug.LogError ("Hp bar Image is missing [HP_BAR.CS]");
		}
	}

	//update hp bar
	public void updateBar(float currHP, float maxHP){
		currHP = Mathf.Clamp (currHP, 0f, maxHP);
		hpImage.fillAmount = currHP / maxHP;
	}
}
