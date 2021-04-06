using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.InputSystem;
public class Climb : MonoBehaviour
{
    //  public Animator anim;
    public bool isClimbing;
    bool inPosition;
    bool isLerping;
    float T;
    Vector3 startPos, targetPos;
    Quaternion startRot, targetRot;

    Transform helper;
    public float positionOffset;
    public float offsetFromWall = 0.3f;
    public float speed_multiplier = 0.2f;
    public float climbSpeed = 3;
    private float ClimbSpeedBase;
    public float rotateSpeed = 5;
    public float inAngleDis = 1;
    float delta;

    [SerializeField] private float TimerClimb = 3;
    public UnityEvent<float> timerChange;
    public float Timer
    {
        get { return TimerClimb; }
        set
        {
            TimerClimb = value;
            timerChange.Invoke(TimerClimb);
        }

    }

    Vector2 InputOrientation;

    public UnityEvent<float> mStartClimb;
    public UnityEvent mStopClimbing;
    // Start is called before the first frame update
    void Start()
    {

        Init();

    }

    public void OnMovementClimb(InputAction.CallbackContext context)
    {
        InputOrientation = context.ReadValue<Vector2>();
    }
    void Init()
    {
        helper = new GameObject("climb helper").transform;
        helper.gameObject.AddComponent<BoxCollider>();

        ClimbSpeedBase = climbSpeed;
        mStartClimb.AddListener(startspeed);

    }
    void InitForClimb(RaycastHit hit, float speedbonus)
    {
        isClimbing = true;

        helper.transform.rotation = Quaternion.LookRotation(-hit.normal);
        startPos = transform.position;
        targetPos = hit.point + (hit.normal * offsetFromWall);
        T = 0;
        inPosition = false;
        mStartClimb.Invoke(speedbonus);
        // anim.CrossFade("climb_idle",2); //PlayAnimation

    }
    public bool CheckForClimb(float speedbonus)
    {
        Vector3 origin = transform.position;
        Vector3 dir = transform.forward;
        RaycastHit hit;

        if (Physics.Raycast(origin, dir, out hit, 0.5f))
        {
            helper.position = PosWithOffset(origin, hit.point);
            InitForClimb(hit, speedbonus);
            return true;
        }
        return false;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (isClimbing)
        {
            isClimbing = false;
            mStopClimbing.Invoke();

        }
    }
    public void LookForGround()
    {
        Vector3 origin = transform.position;
        Vector3 dir = -Vector3.up;
        RaycastHit hit;
        if (Physics.Raycast(origin, dir, out hit, 1.1f))
        {
            isClimbing = false;
            mStopClimbing.Invoke();
        }
    }
    public void tick(float Timedelta)
    {
        Timer -= Timedelta;

        delta = Timedelta * 5;

        if (Timer < 0)
        {
            mStopClimbing.Invoke();
            return;
        }

        if (!inPosition)
        {
            GetInPosition();
            return;
        }

        if (!isLerping)
        {
            float m = Mathf.Abs(InputOrientation.x) + Mathf.Abs(InputOrientation.y);

            Vector3 h = helper.right * InputOrientation.x;
            Vector3 v = helper.up * InputOrientation.y;
            Vector3 moveDir = (h + v).normalized;

            bool canMove = CanMove(moveDir);
            if (!canMove || moveDir == Vector3.zero)
                return;

            T = 0;
            isLerping = true;
            startPos = transform.position;
            // Vector3 tp = helper.position - transform.position;

            targetPos = helper.position;

        }
        else
        {
            T += Timedelta * climbSpeed;
            if (T > 1)
            {
                T = 1;
                isLerping = false;
            }
            Vector3 cp = Vector3.Lerp(startPos, targetPos, T);
            transform.position = cp;
            transform.rotation = Quaternion.Slerp(transform.rotation, helper.rotation, Timedelta * rotateSpeed);
            LookForGround();
        }
    }

    bool CanMove(Vector3 moveDir)
    {

        Vector3 origin = transform.position;
        float dis = positionOffset;
        Vector3 dir = moveDir;

        RaycastHit hit;

        if (Physics.Raycast(origin, dir, out hit, dis))
        {
            return false;
        }

        origin += moveDir * dis;
        dir = helper.forward;
        float dis2 = inAngleDis;


        if (Physics.Raycast(origin, dir, out hit, dis))
        {
            helper.position = PosWithOffset(origin, hit.point);
            helper.rotation = Quaternion.LookRotation(-hit.normal);
            return true;
        }

        origin += dir * dis2;
        dir = -Vector3.up;

        if (Physics.Raycast(origin, dir, out hit, dis2))
        {
            float angle = Vector3.Angle(helper.up, hit.normal);

            if (angle < 40)
            {
                helper.position = PosWithOffset(origin, hit.point);
                helper.rotation = Quaternion.LookRotation(-hit.normal);
                return true;
            }
        }

        return false;
    }
    void GetInPosition()
    {
        T += delta;

        if (T > 1)
        {
            T = 1;
            inPosition = true;
            //enable ik 
        }

        Vector3 tp = Vector3.Lerp(startPos, targetPos, T);
        transform.position = tp;
        transform.rotation = Quaternion.Slerp(transform.rotation, helper.rotation, delta * rotateSpeed);
    }

    Vector3 PosWithOffset(Vector3 origin, Vector3 target)
    {
        Vector3 direction = origin - target;
        direction.Normalize();
        Vector3 offset = direction * offsetFromWall;
        return target + offset;
    }


    private void Update()
    {
        if (!isClimbing && Timer < 3)
            Timer += Time.deltaTime;

        if (climbSpeed > ClimbSpeedBase)
            climbSpeed -= Time.deltaTime * 5;
        else if (climbSpeed < ClimbSpeedBase)
            climbSpeed = ClimbSpeedBase;
    }

    private void startspeed(float speed)
    {
        climbSpeed += speed;

    }



}
