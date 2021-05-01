using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMove : MonoBehaviour
{
    private Rigidbody rb;
    private Collider col;

    private Animator Anim;
    public float dashSpeed;
    public float startDashTime;
    private bool isDash = false;
    private Vector3 mdirection;

    Coroutine Timer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        Anim = GetComponent<Animator>();
        
        isDash = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDash)
            rb.velocity = mdirection;
    }

    public void Dash(Vector2 direction, DashMode mode = 0)
    {
        mdirection = new Vector3(direction.x, 0, direction.y).normalized * dashSpeed;
        isDash = true;
        Timer = StartCoroutine(TimeDash(startDashTime));
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (Timer != null)
        {
            StopCoroutine(Timer);
            Timer = null;
        }
        isDash = false;
        rb.velocity =Vector3.zero;
       
    
    }
    IEnumerator TimeDash(float timetoWait)
    {

        yield return new WaitForSecondsRealtime(timetoWait);
        isDash = false;

    }

    public enum DashMode
    {
        Time = 0,
        collider = 1,
        Target = 2,
    }
}
