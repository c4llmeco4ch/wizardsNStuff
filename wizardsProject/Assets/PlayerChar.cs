using UnityEngine;
using System.Collections;

public class PlayerChar : MonoBehaviour {
	int hp; //the player's current hp
	int mana; //the player's current mana
	int es; //"0" if no element is stored, 1=fire, 2=water, 3=earth, 4=wind
	int maxHp; //player's max hp
	int maxMana; //player's max mana
	int xPos; //player's current x position on the map
	int yPos; //player's current y pos on the map
	bool jump; //true if player is currently jumping?
	int dotVal; //damage taken per second
	bool isDead; //true if player is dead, else false
	int stunT; //"0" when player is not stunned, else duration of stun remaining

	//instantiate new instance of player char. @param isP1 determines start location
	public PlayerChar(bool isP1) {
		maxHp = 100;
		maxMana = 100;
		es = 0;
		if (isP1) {
			xPos = 5;
			yPos = 5;
		} 
		else {
			xPos = 15;
			yPos = 5;
		}
		jump=false;
		dotVal=0;
		hp=maxHp;
		mana=maxMana;
		isDead=false;
		stunT=0;
	}



	// Update is called once per frame
	public void Update () {
	
	}

	public void takeDamage(int dmg){
		if (dmg>=hp)
			isDead=true;
		else
			hp-=dmg;
	}

}
