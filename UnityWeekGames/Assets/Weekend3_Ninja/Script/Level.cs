using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] List<Target_Character> NbEnnemies;
    
   public static Level GameMode;

    public LevelState StateOfGame;
    [SerializeField] Canvas Win ;
    [SerializeField] Canvas loose ;
    public int NbEnnemiesInLevel
    {
        get
        {
            return NbEnnemies.Capacity;
        }
    }

  public void EndGame()
    {
        

        if(EnnemieAlive())
        {
            Win.gameObject.SetActive(true);
        }
        else
        {
            loose.gameObject.SetActive(true);
        }
    }

    public bool StillEnnemies(int Nb)
    {
        return Nb<NbEnnemies.Count;
    }

    private bool EnnemieAlive()
    {
        foreach (Target_Character item in NbEnnemies)
        {
            Debug.Log(item.gameObject.activeSelf);
              if(item.gameObject.activeSelf)
              {
                  return false;
              }  

        }
        return true;
    }
    
    private void Awake() {
        GameMode = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        StateOfGame = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public  enum LevelState
    {
        Prepare = 0,
        Attack =1,

    }
}
