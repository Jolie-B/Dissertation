using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

public class history : MonoBehaviour{
    public TMP_Text textBox1;
    public TMP_Text textBox2;
    public TMP_Text textBox3;

    public string csvName;
  

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(TheSequence());
    }


    IEnumerator TheSequence(){
        yield return new WaitForSeconds(Convert.ToSingle(5));

        //set up a new file reader
        Debug.LogWarning("entered read");
        string filePath = "C:\\Users\\students20-21\\Documents\\Jolie\\My project\\My project\\Assets\\Transcripts\\" + csvName + ".csv";
        StreamReader reader = new StreamReader(filePath);

        //until end of file
        bool end = false;
        while(!end){
            string data = reader.ReadLine();
            if (data == null){
                end = true;
                break;
            }
            
            //split sentence into list of words
            string fullString = data.ToString();
            string[] words = fullString.Split(" ");
            
            string temp  = "";

            //for every word in sentence
            for (int i = 0; i < words.Length; i++) 
                {
                // add that word to the displayed sentence after an ammount of time, determined by the #chars in that word
                temp = temp + words[i] + " ";
                textBox1.text = temp;
                yield return new WaitForSeconds(Convert.ToSingle(0.15)*words[i].Length);
                }
            //pause at end of sentence
            yield return new WaitForSeconds(Convert.ToSingle(0.8));

            // move text down into next text box
            textBox3.text = textBox2.text;
            textBox2.text = temp;
        }
   
    }

}
