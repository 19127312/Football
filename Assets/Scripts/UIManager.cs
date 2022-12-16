using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    [Header("Music and Sound Options")]
    public Image musicAndSoundOption;
    private AudioPlayerController audioPlayerController;
    private GameManager gameManager;


    public Sprite
        soundSprite,
        muteSprite,
        musicSprite;
    
    [Header("Current Character")]
    public Image currentCharacterImage;
    public GameObject currentPricePanel;
    public TextMeshProUGUI  currentPriceText;
    private UnityAction newGameAction;
    private UnityAction backMainMenuAction;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
      
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
        audioPlayerController.playButtonClickClip();
        newGameAction = () => SceneManager.LoadScene("ChooseCharacterScene");
        StartCoroutine(HoldHostage(newGameAction));   
        
    }
    public void BackMainMenu(){
        audioPlayerController.playButtonClickClip();
        backMainMenuAction = () => SceneManager.LoadScene("MenuScene");
        StartCoroutine(HoldHostage(backMainMenuAction));   
        
    }
    public void LoadGame(){
        audioPlayerController.playButtonClickClip();
    }
    public void ExitGame(){
        Application.Quit();
    }
    public void LevelUpPlayer(){
        audioPlayerController.playButtonClickClip();
    }
    public void OneVsOne(){
        audioPlayerController.playButtonClickClip();

    }
    public void OneVsAI(){
        audioPlayerController.playButtonClickClip();

    }
    public void ChangeVolume(){
        // Sound only -> Mute
        if(musicAndSoundOption.sprite == soundSprite){
            musicAndSoundOption.sprite = muteSprite;
            audioPlayerController.Mute();
        }
        //Mute -> Music
        else if(musicAndSoundOption.sprite == muteSprite){
            musicAndSoundOption.sprite = musicSprite;
            audioPlayerController.UnMute();
        }
        //Music -> Sound only
        else if(musicAndSoundOption.sprite == musicSprite){
            musicAndSoundOption.sprite = soundSprite;
            audioPlayerController.playSoundOnly();

        }
      
    }
    public void goToLeftCharacter(){
                audioPlayerController.playButtonClickClip();

        gameManager.changeLeftCharacter();
        updateCurrentCharacter(gameManager.GetSelectedCharacter());
    }
    public void goToRightCharacter(){
                audioPlayerController.playButtonClickClip();

        gameManager.changeRightCharacter();
        updateCurrentCharacter(gameManager.GetSelectedCharacter());
    }
    public void updateCurrentCharacter(Character character){
        currentCharacterImage.sprite = character.Image;
        currentPriceText.text = character.GoldToBuy.ToString();
        if(character.IsOwn){
            currentPricePanel.SetActive(false);
        }else{
            currentPricePanel.SetActive(true);
        }
    }
    private IEnumerator HoldHostage(UnityAction callback)
    {
        yield return new WaitForSeconds(0.5f);
        callback.Invoke();
    }
    
}
