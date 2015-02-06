using UnityEngine;
using System.Collections;

public abstract class Spell : MonoBehaviour {
	bool hasDot;//does the spell have a DoT effect on hit.
	int dotVal; //damage per second on DoT
	int dotT;   //duration of DoT
	int dmg;    //base damage of spell
	int spd;    //base speed of spell
	int knock;  //base knockback of spell
	int manaC;  //mana cost to use spell
	int range;  //range of the spell
	//defines how a given spell will fire
	public abstract void cast(PlayerChar p);
	
	//defines what happens when an object collides with a given spell
	public abstract void onCollide(Object collider);

	//defines spell changes when an element is put on a spell
	public abstract void infuse(Element e);
	
	//Accessors and modifiers
	public void setDot(bool has, int val, int t){hasDot=has;dotVal=val;dotT=t;}
	
	public bool getDotB(){return hasDot;}
	
	public int getDotV(){return dotVal;}
	
	public int getDotT(){return dotT;}
	
	public int getDmg(){return dmg;}
	
	public void setDmg(int d){dmg=d;}
	
	public void modDmg(int d){dmg+=d;}
	
	public int getSpd(){return spd;}
	
	public void setSpd(int s){spd=s;}
	
	public void modSpd(int s){spd+=s;}
	
	public int getKnock(){return knock;}
	
	public void setKnock(int k){knock=k;}
	
	public int getMana(){return manaC;}
	
	public void setMana(int m){manaC=m;}
	
	public void modMana(int m){manaC+=m;}
	
	public int getRange(){return range;}
	
	public void setRange(int r){range=r;}
	
	public void modRange(int r){range+=r;}
	//End of Accessors and modifiers
}
