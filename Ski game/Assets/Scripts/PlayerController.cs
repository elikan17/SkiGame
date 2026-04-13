using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    InputAction move;
    [SerializeField] private float rotationSpeed = 30, moveSpeed = 30;
    Rigidbody rb;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        move = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 moveInput = move.ReadValue<Vector2>();
        float slopeAngle = Mathf.Abs(transform.localEulerAngles.y - 180);
        float speedMultiplier = Mathf.Cos(Mathf.Deg2Rad * slopeAngle);
        rb.AddForce(transform.forward * moveSpeed * speedMultiplier * Time.fixedDeltaTime);
        transform.Rotate(0, moveInput.x * rotationSpeed * Time.fixedDeltaTime, 0);
        //Debug.Log("move x : " + moveInput.x +  " move y : " + moveInput.y);
        
    }
}
