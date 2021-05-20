using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTV : MonoBehaviour
{
    public float viewDistance;
    private float viewAngle;
    public Light spotLight;
    Color OGLightColor;
    Transform player;
    [SerializeField] LayerMask whatIsObastacle = default;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        viewAngle = spotLight.spotAngle;
        OGLightColor = spotLight.color;
    }

    void Update()
    {
        if (CanSeePlayer())
        {
            spotLight.color = Color.red;
        }
        else
        {
            spotLight.color = OGLightColor;
        }

    }

    bool CanSeePlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < viewDistance)
        {
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float angleBtnEnemyAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if (angleBtnEnemyAndPlayer < viewAngle / 2f)
            {

                if (!Physics.Linecast(transform.position, player.position, whatIsObastacle))
                {
                    return true;
                    //we can see the player
                }
            }
        }
        return false;
        // player not in sight;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }
}
