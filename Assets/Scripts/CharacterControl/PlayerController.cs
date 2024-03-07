using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    public float dashPower = 5f;
    public float dashCooldown = 2f;
    public float dashDuration = .5f;
    public float jumpCooldown = 1f;
    public float jumpPower = 5f;
    public float lastJumpPressed;
    private bool canDash;
    private bool isDashing;
    private bool isJumping;
    private bool isGrounded;
    private bool isMoving;
    private bool isFalling;
    private bool canJump;
    [SerializeField]
    private float movementSpeed = 0.1f;
    
    // skill related
    public UnityEvent<int, int> OnCardChanged;
    public UnityEvent OnSkillUsed;
    int totalSkillCount = Enum.GetValues(typeof(CardType)).Length;
    int currSkill = -1;
    
    void Start()
    {
        isGrounded = true;
        isFalling = false;
        canJump = true;
        canDash = true;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Debug.DrawRay(transform.position+Vector3.up*.5f, Vector3.down * .65f, Color.red);
        if (Physics.Raycast(rb.transform.position + Vector3.up * .5f, Vector3.down, out RaycastHit groundHit,.8f))
        {
            if (groundHit.collider.CompareTag("Ground")) {
                isFalling = false;
                isGrounded = true;
                canJump = true;
                anim.SetBool("IsGrounded", isGrounded);
                anim.SetBool("IsJumping", false);
                anim.SetBool("IsFalling", isFalling);
            }
        }
        else {
            canJump = false;
            isGrounded = false;
            isFalling = true;
            anim.SetBool("IsFalling", isFalling);
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsGrounded", isGrounded);
        }
        if (Time.time - lastJumpPressed > jumpCooldown&& isGrounded) {
            canJump = true;
        }
        if (isGrounded && canJump && Input.GetKeyDown(KeyCode.Space)) {
            lastJumpPressed = Time.time;
            Jump();
            isJumping = true;
            anim.SetBool("IsJumping", isJumping);
            canJump = false;
            isGrounded = false;
        }

        if (canDash && Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dash());
        }
        
        HandleSkill();
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
    void Jump() {
        Debug.Log("Jump()");
        rb.AddForce(jumpPower * Vector3.up,ForceMode.Impulse);
        isJumping = false;
        canJump = true;
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

    void HandleSkill()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            OnCardChanged.Invoke(currSkill, 0);
            currSkill = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            OnCardChanged.Invoke(currSkill, 1);
            currSkill = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            OnCardChanged.Invoke(currSkill, 2);
            currSkill = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            OnCardChanged.Invoke(currSkill, 3);
            currSkill = 3;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && currSkill != -1)
        {
            OnSkillUsed.Invoke();
            currSkill = -1;
        }
    }
}
