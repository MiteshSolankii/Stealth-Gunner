using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed;

   

    //public int bulletCount = 3;
   

    public GameObject cineCam;

   

    void Start()
    {
        cineCam = GameObject.Find("CineCam");

    }
    // Update is called once per frame
    void Update()
    {
         
     
    }
    public void ShootBullet()
    {
        if (!GameManager.instance.isGameOver)
        {
           
            
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                bullet.GetComponent<BulletController>().speed = bulletSpeed;
                cineCam.GetComponent<Animator>().Play("CamShake");
            
        }
    }

}
