using UnityEngine;

public class PerspectiveCamera : MonoBehaviour
{
    [SerializeField] private Transform display;
    private Transform _camera;

    private void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
}
