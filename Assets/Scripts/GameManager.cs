using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
   static GameManager instance;
    public List<Character> charactersInGame = new List<Character>();
    public List<Character> charactersOwn = new List<Character>();
    public List<Character> charactersNotOwn = new List<Character>();

    private void Awake() {
        ManageSingleton();
    }

    private void ManageSingleton()
    {

        if(instance!=null){
            gameObject.SetActive(false);
            Destroy(gameObject);
        }else{
            instance=this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
