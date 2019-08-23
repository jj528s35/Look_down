using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using fofulab;

class HapticMakerMenuItems {

#if UNITY_EDITOR
    [MenuItem("GameObject/HapticMaker/Manager", false, 10)]
    public static void CreateManager() {
        if (Object.FindObjectOfType<HapticMakerManager>() != null) {
            Debug.LogWarning("Another HapticMaker Manager is exist.");
            return;
        }

        GameObject manager = new GameObject("HapticMakerManager");
        manager.AddComponent<HapticMakerManager>();
    }
#endif

}
