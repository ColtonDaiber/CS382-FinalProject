using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;

    private Rigidbody rb;
    private Vector3 movement;
    private float lightRotationY = 0f; // Tracks full 360° rotation

    public Light spotlight;
    public float lightDistance = 5f;
    public float lightHeight = 2f; // Height above player

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Movement input (WASD)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        movement = transform.forward * moveZ + transform.right * moveX;

        // Mouse input for light rotation (full 360°)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        lightRotationY += mouseX;

        // Wrap rotation to stay within 0-360° (optional, for clarity)
        if (lightRotationY > 360f) lightRotationY -= 360f;
        if (lightRotationY < 0f) lightRotationY += 360f;

        // Calculate light position in a circle around the player
        Vector3 lightOffset = new Vector3(
            Mathf.Sin(lightRotationY * Mathf.Deg2Rad) * lightDistance,
            lightHeight,
            Mathf.Cos(lightRotationY * Mathf.Deg2Rad) * lightDistance
        );

        spotlight.transform.position = transform.position + lightOffset;

        // Make the light always point at the player (or slightly ahead)
        spotlight.transform.LookAt(transform.position);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(movement.x * moveSpeed, rb.linearVelocity.y, movement.z * moveSpeed);
    }
}