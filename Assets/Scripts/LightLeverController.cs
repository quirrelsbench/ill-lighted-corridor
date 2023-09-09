using Oculus.Interaction.Input;
using Oculus.Interaction.OVR.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Animations;

public class LightLeverController : MonoBehaviour
{
    public GameObject[] lights;
    List<InputDevice> inputDevices = new List<InputDevice>();
    private InputDevice rightHandDevice;
    private InputDevice leftHandDevice;
    private bool inCollider = false;

    private void Start()
    {
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Controller, inputDevices);

        if (inputDevices.Count > 0)
        {
            leftHandDevice = inputDevices[0];
        }
        if (inputDevices.Count > 1)
        {
            rightHandDevice = inputDevices[1];
        }
    }

    void Update()
    {
        leftHandDevice.TryGetFeatureValue(CommonUsages.trigger, out float rightTriggerValue);
        if (rightTriggerValue > 0.1f && inCollider)
        {
            lights[0].transform.position = new Vector3(0.5f, 0.5f, 1.0f);
        }

        rightHandDevice.TryGetFeatureValue(CommonUsages.trigger, out float leftTriggerValue);
        if (leftTriggerValue > 0.1f && inCollider)
        {
            lights[0].transform.position = new Vector3(1.0f, 0.5f, 1.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        inCollider = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inCollider = false;
    }
}
