using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class enemyMovement : MonoBehaviour
{
    GameObject targetPos;
    [SerializeField] float speed = 1000;
    [SerializeField] float rotationSpeed = 360;
    [SerializeField] bool hidePathPoints = true;
    [SerializeField] List<GameObject> pathPoints;

    const float AT_POINT_THRESH = 0.5f;

    bool lookingForPlayer = false;
    int index = 0;

    void Start()
    {
        targetPos = pathPoints[0];

        if(hidePathPoints)
        {
            foreach(GameObject point in pathPoints)
            {
                Destroy(point.GetComponent<MeshRenderer>());
            }
        }

        path = new NavMeshPath();
    }

    NavMeshPath path;
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
            Look(Direction);
        }
    }

    int cnt = 0;
    void CalcNewPath()
    {
        NavMeshPath newPath = new NavMeshPath();

        cnt++;
        if(cnt >= 20)
        {
            if( (new Vector2(transform.position.x, transform.position.z) - new Vector2(targetPos.transform.position.x, targetPos.transform.position.z)).sqrMagnitude < 0.3 )
            {
                index = index + 1;
                if(index >= pathPoints.Count) index = 0;
                targetPos = pathPoints[index];
            }

            if(NavMesh.CalculatePath(this.transform.position, targetPos.transform.position, NavMesh.AllAreas, newPath))
            {
                cnt = 0;
                path = newPath;
                pathNextIndex = 0;

                DrawPath(newPath);
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
    }

    void Look(Vector3 direction)
    {
        if(!lookingForPlayer && direction != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.RotateTowards( transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

}
