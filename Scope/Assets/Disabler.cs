using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Disabler : MonoBehaviour
{
  private Camera camera;
  private Quaternion lockedRotation;
  private Vector3 lockedPosition;
  private bool start;

  private List<InputDevice> devices = new List<InputDevice>();
  private InputDevice headset;

  private InputDevice buttonA;
  private bool wasButtonAPressed = false;
  private bool isPositionLocked = false;

  private InputDevice buttonB;
  private bool wasButtonBPressed = false;
  private bool isRotationLocked = false;

  void Start()
  {
    camera = GetComponentInChildren<Camera>();
    InputDevices.GetDevices(devices);
    foreach (var device in devices)
    {
      if (device.TryGetFeatureValue(CommonUsages.centerEyeRotation, out Quaternion rotation))
      {
        headset = device;
      }
      if (device.TryGetFeatureValue(CommonUsages.primaryButton, out bool result))
      {
        buttonA = device;
      }
      if (device.TryGetFeatureValue(CommonUsages.secondaryButton, out result))
      {
        buttonB = device;
      }
    }
  }

  private void Update()
  {
    CheckButtonPress();

    if (isPositionLocked)
    {
      headset.TryGetFeatureValue(CommonUsages.centerEyePosition, out Vector3 headsetPosition);
      this.transform.position = new Vector3(
        -headsetPosition.x + lockedPosition.x,
        -headsetPosition.y + lockedPosition.y,
        -headsetPosition.z + lockedPosition.z
      );
    }
    if (isRotationLocked)
    {
      headset.TryGetFeatureValue(CommonUsages.centerEyeRotation, out Quaternion headsetRotation);
      this.transform.rotation = lockedRotation * (Quaternion.Inverse(headsetRotation) * Quaternion.identity);
    }
  }

  void CheckButtonPress()
  {
    bool hasA = buttonA.TryGetFeatureValue(CommonUsages.primaryButton, out bool isAPressed);
    bool hasB = buttonB.TryGetFeatureValue(CommonUsages.secondaryButton, out bool isBPressed);

    if (hasA && isAPressed && !wasButtonAPressed)
    {
      if (!isPositionLocked) StartLockPosition();
      isPositionLocked = !isPositionLocked;
    }
    if (hasB && isBPressed && !wasButtonBPressed)
    {
      if (!isRotationLocked) StartLockRotation();
      isRotationLocked = !isRotationLocked;
    }

    wasButtonAPressed = isAPressed;
    wasButtonBPressed = isBPressed;
  }

  void StartLockRotation()
  {
    headset.TryGetFeatureValue(CommonUsages.centerEyeRotation, out Quaternion headsetRotation);
    lockedRotation = headsetRotation;
  }

  void StartLockPosition()
  {
    headset.TryGetFeatureValue(CommonUsages.centerEyePosition, out Vector3 headsetPosition);
    lockedPosition = headsetPosition;

  }

}
