using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
//This is a script that is attached to the hand presence object.
public class HandPresence : MonoBehaviour
{   
    public InputDeviceCharacteristics controllerType;
    //This is a list of the hand objects.
    public List<GameObject> handObjects;
    private GameObject spawnedController;
    private InputDevice targetDevice;
    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        // InputDeviceCharacteristics rightCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(controllerType, devices);

        foreach (var device in devices)
        {
            Debug.Log(device.name + device.characteristics);
            
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject prefab = handObjects.Find(x => x.name == targetDevice.name);
            if (prefab)
            {
                spawnedController = Instantiate(prefab, transform);
                // hand.transform.localPosition = Vector3.zero;
                // hand.transform.localRotation = Quaternion.identity;
            }
            else
            {
                Debug.LogError("No prefab found for " + targetDevice.name);
                spawnedController = Instantiate(handObjects[0], transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);        
        if (primaryButtonValue)
        {
            Debug.Log("Primary button pressed");
        }
    }
}
