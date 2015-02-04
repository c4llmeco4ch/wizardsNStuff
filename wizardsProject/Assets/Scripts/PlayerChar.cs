using UnityEngine;
using System.Collections;

public class PlayerChar : MonoBehaviour {
	int hp; //the player's current hp
	int mana; //the player's current mana
	int es; //"0" if no element is stored, 1=fire, 2=water, 3=earth, 4=wind
	int maxHp; //player's max hp
	int maxMana; //player's max mana
	public float moveForce = 40f; // Amount of force added to move the player left and right.
	public float maxSpeed = 3f;	// The fastest the player can travel in the x axis.
	bool jump; //true if player is currently jumping?
	int dotVal; //damage taken per second
	bool isDead; //true if player is dead, else false
	int stunT; //"0" when player is not stunned, else duration of stun remaining
	bool facingRight; //true if sprite is facing right
	public int playerNum; //the player and controler number
	private Animator anim; 

	//instantiate new instance of player char. @param isP1 determines start location
	public PlayerChar() {
		maxHp = 100;
		maxMana = 100;
		es = 0;
//		if (isP1) {
//			xPos = 5;
//			yPos = 5;
//		} 
//		else {
//			xPos = 15;
//			yPos = 5;
//		}
		jump=false;
		dotVal=0;
		hp=maxHp;
		mana=maxMana;
		isDead=false;
		stunT=0;
		playerNum = 1;
	}



	// Update is called once per frame
	public void Update () {
	
	}

	// FixedUpdate is called once per physics step 
	public void FixedUpdate () {
		// Cache the contoller input input.
		float rh = Input.GetAxis ("Player"+playerNum+"_Move_Horizontal_Mac");
		float rv = Input.GetAxis ("Player" + playerNum + "_Move_Vertical_Mac");
		Vector3 h = new Vector3(rh, 0, 0);
		Vector3 v = new Vector3 (0, 0, rv);
		float speed = (h + v).magnitude;

		// The Speed animator parameter is set to the absolute value of the horizontal input.
		anim.SetFloat ("Speed", speed);

		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if (speed + rigidbody.velocity.magnitude < maxSpeed)
			rigidbody.AddForce( new Vector3
	}

	public void takeDamage(int dmg){
		if (dmg>=hp)
			isDead=true;
		else
			hp-=dmg;
	}

}
