using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaugeOrientation : MonoBehaviour
{
    
    public Transform target;
    public float speed = 9;
    public float speedRotation = 6;
    Transform Camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   
    private void FixedUpdate()
    {
        if (target == null)
            return;


        Vector3 p = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
        Quaternion r = Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime * speedRotation);

            transform.position = p;
            transform.rotation = r;
    }
}
