using UnityEngine;
using System.Collections;

public abstract class Spell : MonoBehaviour {
	string name;//spell's type
	bool hasDot;//does the spell have a DoT effect on hit.
	string dotName;//name of on-hit effect
	int dotV; //damage per second on DoT
	int dotT; //duration of DoT
	int dmg;  //base damage of spell
	int cast; //cast time modifier
	float spd;//base speed of spell
	int knock;//base knockback of spell
	int manaC;//mana cost to use spell
	int range;//range of the spell
	public Element element; //element the spell has been infused with
    public AudioSource sound;
    public Animator anim;
	
	//defines what happens when an object collides with a given spell
	public void OnCollisionEnter(Collision c){}
	
	//define what happens when a spell is finished
	public abstract void kill();
	
	//defines what happens when a spell collides with a different spell
	public abstract void versus(Spell s);

	//defines spell changes when an element is put on a spell
	public void infuse(Element e){
		Debug.Log("Infusing spell with "+e.getName());
		element=e;
		hasDot=e.getDotB();
		dotV=e.getDotV();
		dotT=e.getDotT();
		modDmg(e.getDmg());
		modCast(e.getCast());
		modSpd(e.getSpd());
		setKnock(e.getKnock());
		modMana(e.getMana());
		modRange(e.getRange());
		
		picSwap(e);
        setAudio();
        anim.SetBool(element.getName(), true);
	}
	
	private void picSwap(Element e){
		MeshRenderer mr=this.gameObject.GetComponent<MeshRenderer>() as MeshRenderer;
		Material ma=Resources.Load("Materials/"+e.getName()+" "+name, typeof(Material)) as Material;
		mr.material=ma;
	}
	
	private void setAudio(){
		AudioClip ac=Resources.Load("Audio/"+element.getName()+"_-_"+name+"_2", typeof(AudioClip)) as AudioClip;
		sound.clip=ac;
	}
	
	public abstract void resetSpell();
	
	//Accessors and modifiers
	public void setName(string s){name=s;}
	
	public string getName(){return name;}
	
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
	
	public void modDmg(int d){dmg+=(d*2);}
	
	public int getCast(){return cast;}
	
	public void setCast(int c){cast=c;}
	
	public void modCast(int c){cast-=c;}
	
	public float getSpd(){return spd;}
	
	public void setSpd(float s){spd=s;}
	
	public void modSpd(float s){spd+=(float)s;}
	
	public int getKnock(){return knock;}
	
	public void setKnock(int k){knock=k;}
	
	public int getMana(){return manaC;}
	
	public void setMana(int m){manaC=m;}
	
	public void modMana(int m){manaC-=(m*2);}
	
	public int getRange(){return range;}
	
	public void setRange(int r){range=r;}
	
	public void modRange(int r){range+=r;}
	
	public void setElement(Element e){element=e;}
	
	public Element getElement(){return element;}
	//End of Accessors and modifiers
}
