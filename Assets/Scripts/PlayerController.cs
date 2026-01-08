using System.Reflection.Emit;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // ship movement options
    [Header("Track Follow Settings")]
    public LayerMask layerMask;
    public float groundDetectHeight;
    public float shipHeight;
    public float rotationSpeed;
    public float rotationSmoothingSpeed;
    public float heightSmoothingSpeed;

    [Header("Controller Settings")]
    public float shipSpeed;
    public float shipSpeedDeadzone;
    public float shipAccel;
    public float shipNaturalDecel;
    public float shipBrake;
    public float shipTurnRate;

    // input system
    private InputSystem_Actions playerControls;
    private InputAction throttleControl;
    private InputAction brakeControl;
    private InputAction steeringControl;

    // misc
    private Rigidbody rb;

    void Awake()
    {
        playerControls = new InputSystem_Actions();
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        throttleControl = playerControls.Player.Throttle;
        throttleControl.Enable();

        brakeControl = playerControls.Player.Brake;
        brakeControl.Enable();

        steeringControl = playerControls.Player.Steering;
        steeringControl.Enable();
    }

    void OnDisable()
    {
        throttleControl.Disable();
        brakeControl.Disable();
        steeringControl.Disable();
    }

    void UpdateShipTransform()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.up, out hit, groundDetectHeight, layerMask))
        {
            //Debug.DrawRay(transform.position, -transform.up * shipHeight, Color.green);
            rb.angularVelocity = Vector3.zero;

            Vector3 targetPosition = hit.point + (hit.normal * shipHeight);

            Vector3 cross = Vector3.Cross(transform.right, hit.normal);
            Quaternion targetRotation = Quaternion.LookRotation(cross, hit.normal);

            rb.MovePosition(Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * heightSmoothingSpeed));
            rb.MoveRotation(Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmoothingSpeed));
        } else
        {
            //Debug.DrawRay(transform.position, -transform.up * shipHeight, Color.yellow);
        }
    }

    void movePlayer()
    {
        float throttleVal = throttleControl.ReadValue<float>();
        float brakeVal = brakeControl.ReadValue<float>();
        float steeringInput = steeringControl.ReadValue<float>();

        Vector3 forceVector = Vector3.zero;

        // if thottle but no brake
        if (throttleVal > 0 && brakeVal == 0)
        {
            //Debug.Log("throttle");
            // if the speed of ship is less than max speed
            if (rb.linearVelocity.magnitude < shipSpeed)
            {
                //Debug.Log("forward");
                forceVector = transform.forward * (shipAccel * throttleVal);
            }
        }

        // if no throttle but brake
        if (throttleVal == 0 && brakeVal > 0)
        {
            //Debug.Log("brake");
            if (rb.linearVelocity.magnitude > shipSpeedDeadzone)
            {
                //Debug.Log("adding brakes");
                forceVector = -rb.linearVelocity.normalized * (shipBrake * brakeVal);
            }
        }

        // no throttle nor brake
        if (throttleVal == 0 && brakeVal == 0)
        {
            if (rb.linearVelocity.magnitude > shipSpeedDeadzone)
            {
                //Debug.Log("natty decel");
                forceVector = -rb.linearVelocity.normalized * shipNaturalDecel;
            } else
            {
                // if no input and below speed deadzone, set velocity to zero; stops slow drifts
                //Debug.Log("deadzone");
                rb.linearVelocity = Vector3.zero;
            }
        }

        // steering
        if (steeringInput != 0)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, shipTurnRate * steeringInput, 0f));
        }

        // add force
        rb.AddForce(forceVector);

        // debug
        Debug.DrawRay(transform.position, forceVector, Color.red);
    }

    void FixedUpdate()
    {
        movePlayer();
        UpdateShipTransform();
    }
}
