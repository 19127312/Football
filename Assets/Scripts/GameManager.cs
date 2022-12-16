using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
   static GameManager instance;
    public List<Character> charactersInGame = new List<Character>();
 
    private Character selectedCharacter;
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
       selectedCharacter=charactersInGame[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeLeftCharacter(){
        int index= charactersInGame.IndexOf(selectedCharacter);
        if(index==0){
            index=charactersInGame.Count-1;
            selectedCharacter=charactersInGame[index];
        }else{
            index--;
            selectedCharacter=charactersInGame[index];
        }
    }
    public void changeRightCharacter(){
        int index= charactersInGame.IndexOf(selectedCharacter);
        if(index==charactersInGame.Count-1){
            index=0;
            selectedCharacter=charactersInGame[index];
        }else{
            index++;
            selectedCharacter=charactersInGame[index];
        }
    }
    public Character GetSelectedCharacter(){
        return selectedCharacter;
    }
}
