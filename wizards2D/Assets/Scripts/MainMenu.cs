﻿using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void play(bool tutorial) {
        GameInit.tutorial = tutorial;
        Application.LoadLevel("ChooseElementScene");
    }
}
