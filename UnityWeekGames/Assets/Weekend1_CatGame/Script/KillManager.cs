using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillManager : MonoBehaviour
{
    [SerializeField] Transform Spawner;
    [SerializeField] TestController player;
    
    public static KillManager singleton;
    public void KillPlayer()
    {
        player.transform.position=Spawner.position;
        player.transform.rotation=Spawner.rotation;
        player.GetComponent<Rigidbody>().velocity=Vector3.zero;

    }

    // Start is called before the first frame update
    void Start()
    {
        singleton = this;
     
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        
    }
}
