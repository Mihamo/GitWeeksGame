using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class TestController : MonoBehaviour
{

    // Component
    private Rigidbody mRig;
    private Collider mCol;
    private Climb CptClimb;

    //Mouvement
    private Vector2 mMovement;
    private float moveAmount;
    private Vector3 moveDirection;
    [SerializeField] private float SizeModel = 2f;
    [SerializeField] private float mJumpPower = 1f;
    [SerializeField] private float mSpeed = 50f;
    public float mRotaSpeed = 9;
    [SerializeField] private float mMaxSpeed = 100f;


    // Cam
    Transform camHolder;
    Vector3 camYforward;

    // Start is called before the first frame update
    void Start()
    {
        mRig = gameObject.GetComponent<Rigidbody>();
        mRig.constraints = RigidbodyConstraints.FreezeRotation;
        mCol = GetComponent<Collider>();
        CptClimb = GetComponent<Climb>();
        CptClimb.mStopClimbing.AddListener(disableClimbing);
        CptClimb.mStartClimb.AddListener(enableClimbing);
        camHolder = CameraHolder.singleton.transform;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Vector3 dir = Vector3.zero;
        if (OnGround())
            dir = transform.up * mJumpPower;
        else if (CptClimb.isClimbing)
        {
            CptClimb.StopClimb();
            dir = -transform.forward * mJumpPower;
        }

        mRig.AddForce(dir, ForceMode.Impulse);
    }

    bool OnGround()
    {
        Vector3 origin = transform.position;
        Vector3 dir = -transform.up;
        RaycastHit hit;

        if (Physics.Raycast(origin, dir, out hit, SizeModel / 2 + 0.3f))
        {

            return true;
        }
        return false; ;
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        mMovement = new Vector2(move.x, move.y);
        
    }

    void FixedUpdate()
    {
        float dTime = Time.deltaTime;
        if (CptClimb.isClimbing)
        {
            CptClimb.tick(dTime);
            return;
        }
        else if (CptClimb.CheckForClimb(mRig.velocity.magnitude*0.4f))
        {
            
        }
        else
        {

        }
        Mouvement();


    }

    void enableClimbing(float speed)
    {
        CptClimb.isClimbing = true;
        mRig.isKinematic = true;

    }
    void disableClimbing()
    {
        CptClimb.isClimbing = false;
        mRig.isKinematic = false;
       

        Quaternion lookdir = Quaternion.LookRotation(-transform.forward);
        Quaternion targetRot = Quaternion.Slerp(transform.rotation, lookdir, Time.deltaTime * mRotaSpeed);
        transform.rotation = lookdir;
    }
    void Mouvement()
    {
        if (Mathf.Abs(mRig.velocity.magnitude) <= mMaxSpeed)
        {
            Vector3 dir = transform.forward * (mSpeed * moveAmount);
            //mRig.velocity = dir;
            mRig.AddForce(dir, ForceMode.Force);
        }

        camYforward = camHolder.forward;

        Vector3 v = mMovement.y * camYforward;

        Vector3 h = mMovement.x * camHolder.right;

        moveDirection = (v + h).normalized;
        moveAmount = Mathf.Clamp01(Mathf.Abs(mMovement.x) + Mathf.Abs(mMovement.y));

        Vector3 targetDir = moveDirection;
        targetDir.y = 0;
        if (targetDir == Vector3.zero)
            targetDir = transform.forward;

        Quaternion lookdir = Quaternion.LookRotation(targetDir);
        Quaternion targetRot = Quaternion.Slerp(transform.rotation, lookdir, Time.deltaTime * mRotaSpeed);
        transform.rotation = targetRot;

    }

    public void Quit(InputAction.CallbackContext context)
    {
        Application.Quit();
    }

}
