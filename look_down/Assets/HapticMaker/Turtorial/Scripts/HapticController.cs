using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticController : MonoBehaviour {
	public GameObject[] Elements;
	public int index;
	int LastIndex;
	// Use this for initialization
	void Start () {
		index = 0;
		LastIndex =0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)){
			LastIndex = index;
			if(index<Elements.Length-1){
				index++;
			}
			else {
				index = 0;
			}
		}
		
		if(index!=0){
			Elements[index].gameObject.GetComponent<HapticBox>().select = true;
			Elements[index].transform.position = 
				Vector3.Lerp(Elements[index].transform.position, gameObject.transform.position, 0.1f);
		}

		if(LastIndex!=0){
			Elements[LastIndex].gameObject.GetComponent<HapticBox>().select = false;
			Vector3 Target= Elements[LastIndex].gameObject.GetComponent<HapticBox>().Target.position;
			Elements[LastIndex].transform.position = 
				Vector3.Lerp(Elements[LastIndex].transform.position, Target, 0.1f);
		}
		
	}

	void OnTriggerEnter(Collider other)
    {
		
    }
}