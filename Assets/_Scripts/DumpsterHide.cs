using UnityEngine;

public class DumpsterHide : MonoBehaviour
{
    public GameObject player;              
    public Light spotlight;               
    public KeyCode hideKey = KeyCode.E;
    public float hideDistance = 2f;

    private bool isHidden = false;
    private Renderer[] playerRenderers;
    private Collider[] playerColliders;
    private MonoBehaviour playerMovement; // Replace with actual movement script name
    private Rigidbody playerRigidbody;

    void Start()
    {
        playerRenderers = player.GetComponentsInChildren<Renderer>();
        playerColliders = player.GetComponentsInChildren<Collider>();
        playerMovement = player.GetComponent<Player>(); // Replace if your script is named differently
        playerRigidbody = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        bool isInRange = distance <= hideDistance;

        if (isInRange && Input.GetKeyDown(hideKey))
        {
            if (!isHidden)
                HidePlayer();
            else
                UnhidePlayer();
        }

        // 👇 Auto-unhide if player walks away after hiding
        if (isHidden && !isInRange)
        {
            UnhidePlayer();
        }
    }

    void HidePlayer()
    {
        // Disable rendering and collision
        foreach (Renderer rend in playerRenderers)
            rend.enabled = false;

        foreach (Collider col in playerColliders)
            col.enabled = false;

        if (spotlight != null)
            spotlight.enabled = false;

        // Stop and disable movement
        if (playerRigidbody != null)
            playerRigidbody.linearVelocity = Vector3.zero;

        if (playerMovement != null)
            playerMovement.enabled = false;

        isHidden = true;
    }

    void UnhidePlayer()
    {
        foreach (Renderer rend in playerRenderers)
            rend.enabled = true;

        foreach (Collider col in playerColliders)
            col.enabled = true;

        if (spotlight != null)
            spotlight.enabled = true;

        if (playerMovement != null)
            playerMovement.enabled = true;

        isHidden = false;
    }
}
