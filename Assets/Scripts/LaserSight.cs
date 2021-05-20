using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserSight : MonoBehaviour
{
   public LineRenderer lr;

   

    public float distance = 10f;
    void Start()
    {
        lr = GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (lr != null)
        {
            lr.SetPosition(0, new Vector3(0, 0, 0));
            lr.SetPosition(1, new Vector3(0, 0, distance ));
        }
    }
}
