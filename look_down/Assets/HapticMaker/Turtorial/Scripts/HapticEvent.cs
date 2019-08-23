using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fofulab;

public class HapticEvent : MonoBehaviour {

    public enum TriggerType {None, TriggerEnter, TriggerExit, TriggerStay, CollisionEnter, CollisionExit, CollisionStay, KeyDown, KeyUp, Key}
    public TriggerType triggerType;
    public KeyCode Key;
    public int HMIndex;
    public HapticParams[] Actions;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (triggerType == TriggerType.KeyDown) {
            if (Input.GetKeyDown(Key)) {
                InvokeHaptic(HMIndex);
            }
        }

        if (triggerType == TriggerType.KeyUp) {
            if (Input.GetKeyUp(Key)) {
                InvokeHaptic(HMIndex);
            }
        }

        if (triggerType == TriggerType.Key) {
            if (Input.GetKey(Key)) {
                InvokeHaptic(HMIndex);
            }
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if(triggerType == TriggerType.CollisionEnter)
            InvokeHaptic(HMIndex);
    }

    private void OnCollisionExit(Collision collision) {
        if (triggerType == TriggerType.CollisionExit)
            InvokeHaptic(HMIndex);
    }

    private void OnCollisionStay(Collision collision) {
        if (triggerType == TriggerType.CollisionStay)
            InvokeHaptic(HMIndex);
    }

    private void OnTriggerEnter(Collider other) {
        if (triggerType == TriggerType.TriggerEnter)
            InvokeHaptic(HMIndex);
    }

    private void OnTriggerExit(Collider other) {
        if (triggerType == TriggerType.TriggerExit)
            InvokeHaptic(HMIndex);
    }

    private void OnTriggerStay(Collider other) {
        if (triggerType == TriggerType.TriggerStay)
            InvokeHaptic(HMIndex);
    }

    public void InvokeHaptic(int index = 0) {
        if (Actions != null) {
            for (int i = 0; i < Actions.Length; i++) {
                if (Actions[i] == null)
                    continue;

                int v = (int)(Actions[i].value * (Actions[i].type == PinType.SERVO ? 180 : 255));
                HapticMakerManager.setValue(index, Actions[i].pin, v, Actions[i].type, Actions[i].curve, Actions[i].duration);
                if (Actions[i].changeDefaultValue) {
                    int dv = (int)(Actions[i].value * (Actions[i].type == PinType.SERVO ? 180 : 255));
                    HapticMakerManager.setDefaultValue(index, Actions[i].pin, dv, Actions[i].type);
                }
            }
        }
    }
}

[System.Serializable]
public class HapticParams {
    public OutputPin pin;
    [Range(0f, 1f)]
    public float value;
    public PinType type;
    public AnimationCurve curve;
    public float duration;

    public bool changeDefaultValue;
    [Range(0f, 1f)]
    public float defaultValue;
}
