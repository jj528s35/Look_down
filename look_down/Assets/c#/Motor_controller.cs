using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fofulab;

public class Motor_controller : MonoBehaviour
{
    public OutputPin pin;
    public int value;
    public PinType type;
    public int HMIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown("space"))
        {
            print("send message");
            HapticMakerManager.setValue(HMIndex, pin, value, type);
        }
    }

    public void ButtomOnClick_90()
    {
        value = 90;
        print("send message 90");
        HapticMakerManager.setValue(HMIndex, pin, value, type);
    }

    public void ButtomOnClick_0()
    {
        value = 0;
        print("send message 0");
        HapticMakerManager.setValue(HMIndex, pin, value, type);
    }
}
