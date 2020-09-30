using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public virtual void Start() { }
    public virtual void Update() { }
    public virtual void Rotate(bool isClockwise) { }
    public virtual void MoveTo(Transform targetTransform) { }
    public virtual void Move(Vector3 direction) { }
    public virtual void RotateTowards(Transform targetTransform) { }
}
