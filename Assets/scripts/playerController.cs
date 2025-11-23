using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : NetworkBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float airControl = 0.4f;
    public float jumpForce = 6f;

    [Header("Grounding")]
    public float maxSlopeAngle = 50f;
    public float groundRay = 0.55f;
    public LayerMask groundMask = ~0;

    private Rigidbody rb;
    private bool grounded;
    private Vector2 moveInput;
    private bool jumpPressed;

    void Awake() => rb = GetComponent<Rigidbody>();

    // Called from PlayerInput "Move" event
    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (!IsOwner) return;
        moveInput = ctx.ReadValue<Vector2>();
    }

    // Called from PlayerInput "Jump" event
    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (!IsOwner) return;
        if (ctx.performed)
            jumpPressed = true;
    }

    void Update()
    {
        if (!IsOwner) return;

        if (grounded && jumpPressed)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
        jumpPressed = false;
    }

    void FixedUpdate()
    {
        if (!IsOwner) return;

        grounded = CheckGrounded();

        var cam = Camera.main ? Camera.main.transform : null;
        Vector3 forward = cam ? cam.forward : Vector3.forward;
        Vector3 right   = cam ? cam.right   : Vector3.right;
        forward.y = right.y = 0f;
        forward.Normalize(); right.Normalize();

        Vector3 wishDir = (forward * moveInput.y + right * moveInput.x).normalized;
        float control = grounded ? 1f : airControl;
        Vector3 targetVel = wishDir * moveSpeed * control;

        Vector3 vel = rb.linearVelocity;
        rb.linearVelocity = new Vector3(targetVel.x, vel.y, targetVel.z);
    }

    bool CheckGrounded()
    {
        if (Physics.SphereCast(transform.position, 0.49f, Vector3.down,
                               out var hit, groundRay, groundMask))
        {
            float angle = Vector3.Angle(hit.normal, Vector3.up);
            return angle <= maxSlopeAngle;
        }
        return false;
    }
}
