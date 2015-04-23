using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {
	int framesLeft;
	public bool facingRight = true;
	// Use this for initialization
	public void Start () {
		framesLeft = 30;
	}
	
	// Update is called once per frame
	void Update () {
		if(--framesLeft == 0)
			gameObject.SetActive(false);
	}
	
	//Flips the direction the player is facing
	public void Flip() {
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		/*if(!facingRight)
			facingRight=!facingRight;*/
	}
}
