using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CameraControler : MonoBehaviour
{
    public static CameraControler MainCamera;

    Player_Character mPlayer;
    // Start is called before the first frame update
    private void Awake()
    {
        MainCamera = this;
    }

    private void Start() {
      mPlayer = Player_Character.Player;
    }
    // Update is called once per frame
    void Update()
    {

    }





}
