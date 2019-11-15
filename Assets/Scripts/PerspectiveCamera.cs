using UnityEngine;

public class PerspectiveCamera : MonoBehaviour
{
    [SerializeField] private Transform display;
    private Transform _camera;
    private Vector3 _initialForward;

    private void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").transform;

        _initialForward = transform.forward;
    }

    private void Update()
    {
        Vector3 axisOfRotation = Vector3.Cross((display.position - _camera.position), -display.forward).normalized;
        
        float angleBetween = Vector3.Angle(_camera.position - display.position, -display.forward);
        
        transform.forward = Quaternion.AngleAxis(angleBetween, axisOfRotation) * _initialForward;
    }
}