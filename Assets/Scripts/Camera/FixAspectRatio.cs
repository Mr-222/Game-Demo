using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FixAspectRatio : MonoBehaviour
{
    [Tooltip("You need to restart to apply the changes")]
    [SerializeField] private float aspectRatio = 16f / 9f;
    
    void Start()
    {
        GetComponent<Camera>().aspect = aspectRatio;
    }
}