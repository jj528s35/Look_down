using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class show_fingertips : MonoBehaviour
{
    private MRTouchServer2 server;
    private ZEDManager zedManager;
    private Camera LeftCamera;

     [Header("MRTouch Coordinate")]
    public int mrWidth = 672;
    public int mrHeight = 376;

    public GameObject[] fingertips;

   
    
    // Start is called before the first frame update
    void Start()
    {
        server = GetComponent<MRTouchServer2>();
        zedManager = ZEDManager.GetInstance(sl.ZED_CAMERA_ID.CAMERA_ID_01);
        LeftCamera = zedManager.GetLeftCameraTransform().gameObject.GetComponent<Camera>();
        print(LeftCamera.pixelWidth + " " + LeftCamera.pixelHeight);

        for(int i=0; i<10; i++)
        {
            fingertips[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
         for(int i=0;i<server.fingerNumber;i++)
        {
            if (server.touchedState[i])
            {
                //Debug.LogFormat("touch index: {0}", i);
                Vector3 worldpos;
                Vector2 unityPos = MRTouchToUnityWorldPosition(server.fingertips[i]);
                ZEDSupportFunctions.GetWorldPositionAtPixel(zedManager.zedCamera, unityPos, LeftCamera,out worldpos);
                //print(unityPos + ""+ worldpos);
                fingertips[i].SetActive(true);
                fingertips[i].transform.position = worldpos;
                
            }
            else
            {
                fingertips[i].SetActive(false);
            }
        }

    }


    Vector2 MRTouchToUnityWorldPosition(Vector2 touchedPos)
    {
        
        Vector2 normalizeTouchedPos = new Vector2(touchedPos.x / mrWidth, touchedPos.y / mrHeight);
        Debug.LogFormat("touch pos: ({0}, {1})", touchedPos.x, touchedPos.y);
        Debug.LogFormat("normalize touch pos: ({0}, {1})", normalizeTouchedPos.x, normalizeTouchedPos.y);
        Vector2 localPos = new Vector2(normalizeTouchedPos.x * Screen.width, Screen.height + normalizeTouchedPos.y * -Screen.height);
        //Debug.LogFormat("loca pos: ({0}, {1}, {2})", localPos.x, localPos.y, localPos.z);
        return localPos;
    }
}
