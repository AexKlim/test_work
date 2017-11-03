using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScaleChanging : MonoBehaviour {

	public void ChangeTimeScale(){
		Time.timeScale = gameObject.GetComponent<Slider>().value;
	}
}
