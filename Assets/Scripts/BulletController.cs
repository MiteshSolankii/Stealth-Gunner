using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 5f;
    private GunController gun;
    
    void Start()
    {
        gun = FindObjectOfType<GunController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.layer == 6)
        {
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
