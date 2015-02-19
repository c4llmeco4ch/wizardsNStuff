using UnityEngine;
using System.Collections;


public class Slash : Spell {
	public PlayerChar p; //the player who is casting this slash
	GameObject g;
	public bool casting; //whether the slash is currently in effect
	public int framesLeft; //how many frames till the spell ends
	public bool facingRight; //true if sprite is facing right
	
	//called once per engine step
	public void FixedUpdate(){
		if(!casting)
			return;
		else if(framesLeft==0){
			kill();
		}
		else if(framesLeft>0)
			framesLeft--;	
		//Debug.Log(p.facingRight);
	}
	
	//call every time after spell fades to reset base values
	override public void resetSpell(){
		element=null;
		setDot(false,0,0);
		setDmg(5);
		setKnock(0);
		setMana(5);
		setRange(0);
		setSpd(0);
	}
	
	//call this method when you wish to cast the slash
	public void Start(){
		g.transform.position=cast();
		casting=true;
		framesLeft=30;
	}
	
	//call this immediately after creating this object
	public void prepSlash(PlayerChar p,GameObject g){this.p=p; this.g=g; facingRight = true;}
	
	//what happens when the spell ends
	override public void kill(){
		casting=false;
		framesLeft=0;
		g.SetActive(false);
		p.casting=false;
		resetSpell();
	}
	
	//initialize a given spell
	public void Awake(){
		casting=false;
		setDmg(5);
		setMana(5);
	}
	
	//defines how a given spell will fire
	override public Vector3 cast(){
		float y=p.transform.position.y;
		float x=p.transform.position.x;
		float z=p.transform.position.z;
		if(p.facingRight){
//			Debug.Log("X: "+x+"||Y: "+y+"||Z: "+x);
			x=p.collider.bounds.size.x/2+x+(float).75;
			return new Vector3((float)x+(getRange()/4),y,z);
		}
		else{
//			Debug.Log("X: "+x+"||Y: "+y+"||Z: "+x);
			x=x-(float)1-(p.collider.bounds.size.x/2);
			return new Vector3((float)x-(getRange()/4),y,z);
		}
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
	public void OnCollisionEnter(Collision c){
		Debug.Log ("Anything!!??!!"+c.gameObject.name);
		if(c.gameObject.tag.Equals("Player")){
			Debug.Log("Collision!!");
			PlayerChar p=c.gameObject.GetComponent("PlayerChar") as PlayerChar;//issues grabbing the playerchar from the gameobject
			p.takeDamage(this.getDmg(),this);
		}
		else if(c.gameObject.name.Contains("Spell")){ //same as above
			Spell s=c.gameObject.GetComponent("Spell") as Spell;//issues grabbing the playerchar from the gameobject
			s.versus(this);
		}
			
	}

	public void Flip (){
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
}
