using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Public Variables
    public Transform target;
    public Transform pivot;
    public Vector3 offset;
    public float rotateSpeed;
    public float maxViewAngle;
    public float minViewAngle;
    public bool useOffsetValues;

    // Start is called before the first frame update
    void Start()
    {
        if (!useOffsetValues)
        {
            offset = target.position - transform.position;
        }

        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Rotate Camera
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.Rotate(0, horizontal, 0);
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        pivot.Rotate(-vertical, 0, 0);

        // Limit up/down camera rotation
        if(pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
        }

        if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minViewAngle)
        {
            pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0, 0);
        }

        // Move Camera
        float desiredYAngle = target.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation * offset);

        // Camera Aim
        if(transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y - 0.5f, transform.position.z);
        }
        transform.LookAt(target);
    }
}
