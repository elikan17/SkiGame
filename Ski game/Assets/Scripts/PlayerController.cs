using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    InputAction move;
    [SerializeField] private float rotationSpeed = 30, moveSpeed = 30;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector3 pushbackForce;
    [SerializeField] private bool disabled = false;
    [SerializeField] private float disableTime = 0.7f;
    private float lastDisableTime;
    Rigidbody rb;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        move = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        Obstacle.OnPlayerHit += TakeDamage;
    }

    void TakeDamage()
    {
        disabled = true;
        lastDisableTime = Time.timeSinceLevelLoad;
        rb.AddForce(pushbackForce);
        Debug.Log("OUCH");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics.Linecast(transform.position, 
            transform.position - transform.up, groundLayer);
        Debug.DrawLine(transform.position,
            transform.position - transform.up, Color.red );
        
        if(Time.timeSinceLevelLoad > lastDisableTime + disableTime)
            disabled = false;
        
        if (isGrounded && !disabled)
        {
            Vector2 moveInput = move.ReadValue<Vector2>();
            float slopeAngle = Mathf.Abs(transform.localEulerAngles.y - 180);
            float speedMultiplier = Mathf.Cos(Mathf.Deg2Rad * slopeAngle);
            rb.AddForce(transform.forward * moveSpeed * speedMultiplier * Time.fixedDeltaTime);
            transform.Rotate(0, moveInput.x * rotationSpeed * Time.fixedDeltaTime, 0);
            //Debug.Log("move x : " + moveInput.x +  " move y : " + moveInput.y);
        }

    }
}
