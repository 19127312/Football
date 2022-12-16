using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Music and Sound Options")]
    public Image musicAndSoundOption;
    private AudioPlayerController audioPlayerController;

    public Sprite
        soundSprite,
        muteSprite,
        musicSprite;
    // Start is called before the first frame update
    void Start()
    {
        audioPlayerController = FindObjectOfType<AudioPlayerController>();
        if(audioPlayerController.audioState == "Music"){
            musicAndSoundOption.sprite = musicSprite;
        }
        else if(audioPlayerController.audioState == "Sound"){
            musicAndSoundOption.sprite = soundSprite;
        }
        else if(audioPlayerController.audioState == "Mute"){
            musicAndSoundOption.sprite = muteSprite;
        }
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
            audioPlayerController.Mute();
        }
        //Mute -> Music
        else if(musicAndSoundOption.sprite == muteSprite){
            musicAndSoundOption.sprite = musicSprite;
            Debug.Log("Music");
            audioPlayerController.UnMute();
        }
        //Music -> Sound only
        else if(musicAndSoundOption.sprite == musicSprite){
            musicAndSoundOption.sprite = soundSprite;
            Debug.Log("Sound");
            audioPlayerController.playSoundOnly();

        }
      
    }

}
