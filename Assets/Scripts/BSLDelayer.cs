using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;


public class BSLDelayer : MonoBehaviour
{

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delayer());
       

    }


    IEnumerator Delayer()
    {
        //player.GetComponent(UnityEngine.Video.VideoPlayer);
       var video =  player.GetComponent<UnityEngine.Video.VideoPlayer>();
        // player.Get
        // var video = player.AddComponent<UnityEngine.Video.VideoPlayer>();
        // NativeVideoPlayer video = player.GetComponent("VideoPlayer");
        //var video = player.GetComponent("VideoPlayer");

        video.Play();

        video.Pause();

        yield return new WaitForSeconds(Convert.ToSingle(3));

        video.Play();

    }        // Update is called once per frame
        void Update()
    {
        
    }
}
