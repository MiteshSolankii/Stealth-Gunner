using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f, rotateSpeed = 1f;

    [HideInInspector]
    public Vector3 moveInput;

    private Rigidbody RB;
    private GunController gunController;
    public Joystick moveJoystick;

    public Animator anim;

    public int playerHealth = 1;

    public bool canMove = true;

    public GameObject cineCam;

    [SerializeField] Transform enemyCheck,enemyCheck2;
    [SerializeField] float enemyCheckdistance;

    public float waitTime = 0.0f, waitTimeDefault = 0.2f;

    private LaserSight laserSight;

    // Start is called before the first frame update
    void Start()
    {
        cineCam = GameObject.Find("CineCam");
        canMove = true;
       
        RB = GetComponent<Rigidbody>();
        gunController = FindObjectOfType<GunController>();
        laserSight = FindObjectOfType<LaserSight>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            // MovementInput();
            JoystickMovement();
        }
        if(playerHealth <= 0)
        {
            gunController.enabled = false;
            canMove = false;
            anim.SetBool("IsDead", true);
            cineCam.GetComponent<Animator>().Play("CamZoomToPlayer");
            GameManager.instance.isGameOver = true;
        }
        CheckForEnemy();
        CheckForEnemy2();
    }

    private void FixedUpdate()
    {
        if(canMove)
        PlayerMovement();
    }

    void JoystickMovement()
    {
        float moveX = moveJoystick.Horizontal;
        float moveY = moveJoystick.Vertical;
        moveInput = new Vector3(moveX, RB.velocity.y, moveY);
        if (moveInput != Vector3.zero)
        {
            anim.SetBool("IsRunning", true);
            laserSight.lr.enabled = true;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveInput), rotateSpeed * Time.deltaTime);
        }
        else
        {
            laserSight.lr.enabled = false;
            anim.SetBool("IsRunning", false);
        }
    }
    void MovementInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        moveInput = new Vector3(moveX , RB.velocity.y, moveZ);
       
        if(moveInput != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveInput), rotateSpeed * Time.deltaTime);
        }
    }

    void PlayerMovement()
    {
      RB.velocity = moveInput * moveSpeed;
    }

    void CheckForEnemy()
    {
        Ray ray = new Ray(enemyCheck.position, enemyCheck.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, enemyCheckdistance))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("Enemy is visible");
                ShootGunBullet();
            }
        }
    }
    void CheckForEnemy2()
    {
        Ray ray = new Ray(enemyCheck2.position, enemyCheck2.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, enemyCheckdistance))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("Enemy is visible");
                ShootGunBullet();
            }
        }
    }

    void ShootGunBullet()
    {
      
        
            waitTime -= Time.deltaTime;
            if (waitTime <= 0f)
            {
                gunController.ShootBullet();
                waitTime = waitTimeDefault;
            }
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            playerHealth--;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(enemyCheck.position, enemyCheck.forward * enemyCheckdistance);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(enemyCheck2.position, enemyCheck2.forward * enemyCheckdistance);
    }
}
