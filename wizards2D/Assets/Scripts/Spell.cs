using UnityEngine;
using System.Collections;

public abstract class Spell : MonoBehaviour {
	bool hasDot;//does the spell have a DoT effect on hit.
	string dotName; //name of on-hit effect
	int dotV; //damage per second on DoT
	int dotT;   //duration of DoT
	int dmg;    //base damage of spell
	int spd;    //base speed of spell
	int knock;  //base knockback of spell
	int manaC;  //mana cost to use spell
	int range;  //range of the spell
	Element element; //element the spell has been infused with
	//defines how a given spell will fire
	public abstract void cast(PlayerChar p);
	
	//defines what happens when an object collides with a given spell
	public abstract void onCollide(Object collider);

	//defines spell changes when an element is put on a spell
	public void infuse(Element e){
		element=e;
		hasDot=e.getDotB();
		dotV=e.getDotV();
		dotT=e.getDotT();
		dmg+=e.getDmg();
		knock+=e.getKnock();
		manaC+=e.getMana();
		range+=e.getRange();
	}
	
	//Accessors and modifiers
	public void setDot(bool has, int val, int t){hasDot=has;dotV=val;dotT=t;}
	
	public bool getDotB(){return hasDot;}
	
	public void setDotB(bool b){hasDot=b;}
	
	public string getDotN(){return dotName;}
	
	public void setDotN(string s){dotName=s;}
	
	public int getDotV(){return dotV;}
	
	public void setDotV(int v){dotV=v;}
	
	public int getDotT(){return dotT;}
	
	public void setDotT(int t){dotT=t;}
	
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
	
	public void setElement(Element e){element=e;}
	
	public Element getElement(){return element;}
	//End of Accessors and modifiers
}
