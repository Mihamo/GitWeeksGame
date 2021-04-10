using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private ParticleSystem particuleEmote;
    // Start is called before the first frame update
    void Start()
    {
        bool test = particuleEmote.isPlaying;
        var ps = particuleEmote.main;
        ps.loop = test;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
