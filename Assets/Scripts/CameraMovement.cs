using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothingSpeed;
    public Vector2 maxCameraBounds;
    public Vector2 minCameraBounds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        if (transform.position != targetPosition)
        {
            targetPosition.x = Mathf.Clamp(targetPosition.x, minCameraBounds.x, maxCameraBounds.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minCameraBounds.y, maxCameraBounds.y);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothingSpeed);
        }
    }
}
