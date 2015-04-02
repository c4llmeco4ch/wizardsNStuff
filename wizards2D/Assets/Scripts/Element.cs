using UnityEngine;
using System.Collections;

public class Element{//: MonoBehaviour {
	int dmg;     //damage mod for element
	int speed;   //projectile mod for element
	int cast;    //cast point mod for element
	int knock;   //knockback mod for element
	bool hasDot; //true if element has on-hit
	string dotN; //name of on-hit
	int dotT;    //duration of on-hit
	int dotV;    //value per second of on-hit
	int range;   //range mod for element
	int manaC;   //mana cost mod for element
	int duration;//duration of the spell
	string eName; //name of element
    Color auraColor; //color of aura
	
	//accessors and modifiers
	//*
	public void setDot(bool has, int val, int t){hasDot=has;dotV=val;dotT=t;}
	
	public bool getDotB(){return hasDot;}
	
	public void setDotB(bool b){hasDot=b;}
	
	public int getDotV(){return dotV;}
	
	public void setDotV(int v){dotV=v;}
	
	public int getDotT(){return dotT;}
	
	public void setDotT(int t){dotT=t;}
	
	public string getDotN(){return dotN;}
	
	public void setDotN(string s){dotN=s;}
	
	public int getDmg(){return dmg;}
	
	public void setDmg(int d){dmg=d;}

	public int getSpd(){return speed;}
	
	public void setSpd(int s){speed=s;}
			
	public int getCast(){return cast;}
	
	public void setCast(int s){cast=s;}
	
	public int getKnock(){return knock;}
	
	public void setKnock(int k){knock=k;}
	
	public int getMana(){return manaC;}
	
	public void setMana(int m){manaC=m;}
	
	public int getRange(){return range;}
	
	public void setRange(int r){range=r;}
	
	public int getDuration(){return duration;}
	
	public void setDuration(int d){duration=d;}
	
	public string getName(){return eName;}
	
	public void setName(string s){eName=s;}
    
    public Color getColor(){return auraColor;}
    
    public void setColor(Color c){auraColor = c;}
	//*
	//end of accessors and modifiers
	
}
