using UnityEngine;
using System.Collections;

public class Slash : Spell {

	public void FixedUpdate(){
		
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
	
	//defines spell changes when an element is put on a spell
	override public void infuse(Element e){
		
	}
}
