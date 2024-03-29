﻿using UnityEngine;
using System.Collections;


public class Slash : Spell {
	public PlayerChar p; //the player who is casting this slash
	GameObject g;
	public bool casting; //whether the slash is currently in effect
	public int framesLeft; //how many frames till the spell ends
	public bool facingRight; //true if sprite is facing right
	public bool charging; //true if player should be charging spell
	public int chargeLeft; //how many frames till the charge ends
	
	//called once per engine step
	public void FixedUpdate(){
		if(!casting && !charging)
			return;
		else if(charging){
			if(chargeLeft==0){
				charging=false;
				start();
			}
			else if(chargeLeft>0)
				chargeLeft--;
		}
		else if(casting){	
			if(framesLeft==0){
				kill();
			}
			else if(framesLeft>0)
				framesLeft--;	
		}
		//Debug.Log(p.facingRight);
	}
	
	//call every time after spell fades to reset base values
	override public void resetSpell(){
		setName("Slash");
		element=null;
		setDot(false,0,0);
		setDmg(15);
		setCast(3);
		setKnock(0);
		setMana(20);
		setRange(0);
		setSpd(0);
	}
	
	//call this method when you wish to cast the slash
	public void start(){
		g.transform.position=cast();
		casting=true;
		p.charging=false;
		p.casting=true;
		p.anim.SetBool("isCharging",false);
        p.elementLoaded.SetActive(false);
		p.anim.SetBool("Slash",true);
		this.GetComponent<MeshRenderer>().enabled=true;
		this.GetComponent<BoxCollider>().enabled=true;
		framesLeft=(int)((getSpd()+3)*10);
		p.spellsCast++;
		sound.Play();	
	}
	
	public void charge(){//muh lazer
		Debug.Log("Chargin muh lazer");
		charging=true;
		chargeLeft=(int)(getCast()*7);
		this.GetComponent<MeshRenderer>().enabled=false;
		this.GetComponent<BoxCollider>().enabled=false;
		p.charging=true;
		p.anim.SetBool("isCharging",true);
        p.elementLoaded.SetActive(true);
        p.elementLoaded.GetComponent<Animator>().SetBool(getElement().getName(), true);
	}
	
	//call this immediately after creating this object
	public void prepSlash(PlayerChar p,GameObject g){this.p=p; this.g=g; facingRight = true;}
	
	//what happens when the spell ends
	override public void kill(){
		casting=false;
		framesLeft=0;
		this.GetComponent<MeshRenderer>().enabled=false;
		this.GetComponent<BoxCollider>().enabled=false;
        p.casting=false;
        anim.SetBool(element.getName(), false);
		resetSpell();
	}
	
	//initialize a given spell
	public void Awake(){
		casting=false;
		resetSpell();
	}
	
	//defines how a given spell will fire
	public Vector3 cast(){
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
		if(s is Punch){return;}
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
			PlayerChar pc=c.gameObject.GetComponent("PlayerChar") as PlayerChar;//issues grabbing the playerchar from the gameobject
			pc.takeDamage(this.getDmg(),this);
		}
		else if(c.gameObject.tag=="Spell"){ //same as above
			Spell s=c.gameObject.GetComponent("Spell") as Spell;//issues grabbing the playerchar from the gameobject
			s.versus(this);
		}
		else if(c.gameObject.tag=="SideWall"){
			kill ();
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
