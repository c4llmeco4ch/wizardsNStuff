using UnityEngine;
using System.Collections;

public class Wall : Spell {
	public PlayerChar p; //the player who is casting this slash
	public bool casting; //whether the slash is currently in effect
	public int framesLeft; //how many frames till the spell ends
	public bool facingRight; //true if sprite is facing right
	public bool charging; //true if player should be charging spell
	public int chargeLeft; //how many frames till the charge ends
	
	// Use this for initialization
	void start () {
		Debug.Log("...cuz I think he's walling.");
		casting=true;
		p.casting=false;
		p.charging=false;
		p.elementLoaded.SetActive(false);
		p.anim.SetBool("isCharging",false);
		p.anim.SetBool("Wall",true);
		framesLeft=((int)getDur())*35;
		//this.transform.position=p.transform.position;
		this.transform.position=cast();
		this.GetComponent<MeshRenderer>().enabled=true;
		this.GetComponent<BoxCollider>().enabled=true;
		this.GetComponentInChildren<SpriteRenderer>().enabled=true;
		string s=getElement().getName()+"_wall1_shadow";
		Debug.Log(s);
		this.GetComponentInChildren<SpriteRenderer>().sprite=Resources.Load("UI Art Assets/"+s,typeof(Sprite)) as Sprite;
		p.spellsCast++;
	}
	
	//call this immediately after creating this object
	public void prepWall(PlayerChar p){this.p=p; facingRight = true;}
	
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
		else if(casting){	
			if(framesLeft==0){
				kill();
			}
			else if(framesLeft>0)
				framesLeft--;	
		}
	}
	
	public void charge(){
		Debug.Log("Check for hax...");
		charging=true;
		chargeLeft=(int)(getCast()*6);
		this.GetComponent<MeshRenderer>().enabled=false;
		this.GetComponent<BoxCollider>().enabled=false;
		p.charging=true;
		p.anim.SetBool("isCharging",true);
		p.elementLoaded.SetActive(true);
		p.elementLoaded.GetComponent<Animator>().SetBool(getElement().getName(), true);
	}
	
	public void Awake(){
		casting=false;
		resetSpell();
	}
	
	//defines how a given spell will fire
	public Vector3 cast(){
		float y=p.transform.position.y;
		float x=p.transform.position.x;
		float z=p.transform.position.z;
		y = 2f;
		this.transform.Rotate(new Vector3(45f,0f,0f));
		if(z>-3)
			z=-3f;
		/*else
			z += 2f;*/
		if(p.facingRight){
			Debug.Log("X: "+x);
			x+=p.collider.bounds.size.x/2+(float)2;
			Debug.Log("X is now: "+x);
			return new Vector3((float)(x+(getRange()/3)),y,z);
		}
		else{
			//Debug.Log("X: "+x+"||Y: "+y+"||Z: "+x);
			x-=(float)5-(p.collider.bounds.size.x/2);
			return new Vector3((float)(x-(getRange()/2)),y,z);
		}
	}
	
	//defines what happens when an object collides with a given spell
	public void OnCollisionEnter(Collision c){
		Debug.Log ("Anything!!??!!"+c.gameObject.name);
		if(c.gameObject.tag.Equals("Player")){
			Debug.Log("Collision!!");
			PlayerChar pc=c.gameObject.GetComponent("PlayerChar") as PlayerChar;//issues grabbing the playerchar from the gameobject
			if(pc.transform.position.z==this.transform.position.z+this.transform.localScale.z)
				pc.transform.position=pc.transform.position+new Vector3(0,0,(float)1);
			else if(pc.transform.position.z==this.transform.position.z-this.transform.localScale.z)
				pc.transform.position=pc.transform.position-new Vector3(0,0,(float)1);
			if(pc.facingRight)
				pc.transform.position=pc.transform.position-new Vector3((float).3,0,0);
			else
				pc.transform.position=pc.transform.position+new Vector3((float).3,0,0);
		}
		else if(c.gameObject.tag=="Spell"){ //same as above
			Debug.Log ("Hi");
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
		setName("Wall");
		element=null;
		setDot(false,0,0);
		setDmg(0);
		setKnock(1);
		setCast(4);
		setMana(10);
		setRange(2);
		setSpd((float)4.5);
		setDur(8);
	}
	
	/*public void Flip (){
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}*/
}
