using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    
    [SerializeField]
    private float movementSpeed = 5;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        // Translation
        Vector3 movementInput = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 movementDirection = movementInput.normalized;
        Vector3 movement = movementDirection * movementSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);

        // Rotate toward mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 lookAtPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(lookAtPosition);
        }
        
        // Control animation
        float vely = Vector3.Dot(transform.forward, movementDirection);
        float velx = Vector3.Dot(transform.right, movementDirection);
        anim.SetFloat("velx", velx);
        anim.SetFloat("vely", vely);
    }
}
