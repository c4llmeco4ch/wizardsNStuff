using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XboxCtrlrInput;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		for(int i = 0; i < 4; i++) {
			GameInit.elementChoices.Add(new Element[2]);
			GameInit.colorList.Add(PlayerColor.Blue);
		}
		
		GameInit.elementList = new LinkedList<GameInit.ElementSprite>();
		new GameInit.ElementSprite(new Earth(), Resources.Load("UI Art Assets/Selection/EarthText", typeof(Sprite)) as Sprite, 
		                           Resources.Load("UI Art Assets/Selection/EarthElementBox", typeof(Sprite)) as Sprite);
		new GameInit.ElementSprite(new Air(), Resources.Load("UI Art Assets/Selection/AirText", typeof(Sprite)) as Sprite, 
		                           Resources.Load("UI Art Assets/Selection/AirElementBox", typeof(Sprite)) as Sprite);
		new GameInit.ElementSprite(new Fire(), Resources.Load("UI Art Assets/Selection/FireText", typeof(Sprite)) as Sprite, 
		                           Resources.Load("UI Art Assets/Selection/FireElementBox", typeof(Sprite)) as Sprite);
		new GameInit.ElementSprite(new Water(), Resources.Load("UI Art Assets/Selection/WaterText", typeof(Sprite)) as Sprite, 
		                           Resources.Load("UI Art Assets/Selection/WaterElementBox", typeof(Sprite)) as Sprite);
		Debug.Log("Create List");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void play(bool tutorial) {
        GameInit.tutorial = tutorial;
        Application.LoadLevel("ChooseElementScene");
    }
    
    public void credits() {
    	Application.LoadLevel("Credits");
    }
    
    public void quit() {
    	Application.Quit();
    }
}
