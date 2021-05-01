using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Character : MonoBehaviour, InterfaceClickOn
{
    private static int GlobalID =0;
    private int mId;
    [SerializeField] int mlife;

    public int Life{
        set {mlife-= value;
        if(mlife<=0)
        {
            Death();
        }}
    }
    public int ID{
        get{return mId;}
    }
    // Start is called before the first frame update
    void Start()
    {
        mId = GlobalID;
        GlobalID++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Death()
    {

    }


    private void OnCollisionEnter(Collision other) {
        gameObject.SetActive(false);
    }

    void InterfaceClickOn.CollideCharacter(float power)
    {
       // Life = (int)power;   

    }
    void InterfaceClickOn.Onclick()
    {
        Player_Character.Player.Target=this;
    }
    void InterfaceClickOn.Onrelease()
    {
        Player_Character.Player.UnTarget=this;
    }
    

}
