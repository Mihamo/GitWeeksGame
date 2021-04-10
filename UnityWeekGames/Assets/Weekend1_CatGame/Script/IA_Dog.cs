using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Dog : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform target;//the enemy's target
    [SerializeField] float moveSpeed = 3; //move speed 
    [SerializeField] float rotationSpeed = 3; //speed of turning

    TestController playertarget;
    [SerializeField] GameObject cry;

    Transform myTransform; //current transform data of this enemy
    SphereCollider mhit;
    void Awake()
    {
        myTransform = transform; //cache transform data for easy access/preformance
    }

    void Start()
    {

        //target the player

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.TryGetComponent<TestController>(out playertarget))
            KillManager.singleton.KillPlayer();
    }
    void Update()
    {
        //rotate to look at the player
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
        Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);

        //move towards the player
        myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;

        if (Random.Range(0.0f, 1.0f) > 0.999f)
        {
            if (!cry.activeSelf)
            {
                cry.SetActive(true);
                StartCoroutine(Aboiment());
            }
        }
    }

    IEnumerator Aboiment()
    {
        yield return new WaitForSeconds(1);
        cry.SetActive(false);
    }
}
