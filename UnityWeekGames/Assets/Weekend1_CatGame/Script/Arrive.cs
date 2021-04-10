using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : MonoBehaviour
{
    // Start is called before the first frame update

    Collider mCol; 

    [SerializeField] Canvas win;
    void Start()
    {
        mCol = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        TestController mCont;
        if(other.gameObject.TryGetComponent<TestController>(out mCont))
        {
            win.gameObject.SetActive(true);
            
        }
    }
}
