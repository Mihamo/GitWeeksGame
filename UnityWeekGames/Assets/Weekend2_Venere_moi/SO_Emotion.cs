using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor.Animations;

[CreateAssetMenu(fileName = "Emotion", menuName = "UnityWeekGames/SO_Emotion", order = 0)]
public class SO_Emotion : ScriptableObject
{
    [Header("FX")]
    [SerializeField] private ParticleSystem particuleEmote;
    [SerializeField] bool mloop;

    [Header("Animation")]
    [SerializeField] Animator AnimEmotion;
    
    private int StateEmote;
    private bool isActive;


    public void ActiveEmotion(Animator character =null)
    {
        isActive = true;

        if (particuleEmote != null)
        {
            
            var ps = particuleEmote.main;
            ps.loop = mloop;
            particuleEmote.Play();
        }

         if (AnimEmotion != null && character!=null)
        {
            
        }

    }
    public void StopEmotion()
    {

    }
    // Start is called before the first frame update
    void Start()
    {


    }


    void Setup()
    {

    }




    // Update is called once per frame
    void Update()
    {

    }


}
