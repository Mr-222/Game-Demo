using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [SerializeField] 
    private float mouseSensitivity = 3f;

    private float _rotationY;
    private float _rotationX;

    [SerializeField] 
    private Transform target;
    
    [SerializeField]
    private float yDistanceFromTarget = .5f;
    
    [SerializeField]
    private float zDistanceFromTarget = 3f;
    
    private Vector3 _currentRotation;
    private Vector3 _smoothVelocity = Vector3.zero;

    [SerializeField] 
    private float smoothTime = 0.2f;

    private void Start()
    {
        target.gameObject.GetComponent<PlayerController>().followCamera = GetComponent<Camera>();
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        _rotationY += mouseX;
        _rotationX -= mouseY;

        _rotationX = Mathf.Clamp(_rotationX, -10, 20);

        Vector3 nextRotation = new Vector3(_rotationX, _rotationY);
        _currentRotation = Vector3.SmoothDamp(_currentRotation, nextRotation, ref _smoothVelocity, smoothTime);
        transform.localEulerAngles = _currentRotation;

        transform.position = target.position - transform.forward * zDistanceFromTarget + Vector3.up * yDistanceFromTarget;
    }
}
