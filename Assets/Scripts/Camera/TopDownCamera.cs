using UnityEngine;

[RequireComponent(typeof(Camera))]
public class TopDownCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 10, -4f);
    [SerializeField] private Vector3 viewingAngle = new Vector3(60, 0, 0);
    [SerializeField] private float threshold = 3f;
    [SerializeField] private float smoothSpeed = 0.03f;
    
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
        transform.position = target.position + offset;
    }
    
    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(viewingAngle);
        
        Vector3 currPos = target.position + offset;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 dir = hit.point - transform.position;
            dir.y = 0;
            dir.Normalize();
            Vector3 result = currPos + dir * 5f;
            result.x = Mathf.Clamp(result.x, currPos.x - threshold, currPos.x + threshold);
            result.z = Mathf.Clamp(result.z, currPos.z - threshold, currPos.z + threshold);
        
            // Smoothly move the camera to the new position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, result, smoothSpeed);
            transform.position = smoothedPosition;
        }
        else
        {
            // Smoothly move the camera to the default position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, currPos, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
