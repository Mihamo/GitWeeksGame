using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor.Animations;
using UnityEngine.VFX ;

[CreateAssetMenu(fileName = "Emotion", menuName = "UnityWeekGames/SO_Emotion", order = 0)]
public class SO_Emotion : ScriptableObject
{
    [Header("FX")]
    public VisualEffectAsset particuleEmote;

     
    [SerializeField] bool mloop;
    [SerializeField] Color colorOfEmotion;
    public Color EmotionColor{
        get{return colorOfEmotion;}
    }

    [Header("Animation")]
    [SerializeField] Animator AnimEmotion;


    private void Start() {
        
    
    }

    private float powerOfEmote;

    public float Power{
        get { return powerOfEmote;}
        set {powerOfEmote = value;
        ActiveEmotion();}
    }
    private bool isActive;


    public void ActiveEmotion()
    {
        isActive = true;

        if (particuleEmote != null)
        {
            //particuleEmote.
        
            
        }

        if (AnimEmotion != null)
        {

        }

    }
   
    // Start is called before the first frame update


    void Setup()
    {

    }




    // Update is called once per frame
    void Update()
    {

    }


}
