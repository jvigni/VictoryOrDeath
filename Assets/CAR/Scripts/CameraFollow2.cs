using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2 : MonoBehaviour
{
    public Transform target;
    [Range(1, 10)] public float followSpeed = 2f;
    [Range(1, 10)] public float rotateSpeed = 5f;

    private Vector3 offset;

    void Start()
    {
        // Calculate the initial offset between the camera and the target
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        FollowTarget();
        RotateTowardsTarget();
    }

    void FollowTarget()
    {
        // Calculate the new camera position based on the target's position and the initial offset
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }

    void RotateTowardsTarget()
    {
        // Calculate the direction to look at the target and smoothly rotate the camera
        Vector3 lookDirection = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }
}
