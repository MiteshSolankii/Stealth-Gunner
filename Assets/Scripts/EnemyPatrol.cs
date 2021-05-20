using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform pathHolder;

    public float speed = 4f,turnSpeed = 90f;
    public float waitTime = 0.3f;

    public float viewDistance;
    private float viewAngle;
    public Light spotLight;
    Color OGLightColor;
    public Color seenColor;

    Transform player;
    [SerializeField] LayerMask whatIsObastacle = default;
    Vector3[] waypoints;

    EnemyGunController enemyGun;

    public Animator enemyAnim;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyGun = GetComponent<EnemyGunController>();
        viewAngle = spotLight.spotAngle;
        OGLightColor = spotLight.color;
        waypoints = new Vector3[pathHolder.childCount];
        
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
        }

        StartCoroutine(FollowPath(waypoints));
    }

    // Update is called once per frame
    void Update()
    {
            if (CanSeePlayer())
            {
                if (GameManager.instance.isGameOver == false)
                {
                    spotLight.color = seenColor;                   
                    enemyAnim.SetBool("CanSeePlayer", true);
                    enemyGun.ShootPlayer();
                   
                }
            }
            else
            {
                enemyAnim.SetBool("CanSeePlayer", false);
                spotLight.color = OGLightColor;
                
            }
        
    }

    bool CanSeePlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < viewDistance)
        {
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float angleBtnEnemyAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if(angleBtnEnemyAndPlayer < viewAngle / 2f)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);

                if (!Physics.Raycast(transform.position, dirToPlayer, distanceToPlayer, whatIsObastacle))
                {
                    return true;
                }
                //RaycastHit hitInfo = Physics.ray
                // if (!Physics.Linecast(transform.position, player.position, whatIsObastacle))
                // {return true;} //we can see the player

            }
        }
        return false;
        // player not in sight;
    }

    IEnumerator FollowPath(Vector3[] waypoints)
    {
        transform.position = waypoints[0];
        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = waypoints[targetWaypointIndex];
        transform.LookAt(targetWaypoint);

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
            if(transform.position == targetWaypoint)
            {
                targetWaypointIndex += 1;
                if(targetWaypointIndex == waypoints.Length)
                {
                    targetWaypointIndex = 0;
                }
                targetWaypoint = waypoints[targetWaypointIndex];
                yield return new WaitForSeconds(waitTime);
                yield return StartCoroutine(TurnToFace(targetWaypoint));
            }
            yield return null;

        }
    }
    IEnumerator TurnToFace(Vector3 lookToTarget)
    {
        Vector3 dirToLookTarget = (lookToTarget - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;
        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05f)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;
        foreach (Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, 0.2f);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }
        Gizmos.DrawLine(previousPosition, startPosition);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }
}
