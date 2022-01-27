using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.XR;

// Reference: https://docs.unity3d.com/Manual/xr_input.html#AccessingInputDevices
[System.Serializable]
public class PrimaryButtonEvent : UnityEvent<bool> { }

[System.Serializable]
public class SecondaryButtonEvent : UnityEvent<bool> { }

[System.Serializable]
public class TriggerButtonEvent : UnityEvent<bool> { }

[System.Serializable]
public class GripButtonEvent : UnityEvent<bool> { }

public class InputManager : MonoBehaviour
{
  public PrimaryButtonEvent primaryButtonPress;
  public SecondaryButtonEvent secondaryButtonPress;
  public TriggerButtonEvent triggerButtonPress;
  public GripButtonEvent gripButtonPress;

  private List<InputDevice> devicesWithPrimaryButton;
  private bool lastPrimaryButtonState = false;
  private List<InputDevice> devicesWithSecondaryButton;
  private bool lastSecondaryButtonState = false;
  private List<InputDevice> devicesWithTriggerButton;
  private bool lastTriggerButtonState = false;
  private List<InputDevice> devicesWithGripButton;
  private bool lastGripButtonState = false;


  private void Awake()
  {
    if (primaryButtonPress == null)
      primaryButtonPress = new PrimaryButtonEvent();
    if (secondaryButtonPress == null)
      secondaryButtonPress = new SecondaryButtonEvent();
    if (triggerButtonPress == null)
      triggerButtonPress = new TriggerButtonEvent();
    if (gripButtonPress == null)
      gripButtonPress = new GripButtonEvent();

    devicesWithPrimaryButton = new List<InputDevice>();
    devicesWithSecondaryButton = new List<InputDevice>();
    devicesWithTriggerButton = new List<InputDevice>();
    devicesWithGripButton = new List<InputDevice>();
  }

  void OnEnable()
  {
    List<InputDevice> allDevices = new List<InputDevice>();
    InputDevices.GetDevices(allDevices);

    foreach (InputDevice device in allDevices)
      InputDevices_deviceConnected(device);

    InputDevices.deviceConnected += InputDevices_deviceConnected;
    InputDevices.deviceDisconnected += InputDevices_deviceDisconnected;
  }

  private void OnDisable()
  {
    InputDevices.deviceConnected -= InputDevices_deviceConnected;
    InputDevices.deviceDisconnected -= InputDevices_deviceDisconnected;

    devicesWithPrimaryButton.Clear();
    devicesWithSecondaryButton.Clear();

    devicesWithTriggerButton.Clear();
    devicesWithGripButton.Clear();
  }

  private void InputDevices_deviceConnected(InputDevice device)
  {
    bool discardedValue;

    if (device.TryGetFeatureValue(CommonUsages.primaryButton, out discardedValue))
      devicesWithPrimaryButton.Add(device);
    if (device.TryGetFeatureValue(CommonUsages.secondaryButton, out discardedValue))
      devicesWithSecondaryButton.Add(device);
    if (device.TryGetFeatureValue(CommonUsages.triggerButton, out discardedValue))
      devicesWithTriggerButton.Add(device);
    if (device.TryGetFeatureValue(CommonUsages.gripButton, out discardedValue))
      devicesWithGripButton.Add(device);
  }

  private void InputDevices_deviceDisconnected(InputDevice device)
  {
    if (devicesWithPrimaryButton.Contains(device))
      devicesWithPrimaryButton.Remove(device);
    if (devicesWithSecondaryButton.Contains(device))
      devicesWithSecondaryButton.Remove(device);
    if (devicesWithTriggerButton.Contains(device))
      devicesWithTriggerButton.Remove(device);
    if (devicesWithGripButton.Contains(device))
      devicesWithGripButton.Remove(device);
  }

  void Update()
  {
    OnPrimaryButtonClick();
    OnSecondaryButtonClick();
    OnTriggerButtonClick();
    OnGripButtonClick();
  }

  private void OnPrimaryButtonClick()
  {
    bool tempState = false;
    foreach (var device in devicesWithPrimaryButton)
    {
      bool primaryButtonState = false;
      tempState = device.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonState) // did get a value
                  && primaryButtonState // the value we got
                  || tempState; // cumulative result from other controllers
    }

    if (tempState != lastPrimaryButtonState) // Button state changed since last frame
    {
      lastPrimaryButtonState = tempState;
      if (tempState) primaryButtonPress.Invoke(tempState);
    }
  }

  private void OnSecondaryButtonClick()
  {
    bool tempState = false;
    foreach (var device in devicesWithSecondaryButton)
    {
      bool secondaryButtonState = false;
      tempState = device.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButtonState) // did get a value
                  && secondaryButtonState // the value we got
                  || tempState; // cumulative result from other controllers
    }

    if (tempState != lastSecondaryButtonState) // Button state changed since last frame
    {
      lastSecondaryButtonState = tempState;
      if (tempState) secondaryButtonPress.Invoke(tempState);
    }
  }

  private void OnTriggerButtonClick()
  {
    bool tempState = false;
    foreach (var device in devicesWithTriggerButton)
    {
      bool triggerButtonState = false;
      tempState = device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonState) // did get a value
                  && triggerButtonState // the value we got
                  || tempState; // cumulative result from other controllers
    }

    if (tempState != lastTriggerButtonState) // Button state changed since last frame
    {
      lastTriggerButtonState = tempState;
      if (tempState) triggerButtonPress.Invoke(tempState);
    }
  }

  private void OnGripButtonClick()
  {
    bool tempState = false;
    foreach (var device in devicesWithGripButton)
    {
      bool gripButtonState = false;
      tempState = device.TryGetFeatureValue(CommonUsages.gripButton, out gripButtonState) // did get a value
                  && gripButtonState // the value we got
                  || tempState; // cumulative result from other controllers
    }

    if (tempState != lastGripButtonState) // Button state changed since last frame
    {
      lastGripButtonState = tempState;
      if (tempState) gripButtonPress.Invoke(tempState);
    }
  }
}
