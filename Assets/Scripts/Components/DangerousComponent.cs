using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public struct DangerousComponent
{
    public Transform obstacleTransform;
    public Vector3 pointA;
    public Vector3 pointB;
}
