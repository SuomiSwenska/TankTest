using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed;
    public float eulerSmoothSpeed;

    private void LateUpdate()
    {
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, target.position, smoothSpeed);
        transform.position = smoothedPosition;

        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime * eulerSmoothSpeed);
    }
}