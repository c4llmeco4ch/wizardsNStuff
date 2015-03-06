using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using XboxCtrlrInput;

public class Tutorial : MonoBehaviour {
    public GameObject[] players = new GameObject[4];
    public Sprite[] sprites = new Sprite[3];
    public Image panel; 
    public int cur;
	// Use this for initialization
	void Start () {
        if(!GameInit.tutorial)
            startGame();
        sprites[0] = Resources.Load("UI Art Assets/Tutorials/tutorial1", typeof(Sprite)) as Sprite;
        sprites[1] = Resources.Load("UI Art Assets/Tutorials/tutorial2", typeof(Sprite)) as Sprite;
        sprites[2] = Resources.Load("UI Art Assets/Tutorials/tutorial3", typeof(Sprite)) as Sprite;
        cur = 0;
        panel.sprite = sprites[0];
	}
	
	// Update is called once per frame
	void Update () {
        if(cur == 0 && XCI.GetButtonDown(XboxButton.B))
            Application.LoadLevel("ChooseElementScene");
        else if(cur < 2 && XCI.GetButtonDown(XboxButton.A)){
            panel.sprite = sprites[++cur];
        }
        else if(cur < 3 && XCI.GetButtonDown(XboxButton.B))
            panel.sprite = sprites[--cur];
        else if((cur == 2 && XCI.GetButtonDown(XboxButton.A)) || XCI.GetButtonDown(XboxButton.Start)){
            startGame();
        }
	}
    
    void startGame() {
        for(int i = 0; i < GameInit.playerNum; i++)
            players[i].SetActive(true);
        gameObject.SetActive(false);
    }
    
}
