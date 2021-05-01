using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Venere : MonoBehaviour, InterfaceClickOn
{
    Camera_Control main;
    Collider m_Contact;
    Rigidbody m_rigid;
    [SerializeField] float Power = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        main = Camera.main.GetComponent<Camera_Control>();
        m_rigid = GetComponent<Rigidbody>();
    }

    bool selected = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (selected)
        {
            Vector3 dir = main.mouseWorldPosition - transform.position;
            Vector3 moveDir = dir.normalized;
            m_rigid.velocity = moveDir * 10;

        }
    }



    void InterfaceClickOn.Onclick()
    {

        selected = true;
        gameObject.layer = 2;
        m_rigid.constraints = RigidbodyConstraints.FreezePositionY;
        m_rigid.useGravity=false;

    }

    void InterfaceClickOn.Onrelease()
    {
        gameObject.layer = 0;
        selected = false;
        m_rigid.constraints = RigidbodyConstraints.None;
        m_rigid.useGravity=true;
    }

    void InterfaceClickOn.CollideCharacter(float powerEffect)
    {
        gameObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision other)
    {
        InterfaceClickOn Ivener;

        if (other.collider.TryGetComponent<InterfaceClickOn>(out Ivener)&& other.collider.GetComponent<MaterialGradientColor>())
        {
            Ivener.CollideCharacter(Power);
        }



    }
}
