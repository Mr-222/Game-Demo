using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    public float dashPower = 5f;
    public float dashCooldown = 2f;
    public float dashDuration = .5f;
    public float jumpCooldown = 1f;
    public float jumpPower = 5f;
    private bool canDash;
    private bool isDashing;
    private bool isJumping;
    private bool isGrounded;
    private bool isMoving;
    [SerializeField]
    private float movementSpeed = 0.1f;
    
    void Start()
    {
        canDash = true;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (canDash && Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dash());
        }
        //if(isGrounded &&)
    }

    private void FixedUpdate()
    {
        if (isDashing) return;
        Movement();
    }
    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        // Translation
        Vector3 movementInput = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 movementDirection = movementInput.normalized;
        Vector3 movement = movementDirection * movementSpeed;
        rb.MovePosition(this.transform.position + movement);
        
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

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        Vector3 movementDirection = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical")).normalized;
        rb.velocity = movementDirection * dashPower;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        rb.velocity = new Vector3(0,0,0);
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
