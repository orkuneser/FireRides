using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    #region VARIABLES
    // Object to be tracked
    [SerializeField] private Transform Target;

    // Tracking speed
    [SerializeField] private float smoothSpeed = 0.125f;

    // Target following distance
    [SerializeField] private Vector3 offSet;

    #endregion

    private void LateUpdate()
    {
        CameraFollowMechanic();
    }

    private void CameraFollowMechanic()
    {
        // Calculate Desired Position
        Vector3 desiredPosition = Target.position + offSet;

        // Make it smoother to follow
        Vector3 smoothedPoisiton = Vector3.Lerp(transform.position,desiredPosition,smoothSpeed);

        // Sync camera position to calculated position
        transform.position = smoothedPoisiton;
    }
}
