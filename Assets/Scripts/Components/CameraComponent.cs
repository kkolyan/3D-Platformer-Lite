using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct CameraComponent
{
    public Transform cameraTransform;
    public Vector3 curVelocity;
    public Vector3 offset;
    public float cameraSmoothness;
}
