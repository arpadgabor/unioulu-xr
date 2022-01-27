using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityHandler : MonoBehaviour
{
  private Rigidbody rigidBody;
  void Start()
  {
    rigidBody = GetComponent<Rigidbody>();
  }
  void OnCollisionEnter(Collision collision)
  {
    rigidBody.useGravity = true;
  }
}