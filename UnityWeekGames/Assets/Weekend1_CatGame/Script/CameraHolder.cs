using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float speed = 9;
    public float speedRotation = 6;
    public static CameraHolder singleton;
    Quaternion oldrotation = new Quaternion();

    private void Awake()
    {

        singleton = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (target == null)
            return;

        Vector3 p = Vector3.Lerp(target.position, transform.position, Time.deltaTime * speed);
            transform.position = p;

        Quaternion r = Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime * speedRotation);
            transform.rotation = r;

        oldrotation = target.rotation;
    }

    bool GetRotateDirection(Quaternion from, Quaternion to)
    {
        float fromY = from.eulerAngles.y;
        float toY = to.eulerAngles.y;
        float clockWise = 0f;
        float counterClockWise = 0f;

        if (fromY <= toY)
        {
            clockWise = toY - fromY;
            counterClockWise = fromY + (360 - toY);
        }
        else
        {
            clockWise = (360 - fromY) + toY;
            counterClockWise = fromY - toY;
        }
        return (clockWise <= counterClockWise);
    }
}
