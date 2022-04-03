using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour{
    public GameObject contButton;
    public BalloonSO so;
    public GameObject overwriteDialog;
    public GameObject extraLong;

    private TMP_Text contText;
    private int lenSelected;
    void Start(){
        extraLong.SetActive(SaveSystem.HasPopped());
        overwriteDialog.SetActive(false);
        contText = contButton.GetComponentInChildren<TMP_Text>();
        var prefs = SaveSystem.Load();
        if (prefs.Equals("")){
            contButton.SetActive(false);
        }
        else{
            so.Load(prefs);
            contButton.SetActive(true);
        }
    }

    public void CheckExisting(int len){
        lenSelected = len;
        if (contButton.activeSelf){
            overwriteDialog.SetActive(true);
        }
        else{
            StartGame(len);
        }
    }

    public void StartOverwrite(){
        StartGame(lenSelected);
    }
    public void CancelOverwite(){
        overwriteDialog.SetActive(false);
    }
    public void StartGame(int length){
        so.firstStart = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        so.startTime = so.firstStart;
        so.popped = 0;
        switch (length){
            case 1:
                so.totalFallTime = 6000;
                break;
            case 2:
                so.totalFallTime = 60000;
                break;
            case 3:
                so.totalFallTime = 600000;
                break;
            case 4:
                so.totalFallTime = 6000000;
                break;
        }
        SaveSystem.Save(so);
        SceneManager.LoadScene("Scenes/Main");
    }

    public void ContinueGame(){
        SceneManager.LoadScene("Scenes/Main");
    }

    private void Update(){
        if (!contButton.activeSelf) return;
        
        var text = "";
        var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        if (so.popTime > now){
                var timeleft = (so.popTime - now)/1000;
                text = $"{timeleft} Sec until the Inevitable";
        }
        else{
            text = "The inevitable has happened";
        }
        contText.text = $"Continue\n({text})";
    }
}