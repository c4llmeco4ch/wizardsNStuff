using UnityEngine;
using System.Collections;

public class NotEnoughPlayers : MonoBehaviour {
	int framesLeft;
	// Use this for initialization
	public void Start () {
		framesLeft = 60*5;
	}
	
	// Update is called once per frame
	void Update () {
		if(--framesLeft == 0)
			gameObject.SetActive(false);
	}
}
