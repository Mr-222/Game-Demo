using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController _controller;
    
    [SerializeField]
    private float movementSpeed = 5;
    
    [SerializeField]
    private float rotationSpeed = 10;
    
    public Camera followCamera { get; set; }

    public bool attack;

    public GameObject enemy;
    
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        attack = enemy;

        if (attack)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                
                enemy.GetComponent<HPScript>().hp -= 5;
            }
        }
        Movement();

    }

    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        Vector3 movementInput = Quaternion.Euler(0, followCamera.transform.eulerAngles.y, 0) * new Vector3(horizontalInput, 0, verticalInput);
        Vector3 movementDirection = movementInput.normalized;

        if (movementDirection != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
        }

        _controller.Move(movementDirection * movementSpeed * Time.deltaTime);
    }
}
