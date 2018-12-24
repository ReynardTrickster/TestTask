using UnityEngine;

public class CameraController : MonoBehaviour {

    public Vector3 TargetPoint { get; private set; }
    const float rotationSpeed = 3;
    const float scaleSpeed = 3;
    const float minScale = 8;
    const float maxScale = 15;

    void Start () {
        TargetPoint = new Vector3(0, 0, 0);
        transform.position = new Vector3(0, 10, 0);
        transform.LookAt(TargetPoint);
    }

    public void Rotate(float axisX, float axisY)
    {
        transform.RotateAround(TargetPoint, new Vector3(axisY, axisX, axisY), rotationSpeed);
        transform.LookAt(TargetPoint);
    }

    public void Scroll(float value)
    {
        Vector3 newPosition = Vector3.MoveTowards(transform.position, TargetPoint, value * scaleSpeed);
        if (newPosition.magnitude >= minScale && newPosition.magnitude <= 15)
            transform.position = newPosition;
    }
}
