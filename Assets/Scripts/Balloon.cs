using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Balloon : MonoBehaviour{
    public BalloonSO so;
    public TMP_Text text;
    public float startDist;
    public GameObject restartButton;
    public ParticleSystem psys;
    private Collider col;
    private Renderer bRend;
    private AudioSource popSound;

    private long now => DateTimeOffset.Now.ToUnixTimeMilliseconds();

    private void Start(){
        popSound = GetComponent<AudioSource>();
        col = GetComponentInChildren<Collider>();
        bRend = GetComponentInChildren<MeshRenderer>();
        restartButton.SetActive(so.popped != 0);
        bRend.enabled = so.popped == 0;
        if (so.popped != 0){
            psys.Play();
            popSound.Play();
        }
    }

    public void Restart(){
        SceneManager.LoadScene("Scenes/Menu");
    }

    private void Update(){
        if (Input.GetKeyUp(KeyCode.Q)){
            SceneManager.LoadScene("Scenes/Menu");
        }
        if (Input.GetMouseButtonUp(0) && so.popped == 0){
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (col.Raycast(ray, out RaycastHit hit, 100)){
                so.startTime = now;
                so.DecrementFallTime();
                SaveSystem.Save(so);
            }
        }

        var timeleft = so.popTime - now;
        var dist = timeleft / (float) so.totalFallTime;
        if (dist > 0)
            transform.position = Vector3.up * (dist * startDist);

        if (dist <= 0){
            transform.position = Vector3.zero;
            if (so.popped == 0){
                psys.Play();
                popSound.Play();
                bRend.enabled = false;
                so.popped = so.popTime;
                restartButton.SetActive(true);
                SaveSystem.Save(so);
            }
            var totalTime =  so.popped - so.firstStart;
            if (totalTime > 3 * 60 * 1000){
                SaveSystem.SavePopped();
            }
            var span = TimeSpan.FromMilliseconds(totalTime);
            text.text = "POP!\nYou Kept The Inevitable At Bay For:\n"
                        +
                        $"{span.Days} Days, {span.Hours} Hours, {span.Minutes} Min, {span.Seconds} Seconds";
        }
        else{
            text.text = "Time Remaining Until the Inevitable:\n" +
                        TimeSpan.FromMilliseconds(timeleft).ToString("h\\:mm\\:ss\\.ff");
        }
    }
}