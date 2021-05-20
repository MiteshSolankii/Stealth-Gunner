using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunController : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab = null;
    [SerializeField] Transform firePoint = null;

    public float waitTime = 0.5f,waitTimeDefault =0.5f;

    Transform player;

    public GameObject cineCam;

    private void Start()
    {
        cineCam = GameObject.Find("CineCam");
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }
    
    public void ShootPlayer()
    {
        transform.LookAt(player);
        waitTime -= Time.deltaTime;
        if (waitTime <= 0f)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            cineCam.GetComponent<Animator>().Play("CamShake");
            waitTime = waitTimeDefault;
        }
    }
}
