using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController _controller;
    
    [SerializeField]
    private float movementSpeed = 5;
    
    [SerializeField]
    private float rotationSpeed = 10;
    private Animator anim;
    public Camera followCamera { get; set; }

    //public bool attack;

    //public GameObject enemy;
    
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        //attack = enemy;

        //if (attack)
        //{
        //    if (Input.GetKeyDown(KeyCode.F))
        //    {

        //        enemy.GetComponent<HPScript>().hp -= 5;
        //    }
        //}
        Movement();

    }

    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        anim.SetFloat("velx", horizontalInput);
        anim.SetFloat("vely", verticalInput);
        Vector3 movementInput = Quaternion.Euler(0, followCamera.transform.eulerAngles.y, 0) * new Vector3(horizontalInput, 0, verticalInput);
        Vector3 movementDirection = movementInput.normalized;

        if (movementDirection != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
        }

        _controller.SimpleMove(movementDirection * movementSpeed * Time.deltaTime);
    }
}
