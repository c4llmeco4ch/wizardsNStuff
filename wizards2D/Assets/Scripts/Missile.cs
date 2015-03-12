using UnityEngine;
using System.Collections;

public class Missile : Spell {
	public PlayerChar p; //the player who is casting this slash
	public bool casting; //whether the slash is currently in effect
	public bool facingRight; //true if sprite is facing right
	public bool charging; //true if player should be charging spell
	public int chargeLeft; //how many frames till the charge ends
	
	// Use this for initialization
	public void start () {
		casting=true;
        p.casting=false;
        p.elementLoaded.SetActive(false);
		p.anim.SetBool("isCharging",false);
		p.anim.SetBool("Missile",true);
		this.transform.position=p.transform.position;
		if(facingRight)
			this.transform.position+=new Vector3((float).4,(float)0,(float)0);
		else 
			this.transform.position-=new Vector3((float).4,(float)0,(float)0);
		cast();
		this.GetComponent<MeshRenderer>().enabled=true;
		this.GetComponent<BoxCollider>().enabled=true;
		p.spellsCast++;
	}
	
	//call this immediately after creating this object
	public void prepMissile(PlayerChar p){this.p=p; facingRight = true;}
	
	// Update is called once per frame
	public void FixedUpdate () {
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
		if(casting){
			cast();
		}
	}
	
	public void charge(){//muh lazer
		Debug.Log("Chargin muh lazer");
		charging=true;
		chargeLeft=(int)(getCast()*7);
		this.GetComponent<MeshRenderer>().enabled=false;
		this.GetComponent<BoxCollider>().enabled=false;
        p.anim.SetBool("isCharging",true);
        p.elementLoaded.SetActive(true);
        p.elementLoaded.GetComponent<Animator>().SetBool(getElement().getName(), true);
	}
	
	public void Awake(){
		casting=false;
		resetSpell();
	}
	
	//defines how a given spell will fire
	public void cast(){
		float y=this.transform.position.y;
		float x=this.transform.position.x;
		float z=this.transform.position.z;
		if(facingRight){
			this.transform.position=new Vector3((float)x+(getSpd()/4),y,z);
		}
		else{
			//			Debug.Log("X: "+x+"||Y: "+y+"||Z: "+x);
			this.transform.position=new Vector3((float)x-(getSpd()/4),y,z);
		}
	}
	
	//defines what happens when an object collides with a given spell
	public void OnCollisionEnter(Collision c){
		Debug.Log ("Anything!!??!!"+c.gameObject.name);
		if(c.gameObject.tag.Equals("Player")){
			Debug.Log("Collision!!");
			PlayerChar pc=c.gameObject.GetComponent("PlayerChar") as PlayerChar;//issues grabbing the playerchar from the gameobject
			pc.takeDamage(this.getDmg(),this);
			kill();
		}
		else if(c.gameObject.tag=="Spell"){ //same as above
			Debug.Log("Hi");
			Spell s=c.gameObject.GetComponent("Spell") as Spell;//issues grabbing the playerchar from the gameobject
			s.versus(this);
		}
		else if(c.gameObject.tag=="SideWall"){
			kill();
		}
		
	}
	
	//define what happens when a spell is finished
	override public void kill(){
		Destroy(this.gameObject);
	}
	
	//defines what happens when a spell collides with a different spell
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
		else if(s is Wall){
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
		else if(s is Missile){
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
	
	//call every time after spell fades to reset base values
	override public void resetSpell(){
		setName("Missile");
		element=null;
		setDot(false,0,0);
		setDmg(7);
		setKnock(1);
		setCast(5);
		setMana(15);
		setRange(0);
		setSpd((float)2.5);
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