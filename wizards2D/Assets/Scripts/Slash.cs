using UnityEngine;
using System.Collections;


public class Slash : Spell {
	PlayerChar p; //the player who is casting this slash
	public bool casting; //whether the slash is currently in effect
	int framesLeft; //how many frames till the spell ends
	BoxCollider sword; //the collider for this spell
	
	//called once per engine step
	public void FixedUpdate(){
		if(!casting)
			return;
		else if(framesLeft==0){
			kill();
		}
		else if(framesLeft>0)
			framesLeft--;	
	}
	
	//call this method when you wish to cast the slash
	public void Start(){
		cast();
		casting=true;
		framesLeft=64;
	}
	
	//call this immediately after creating this object
	public void prepSlash(PlayerChar p){this.p=p;}
	
	//what happens when the spell ends
	override public void kill(){
		casting=false;
		Destroy(sword);
		framesLeft=0;
	}
	
	//initialize a given spell
	public void Awake(){
		casting=false;
	}
	
	//defines how a given spell will fire
	override public void cast(){
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
	
	//what happens when this collides with a different spell
	override public void versus(Spell s){
		if(s is Slash){
			if(getElement().getName()=="air"){
				if(s.getElement().getName()=="air"){
					//knockback, then...
					this.kill();
					s.kill();
				}
				if(s.getElement().getName()=="water"){
					//apply burn, then...
					this.kill();
					s.kill();
				}
				if(s.getElement().getName()=="fire"){
					//blow away, then...
					this.kill();
					s.kill();
				}
				else{
					this.kill();
					s.kill();
				}
			}
			else if(getElement().getName()=="fire"){
				if(s.getElement().getName()=="air"){
					//applies burn, then...
					this.kill();
					s.kill();
				}
				if(s.getElement().getName()=="water"){
					//fizzle, then...
					this.kill();
					s.kill();
				}
				if(s.getElement().getName()=="fire"){
					this.kill();
					s.kill();
				}
				else{
					//earth hits, then...
					this.kill();
					s.kill();
				}
			}
			else if(getElement().getName()=="water"){
				if(s.getElement().getName()=="air"){
					//apply wet, then...
					this.kill();
					s.kill();
				}
				if(s.getElement().getName()=="water"){
					this.kill();
					s.kill();
				}
				if(s.getElement().getName()=="fire"){
					//fizzle, then...
					this.kill();
					s.kill();
				}
				else{
					//earth is wet, then...
					this.kill();
					s.kill();
				}
			}
			else{
				if(s.getElement().getName()=="air"){
					//earth hits
					this.kill();
					s.kill();
				}
				if(s.getElement().getName()=="water"){
					//earth wet, then...
					this.kill();
					s.kill();
				}
				if(s.getElement().getName()=="fire"){
					//earth hits, then...
					this.kill();
					s.kill();
				}
				else{
					this.kill();
					s.kill();
				}
			}
		}
	}
	
	//defines what happens when an object collides with a given spell
	override public void onCollisionEnter(Collision c){
		if(c.gameObject.name.Contains("Player")){
			PlayerChar p=c.gameObject.GetComponent("PlayerChar") as PlayerChar;//issues grabbing the playerchar from the gameobject
			p.takeDamage(this.getDmg(),this);
		}
		else if(c.gameObject.name.Contains("Spell")){ //same as above
			Spell s=c.gameObject.GetComponent("Spell") as Spell;//issues grabbing the playerchar from the gameobject
			s.versus(this);
		}
			
	}
	
}
