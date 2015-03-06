﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Collections;
using XboxCtrlrInput;


public class ElementSelector : MonoBehaviour {
	public Button left;
	public Button right;
	public int playerNum;
	public int elementNum;
    public bool selected;
	
	public Image selectedElementText;
    public Image background;

	private Element selectedElement;
    
    private LinkedListNode<GameInit.ElementSprite> cur;
    
    const float modifier = 0.3f;
    
    
    int counter = 0;
    const int time = 15;

	// Use this for initialization
    void Start () {
        if(GameInit.elementList == null){
            Debug.Log("Create List");
            GameInit.elementList = new LinkedList<GameInit.ElementSprite>();
            new GameInit.ElementSprite(new Earth(), Resources.Load("UI Art Assets/Selection/EarthText", typeof(Sprite)) as Sprite, 
                                       Resources.Load("UI Art Assets/Selection/EarthElementBox", typeof(Sprite)) as Sprite);
            new GameInit.ElementSprite(new Air(), Resources.Load("UI Art Assets/Selection/AirText", typeof(Sprite)) as Sprite, 
                                       Resources.Load("UI Art Assets/Selection/AirElementBox", typeof(Sprite)) as Sprite);
            new GameInit.ElementSprite(new Fire(), Resources.Load("UI Art Assets/Selection/FireText", typeof(Sprite)) as Sprite, 
                                       Resources.Load("UI Art Assets/Selection/FireElementBox", typeof(Sprite)) as Sprite);
            new GameInit.ElementSprite(new Water(), Resources.Load("UI Art Assets/Selection/WaterText", typeof(Sprite)) as Sprite, 
                                       Resources.Load("UI Art Assets/Selection/WaterElementBox", typeof(Sprite)) as Sprite);
        }
//        if(playerNum > 2 && XCI.GetNumPluggedCtrlrs() <= 2  && playerNum > XCI.GetNumPluggedCtrlrs()) {
//            this.gameObject.SetActive(false);
//            return;
//        }
        if(!selected)
            background.color = new Color(background.color.r - modifier, background.color.g - modifier, background.color.b - modifier);
		if(elementNum == 0)
            cur = GameInit.elementList.First;
        else if(elementNum ==1)
            cur = GameInit.elementList.First.Next;
		selectedElement = (cur.Value as GameInit.ElementSprite).element;
        saveElement();
	}
	
	// Update is called once per frame
	void Update () {
        if(selected){
            if(counter == 0){
    		    float x = XCI.GetAxis (XboxAxis.LeftStickX, playerNum);
                if(x < 0){
                    update(false);
                }
                else if(x > 0)
                    update(true);
            }
//            else if(counter == 5)
//            
            else
                counter--;
        }
	}

    public void update(bool inc) {
        GetComponent<AudioSource>().Play();
        if (inc) {
            cur = cur.Next ?? cur.List.First;//if next is null, then use first
        } else if (!inc) {
			cur = cur.Previous ?? cur.List.Last;//if prev is null, then use last
        }
        GameInit.ElementSprite es = cur.Value as GameInit.ElementSprite;
        selectedElement = es.element;
		selectedElementText.sprite = es.elementText;
        background.sprite = es.box;
        saveElement();
        
        counter = time;
	}
    
    public void saveElement() {
        GameInit.setPlayerElement (playerNum, elementNum, selectedElement);
    }

}
