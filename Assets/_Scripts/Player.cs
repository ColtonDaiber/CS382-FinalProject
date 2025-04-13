using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1;
    public float score = 0f;
    public bool canLeave = false;

    // Update is called once per frame
    void Update()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 velocity;
        velocity.y = 0;
        velocity.x = hAxis * speed;
        velocity.z = vAxis * speed;

        this.GetComponent<Rigidbody>().linearVelocity = velocity;
    }
}
