using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportHandler : MonoBehaviour
{
  public GameObject teleportTarget;
  private Vector3 originalPosition;

  void Start()
  {
    originalPosition = transform.position;
  }

  // Update is called once per frame
  void Update()
  {
    teleportOutside(Input.GetMouseButtonDown(0));
  }

  public void teleportOutside(bool state = false)
  {
    if (!state) return;

    if (transform.position.Equals(teleportTarget.transform.position))
    {
      transform.position = originalPosition;
    }
    else
    {
      transform.position = teleportTarget.transform.position;
    }
  }
}
