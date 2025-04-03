using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 30;

    // Update is called once per frame
    void Update()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 velocity;
        velocity.y = 0;
        velocity.x = hAxis * speed * Time.deltaTime;
        velocity.z = vAxis * speed * Time.deltaTime;

        this.GetComponent<Rigidbody>().linearVelocity = velocity;
    }
}
