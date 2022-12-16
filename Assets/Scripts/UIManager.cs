using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Music and Sound Options")]
    public Image musicAndSoundOption;

    public Sprite
        soundSprite,
        muteSprite,
        musicSprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void NewGame(){
        SceneManager.LoadScene("ChooseCharacterScene");
    }
    public void BackMainMenu(){
        SceneManager.LoadScene("MenuScene");
    }
    public void LoadGame(){
        Debug.Log("Load Game");
    }
    public void ExitGame(){
        Debug.Log("Exit Game");
        Application.Quit();
    }
    public void ChangeVolume(){
        // Sound only -> Mute
        if(musicAndSoundOption.sprite == soundSprite){
            musicAndSoundOption.sprite = muteSprite;
            Debug.Log("Mute");
        }
        //Mute -> Music
        else if(musicAndSoundOption.sprite == muteSprite){
            musicAndSoundOption.sprite = musicSprite;
            Debug.Log("Music");
        }
        //Music -> Sound only
        else if(musicAndSoundOption.sprite == musicSprite){
            musicAndSoundOption.sprite = soundSprite;
            Debug.Log("Sound");
        }
      
    }

}
