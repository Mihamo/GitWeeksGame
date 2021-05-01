using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Camera_Control : MonoBehaviour
{
    static Camera Main;
    private Vector2 Mouseposition;
    public Transform target;
    float sensibility = 0;
    InterfaceClickOn hitobject;
    Ray ray;
    RaycastHit hit;
    public Vector3 mouseWorldPosition;
    void Awake()
    {
        Main = this.gameObject.GetComponent<Camera>();
    }

    public void MouseMove(InputAction.CallbackContext context)
    {
        Mouseposition = context.ReadValue<Vector2>();
        if (Mouseposition.x > Screen.width - 200)
        { sensibility = 2; }
        else if (Mouseposition.x < 200)
        { sensibility = -2; }
        else
        { sensibility = 0; }

    }
    private void FixedUpdate()
    {
        ray = Camera.main.ScreenPointToRay(Mouseposition);
        if (Physics.Raycast(ray, out hit, 10000))
        {
            mouseWorldPosition = hit.point;
        }
        transform.LookAt(target);
        transform.Translate(Vector3.right * sensibility * Time.deltaTime);
    }

    public void MouseClick(InputAction.CallbackContext context)
    {

        if (context.started)
        {
            if (hit.collider.gameObject.GetComponent<InterfaceClickOn>() != null)
            {
                hitobject = hit.collider.gameObject.GetComponent<InterfaceClickOn>();
                hitobject.Onclick();
                
            }
        }
        else if (context.canceled)
        {
            if(hitobject!=null)
            hitobject.Onrelease();
        }
        
    }





}
