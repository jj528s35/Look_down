using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {
	public Transform Target;
	public float speed;
	void Start () {
		Target.position = GameObject.Find("Element").transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(Target.position, Vector3.up, speed * Time.deltaTime);
	}
}
