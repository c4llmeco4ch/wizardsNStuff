using UnityEngine;
using System.Collections;

[RequireComponent(typeof (PlayerChar))]
public class Slash : Spell {
	PlayerChar p; //the player who is casting this slash
	bool casting; //whether the slash is currently in effect
	public void FixedUpdate(){
		
	}
	
	public void Start(){//call this method when you wish to cast the slash
		cast(p);
		
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
		BoxCollider2D sword = new BoxCollider2D();
		sword.size=new Vector2((float)this.getRange(),(float)this.getRange());
		if(p.facingRight){
			x=p.collider2D.bounds.size.x/2+x;
			sword.center=new Vector2((float)x+(this.getRange()/2),(float)y);
		}
		else{
			x=p.collider2D.bounds.size.x/2-x;
			sword.center=new Vector2((float)x-(this.getRange()/2),(float)y);
		}
	}
	
	//defines what happens when an object collides with a given spell
	override public void onCollide(Object collider){
		
	}
	
}
