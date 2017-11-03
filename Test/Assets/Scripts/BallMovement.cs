using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class BallMovement : MonoBehaviour {
	
	private Coordinates coords;
	public string traectoryFileName;
	private string _path;
	private string _jsonString;
	private LineRenderer _traectroy;
	private int _currrnetpointNumber;
	private int _add;
	public bool isMoving;
	const float _doubleClickTime = 0.3f;
	private float _lastClickTime;
	private int _lenght;

	void Start () {
		_traectroy = GetComponent<LineRenderer> ();
		_add = 1;
		SetPathCoordinates ();
		Vector3 pos = new Vector3 ((float)coords.x [0], (float)coords.y [0], (float)coords.z [0]);
		transform.position = pos;
		_lenght = coords.x.Length;
	}

	void FixedUpdate () {
		if (isMoving) {
			if ((_currrnetpointNumber < _lenght && _add == 1) || (_currrnetpointNumber >= 0 && _add == -1)) {
				Vector3 pos = new Vector3 ((float)coords.x [_currrnetpointNumber], (float)coords.y [_currrnetpointNumber], (float)coords.z [_currrnetpointNumber]);
				transform.position = pos;
				if (_add == 1) {
					_traectroy.positionCount = _currrnetpointNumber + 1;
					_traectroy.SetPosition (_currrnetpointNumber, pos);
				} else {
					_traectroy.positionCount = _lenght - _currrnetpointNumber;
					_traectroy.SetPosition (_lenght - _currrnetpointNumber - 1, pos);
				}
				_currrnetpointNumber += _add;
				if ((_currrnetpointNumber == _lenght && _add == 1) || (_currrnetpointNumber == -1 && _add == -1)) {
					_add *= -1;
					_currrnetpointNumber += _add;
					isMoving = false;
					_traectroy.positionCount = 0;
				}
			}
		}
	}

	void SetPathCoordinates(){
		_path = Application.streamingAssetsPath + "/" + traectoryFileName;
		_jsonString = File.ReadAllText (_path);
		coords = JsonUtility.FromJson<Coordinates> (_jsonString);
	}

	void OnMouseDown(){
		Debug.Log (Time.time);
		isMoving = true;
		if ((Time.time - _lastClickTime) <= _doubleClickTime * Time.timeScale) {
			Vector3 pos = new Vector3 ((float)coords.x [0], (float)coords.y [0], (float)coords.z [0]);
			transform.position = pos;
			_traectroy.positionCount = 0;
			isMoving = false;
			_currrnetpointNumber = 0;
			_add = 1;
		}
		_lastClickTime = Time.time;
	}
}
