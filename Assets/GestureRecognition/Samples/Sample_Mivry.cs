using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class Sample_Mivry : MonoBehaviour
{
    public Text HUDText = null;

    // Start is called before the first frame update
    void Start()
    {
        // Hide unused models in the scene
        GameObject controller_oculus_left = GameObject.Find("controller_oculus_left");
        GameObject controller_oculus_right = GameObject.Find("controller_oculus_right");
        GameObject controller_vive_left = GameObject.Find("controller_vive_left");
        GameObject controller_vive_right = GameObject.Find("controller_vive_right");
        GameObject controller_microsoft_left = GameObject.Find("controller_microsoft_left");
        GameObject controller_microsoft_right = GameObject.Find("controller_microsoft_right");
        GameObject controller_dummy_left = GameObject.Find("controller_dummy_left");
        GameObject controller_dummy_right = GameObject.Find("controller_dummy_right");

        controller_oculus_left.SetActive(false);
        controller_oculus_right.SetActive(false);
        controller_vive_left.SetActive(false);
        controller_vive_right.SetActive(false);
        controller_microsoft_left.SetActive(false);
        controller_microsoft_right.SetActive(false);
        controller_dummy_left.SetActive(false);
        controller_dummy_right.SetActive(false);

        var input_devices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(input_devices);
        string input_device = "";
        foreach (var device in input_devices)
        {
            if (device.characteristics.HasFlag(InputDeviceCharacteristics.HeadMounted))
            {
                input_device = device.name;
                break;
            }
        }

        if (input_device.Length >= 6 && input_device.Substring(0, 6) == "Oculus")
        {
            controller_oculus_left.SetActive(true);
            controller_oculus_right.SetActive(true);
        }
        else if (input_device.Length >= 4 && input_device.Substring(0, 4) == "Vive")
        {
            controller_vive_left.SetActive(true);
            controller_vive_right.SetActive(true);
        }
        else if (input_device.Length >= 4 && input_device.Substring(0, 4) == "DELL")
        {
            controller_microsoft_left.SetActive(true);
            controller_microsoft_right.SetActive(true);
        }
        else // 
        {
            controller_dummy_left.SetActive(true);
            controller_dummy_right.SetActive(true);
        }
        if (HUDText == null)
        {
            Debug.Log("Press trigger and perform gesture.");
        } else
        {
            HUDText.text = "Press trigger and perform gesture.";
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnGestureCompleted(GestureCompletionData data)
    {
        if (HUDText == null)
        {
            Debug.Log($"Identified gesture {data.gestureName} ({data.gestureID}).");
        } else
        {
            HUDText.text = $"Identified gesture {data.gestureName} ({data.gestureID}).";
        }
    }
}
