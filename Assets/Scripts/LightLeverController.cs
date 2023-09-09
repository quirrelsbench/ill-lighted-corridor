using Oculus.Interaction.Input;
using Oculus.Interaction.OVR.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Animations;
using System;

public class LightLeverController : MonoBehaviour
{
    // event to interact with lights
    public static event Action leverFlipped;

    private Collider leverCollider;
    private List<InputDevice> inputDevices = new List<InputDevice>();
    private InputDevice rightHandDevice;
    private InputDevice leftHandDevice;
    private bool leftInCollider = false;
    private bool rightInCollider = false;
    private float disableTimer = 0.0f;
    private float timeBuffer = 1.0f;

    private void Start()
    {
        leverCollider = GetComponent<Collider>();

        // gets the left and right controllers
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
        // used to prevent constant light change due to trigger activation
        if (disableTimer > 0.0f)
        {
            disableTimer -= Time.deltaTime;
        }

        Vector3 leftControllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LHand);
        Vector3 rightControllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand);

        // checks if the left controller is in lever bounds
        if (leverCollider.bounds.Contains(leftControllerPosition))
        {
            leftInCollider = true;
        }
        else
        {
            leftInCollider = false;
        }

        // checks if the right controller is in lever bounds
        if (leverCollider.bounds.Contains(rightControllerPosition))
        {
            rightInCollider = true;
        }
        else
        {
            rightInCollider = false;
        }

        // activates if left trigger is pressed while in the lever collider
        leftHandDevice.TryGetFeatureValue(CommonUsages.trigger, out float leftTriggerValue);
        if (leftTriggerValue > 0.1f && leftInCollider && disableTimer <= 0.0f)
        {
            leverFlipped.Invoke();
            disableTimer = timeBuffer;
        }

        // activates if right trigger is pressed while in the lever collider
        rightHandDevice.TryGetFeatureValue(CommonUsages.trigger, out float rightTriggerValue);
        if (rightTriggerValue > 0.1f && rightInCollider && disableTimer <= 0.0f)
        {
            leverFlipped.Invoke();
            disableTimer = timeBuffer;
        }
    }
}
