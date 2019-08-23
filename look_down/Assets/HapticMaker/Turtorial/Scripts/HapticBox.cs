using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticBox : MonoBehaviour {
	public bool select;
	public Transform Target;
	float speed;
	float smooth;
	// Use this for initialization
	void Start () {
		select = false;
		speed = 0.35f;
		smooth = 5.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (!select) {
			transform.Rotate(Vector3.up*speed);
			transform.position = Vector3.Lerp(transform.position, Target.position, 0.1f);
		}
		else {
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity,  Time.deltaTime * smooth);
			
		}
	}
}
