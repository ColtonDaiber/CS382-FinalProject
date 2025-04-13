using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class enemyMovement : MonoBehaviour
{
    //note this is the forward direction of the enemy, such that the length of this vector is 1
    readonly Vector3 ENEMY_FORWARD_DIR = new Vector3(0,0,1);

    GameObject targetPos;
    [SerializeField] float speed = 1;
    [SerializeField] float rotationSpeed = 360;
    [SerializeField] bool hidePathPoints = true;
    [SerializeField] List<GameObject> pathPoints;
    [SerializeField] float viewDist = 1f;
    [SerializeField] float fov = 45;
    GameObject player = null;

    const float AT_POINT_THRESH = 0.5f;

    bool chasingPlayer = false;
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

        GetPlayerGO();
    }

    NavMeshPath path;
    int pathNextIndex = 0;

    void Update()
    {
        LookForPlayer();

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

    void LookForPlayer()
    {
        if(player == null)
        {
            GetPlayerGO();
            return; //return bc we are not garenteed that player game object will be found, and we will check again next frame
        }

        Vector3 enemyToPlayer = player.transform.position - this.transform.position;
        float distanceToPlayer = enemyToPlayer.magnitude;
        Vector3 directionToPlayer = enemyToPlayer / distanceToPlayer;

        Vector3 enemyForwardDirectionGlobal = this.transform.TransformPoint(ENEMY_FORWARD_DIR);
        float angleToPlayer = Mathf.Acos(Vector3.Dot(enemyForwardDirectionGlobal, directionToPlayer)); // A . B = Cos(theta) -> theta = Acos(A . B) //both A and B are already normalized

        if(angleToPlayer <= 0.5f*fov && distanceToPlayer < viewDist)
        { //found player
            chasingPlayer = true;
        }

        if(chasingPlayer) Debug.Log(chasingPlayer);
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
            // Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red, 5.0f);
        }
    }

    void Move(Vector3 velocity)
    {
        velocity.y = 0;
        velocity.x *= speed;
        velocity.z *= speed;

        this.GetComponent<Rigidbody>().linearVelocity = velocity;
    }

    void Look(Vector3 direction)
    {
        if(!chasingPlayer && direction != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.RotateTowards( transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }


    void GetPlayerGO()
    {
        Player playerScript = GameObject.FindObjectsByType<Player>(FindObjectsSortMode.None)[0];
        if(playerScript != null) player = playerScript.gameObject;
    }
}
