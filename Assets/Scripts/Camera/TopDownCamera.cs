using UnityEngine;

[RequireComponent(typeof(Camera))]
public class TopDownCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(-3, 10, -5.5f);
    //[SerializeField] private float threshold = .5f;
    
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
        transform.rotation = Quaternion.Euler(50, 25, 0);
    }
    
    private void Update()
    {
        Vector3 currPos = target.position + offset;
        transform.position = currPos;
        
        // transform.position = currPos;
        //
        // Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        // RaycastHit hit;
        // if (Physics.Raycast(ray, out hit))
        // {
        //     Vector3 result = currPos + new Vector3(hit.point.x, 0, hit.point.z);
        //     result.x = Mathf.Clamp(result.x, target.position.x - threshold + offset.x, target.position.x + threshold + offset.x);
        //     result.z = Mathf.Clamp(result.z, target.position.z - threshold + offset.z, target.position.z + threshold + offset.z);
        //
        //     // Smoothly move the camera to the new position
        //     transform.position = result;
        // }
    }
}
