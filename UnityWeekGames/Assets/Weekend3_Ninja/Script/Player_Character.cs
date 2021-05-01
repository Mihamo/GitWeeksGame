using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Character : MonoBehaviour
{
    public static Player_Character Player;
    private DashMove Cpt_Dash;
    private Level GameMode;
    public List<Target_Character> lTargetOrders = new List<Target_Character>();
    private Vector2 Mouseposition = new Vector2();
    InterfaceClickOn hitobject;
    public Vector3 mouseWorldPosition;

    private Animator Anim;

    
    Ray ray;
    RaycastHit hit;


    public Target_Character Target
    {
        get
        {
            Target_Character TargetReturn = lTargetOrders[0];
            lTargetOrders.RemoveAt(0);
            return TargetReturn;
        }
        set
        {
            lTargetOrders.Add(value);
        }
    }

    public Target_Character UnTarget
    {
        set
        {
            lTargetOrders.Remove(value);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Player = this;
        Cpt_Dash = GetComponent<DashMove>();
        Anim = GetComponent<Animator>();
        GameMode = Level.GameMode;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool KillMove(Transform Target)
    {
        RaycastHit isTarget;
        Physics.Raycast(transform.position, Target.position - transform.position, out isTarget);
       

        if (isTarget.collider != null && isTarget.collider.gameObject.GetComponent<InterfaceClickOn>() != null)
            Cpt_Dash.Dash(new Vector2(Target.position.x - transform.position.x, Target.position.z - transform.position.z));
        else
        {
            Debug.Log("Wrong Move");
        }

        return (hit.collider.transform == Target);
    }

    public bool CheckTarget(Transform Target)
    {
        return true;
    }


    private void FixedUpdate()
    {

    }


    public void MouseMove(InputAction.CallbackContext context)
    {
        Mouseposition = context.ReadValue<Vector2>();
    }

    public void MouseClick(InputAction.CallbackContext context)
    {

        if (GameMode.StateOfGame == 0 && context.started)
        {
            ray = Camera.main.ScreenPointToRay(Mouseposition);
            if (Physics.Raycast(ray, out hit, 10000))
            {
                mouseWorldPosition = hit.point;
            }

            if (hit.collider != null && hit.collider.gameObject.GetComponent<InterfaceClickOn>() != null)
            {
                if (!IsInList(hit.collider.gameObject.GetComponent<Target_Character>()))
                {
                    hitobject = hit.collider.gameObject.GetComponent<InterfaceClickOn>();
                    hitobject.Onclick();
                    if(!GameMode.StillEnnemies(lTargetOrders.Count))
                    {
                        StartCoroutine(ActionFight());
                        GameMode.StateOfGame = Level.LevelState.Attack;
                    }

                }
                else
                {
                    hitobject.Onrelease();
                }
            }

        }
    }

    bool IsInList(Target_Character CheckObject)
    {
        bool inList = false;
        foreach (var item in lTargetOrders)
        {
            if (item.ID == CheckObject.ID)
            {
                inList = true;
            }
        }

        return inList;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!(other.gameObject.tag == "Floor"))
            Anim.SetBool("Attack", true);

    }

    public void EndAttack()
    {
        Anim.SetBool("Attack", false);
    }

  
    private IEnumerator ActionFight()
    {
        foreach(Target_Character Item in lTargetOrders)
        {
        Debug.Log("je suis dans la coroutine");
            if(KillMove(Item.transform))
            {
                Debug.Log("coroutine");
                 GameMode.EndGame();
                yield break;
            }
            
            yield return new WaitForSeconds(1f);

        }

       
    }
}
