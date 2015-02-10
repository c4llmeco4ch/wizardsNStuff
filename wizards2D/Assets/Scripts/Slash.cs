using UnityEngine;
using System.Collections;

[RequireComponent(typeof (PlayerChar))]
public class Slash : Spell {
	PlayerChar p; //the player who is casting this slash
	bool casting; //whether the slash is currently in effect
	int framesLeft;
	BoxCollider sword;
	public void FixedUpdate(){
		if(Input.GetButtonDown("Player"+p.playerNum+"_Spell_Slash_Mac") && !casting)
			Start ();
		else if(framesLeft==0){
			casting=false;
			Destroy(sword);
		}
		else if(framesLeft>0)
			framesLeft--;	
	}
	
	public void Start(){//call this method when you wish to cast the slash
		cast(p);
		casting=true;
		framesLeft=64;
	}
	
	public void prepSlash(PlayerChar p){this.p=p;}//call this immediately after creating this object
	
	
	
	//initialize a given spell
	public void Awake(){
		casting=false;
	}
	
	//defines how a given spell will fire
	override public void cast(PlayerChar p){
		float y=p.transform.position.y;
		float x=p.transform.position.x;
		float z=p.transform.position.z;
		sword = new BoxCollider();
		sword.size=new Vector3((float)this.getRange(),(float)this.getRange(),(float)this.getRange());
		if(p.facingRight){
			x=p.collider.bounds.size.x/2+x;
			sword.center=new Vector3((float)x+(this.getRange()/2),(float)y,(float)z);
		}
		else{
			x=p.collider.bounds.size.x/2-x;
			sword.center=new Vector3((float)x-(this.getRange()/2),(float)y,(float)z);
		}
		sword.transform.Rotate((float)30,(float)0,(float)0,Space.World);
	}
	
	//defines what happens when an object collides with a given spell
	override public void onCollisionEnter(Collision c){
		/*if(c.gameObject.name.Contains("Player")){
			PlayerChar p=c.gameObject.GetComponent(PlayerChar);//issues grabbing the playerchar from the gameobject
			p.takeDamage(this.getDmg(),this);
		}
		else if(c.gameObject.GetType()==Spell){ //same as above
			Spell s=c.gameObject;
		}*/
			
	}
	
}
