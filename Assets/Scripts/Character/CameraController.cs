using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : InputMonoBehaviour
{
    [SerializeField] private float RotationPower = 10f;
    [SerializeField] private float HorizontalDamping = 1f;
    [SerializeField] private GameObject FollowTarget;

    private Vector2 PreviousMouseDelta = Vector2.zero;

    private void OnLook(InputValue delta)
    {
        //Debug.Log("Looking");
        Vector2 aimvalue = delta.Get<Vector2>();
        Quaternion addedRotation = Quaternion.AngleAxis(Mathf.Lerp(PreviousMouseDelta.x, aimvalue.x, 1f / HorizontalDamping) * RotationPower,
            transform.up);

        FollowTarget.transform.rotation *= addedRotation;
        PreviousMouseDelta = aimvalue;
        transform.rotation = Quaternion.Euler(0, FollowTarget.transform.rotation.eulerAngles.y, 0);
        FollowTarget.transform.localEulerAngles = Vector3.zero;
    }

    //private void OnEnable()
    //{
        
    //}

    //private void OnDisable()
    //{
        
    //}
}
