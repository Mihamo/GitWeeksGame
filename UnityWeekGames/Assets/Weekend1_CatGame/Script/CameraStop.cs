using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStop : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Target;
    Vector3 offsetCam;
    float InitialDistance;
    void Start()
    {
        InitialDistance = Mathf.Abs((transform.position - Target.transform.position).magnitude);
        offsetCam = transform.localPosition;
        
    }

    RaycastHit hit;
    // Update is called once per frame
    void Update()
    {
        
       
        if(Physics.Linecast( Target.transform.position, transform.position,out hit))
        {

            transform.localPosition = new Vector3(0,5, - Vector3.Distance(Target.transform.position,hit.point));
        }
        else 
        {
            transform.localPosition=offsetCam;
        }
    }
}
