using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class JumpMaster : MonoBehaviour
{

    private Rigidbody mRig;
    private Vector3 mMovement;
    [SerializeField] private float mPowerPulse = 1f;
    [SerializeField] private float mSpeed = 1f;
    [SerializeField] private float mRotateSpeed = 1f;

    [SerializeField] private float mMaxSpeed = 10f;



    // Start is called before the first frame update
    void Start()
    {
        mRig = gameObject.GetComponent<Rigidbody>();
    }

    public void Jump(InputAction.CallbackContext context)
    {

        mRig.AddForce(Vector3.up * mPowerPulse, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {

        if (Mathf.Abs(mRig.velocity.magnitude) <= mMaxSpeed)
            mRig.AddForce(gameObject.transform.forward * mMovement.x * mSpeed, ForceMode.Force);
        transform.up = mRig.velocity.normalized;
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        mMovement = new Vector3(move.x, 0, move.y);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
