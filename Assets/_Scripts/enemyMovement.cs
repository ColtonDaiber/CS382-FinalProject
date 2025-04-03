using UnityEngine;
using UnityEngine.AI;

public class enemyMovement : MonoBehaviour
{
    [SerializeField] GameObject targetPos;
    [SerializeField] float speed = 2000;

    const float AT_POINT_THRESH = 0.5f;

    void Start()
    {

    }

    NavMeshPath path = new NavMeshPath();
    int pathNextIndex = 0;

    void Update()
    {
        CalcNewPath();

        for(int i = pathNextIndex; path != null && i < path.corners.Length; i++)
        {
            if( (new Vector2(this.transform.position.x, this.transform.position.z) - new Vector2(path.corners[i].x, path.corners[i].z)).magnitude < AT_POINT_THRESH )
            {
                pathNextIndex ++;
            }
        }

        if(path != null && pathNextIndex < path.corners.Length)
        {
            Vector3 Direction = (path.corners[pathNextIndex] - this.transform.position).normalized;
            Move(Direction);
        }
    }

    int cnt = 0;
    void CalcNewPath()
    {
        NavMeshPath newPath = new NavMeshPath();

        cnt++;
        if(cnt >= 20)
        {
            if(NavMesh.CalculatePath(this.transform.position, targetPos.transform.position, NavMesh.AllAreas, newPath))
            {
                cnt = 0;
                path = newPath;
                pathNextIndex = 0;

                // DrawPath(newPath);
            }
        }
    }

    void DrawPath(NavMeshPath path)
    {
        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red, 5.0f);
        }
    }

    void Move(Vector3 velocity)
    {
        velocity.y = 0;
        velocity.x *= speed * Time.deltaTime;
        velocity.z *= speed * Time.deltaTime;

        this.GetComponent<Rigidbody>().linearVelocity = velocity;
        Debug.Log(velocity);
    }
}
