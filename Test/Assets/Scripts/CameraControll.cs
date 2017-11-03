using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour {
	public float minScrollDistance;
	public float maxScrollDistance;
	public GameObject[] balls;
	private int num;

	void Update () {
		Rotation ();
		Scroll ();
		ChangeBall ();
	}

	void Rotation(){
		if (Input.GetMouseButton(1)) {
			transform.RotateAround (transform.parent.position, Vector3.up, Input.GetAxis("Mouse X"));
			transform.RotateAround (transform.parent.position, transform.right, -Input.GetAxis("Mouse Y"));
		}
	}

	void Scroll(){
		if (Vector3.Distance (transform.position, transform.parent.position) <= minScrollDistance && Input.GetAxis ("Mouse ScrollWheel") > 0 
		|| Vector3.Distance (transform.position, transform.parent.position) >= maxScrollDistance && Input.GetAxis ("Mouse ScrollWheel") < 0)
		return;
		transform.position = transform.position - (transform.position - transform.parent.position) * Input.GetAxis ("Mouse ScrollWheel");
	}

	void ChangeBall(){
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			if (num == 0)
				num = 3;
			else
				num -= 1;
			Vector3 pos = transform.localPosition;
			transform.parent = balls[num].transform;
			transform.localPosition = pos;
			balls [num].GetComponent<BallMovement> ().isMoving = false;

		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			if (num == 3)
				num = 0;
			else
				num += 1;
			Vector3 pos = transform.localPosition;
			transform.parent = balls[num].transform;
			transform.localPosition = pos;
			balls [num].GetComponent<BallMovement> ().isMoving = false;
		}
	}
}
