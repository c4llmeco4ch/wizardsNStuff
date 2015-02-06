using UnityEngine;
using System.Collections;

public abstract class Spell : MonoBehaviour {
	bool hasDot;//does the spell have a DoT effect on hit.
	int dotVal; //damage per second on DoT
	int dotT;   //duration of DoT
	int dmg;
	int spd;
	//defines how a given spell will fire
	public abstract void cast();
	
	//defines what happens when an object collides with a given spell
	public abstract void onCollide(Object collider);

	//defines spell changes when an element is put on a spell
	public abstract void infuse(Element e);
	
	//Accessors and modifiers
	public void setDot(bool has, int val, int t){hasDot=has;dotVal=val;dotT=t;}
	
	public bool getDotB(){return hasDot;}
	
	public int getDotV(){return dotVal;}
	
	public int getDotT(){return dotT;}
	//End of Accessors and modifiers
}
