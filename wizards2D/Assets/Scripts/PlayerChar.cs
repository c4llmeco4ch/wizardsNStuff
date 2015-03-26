using UnityEngine;
using System;
using UnityEngine.UI;
using XboxCtrlrInput;

public class PlayerChar : MonoBehaviour {
    public int hp; //the player's current hp
    public float mana; //the player's current mana
    public int es; //"0" if no element is stored, 1=fire, 2=water, 3=earth, 4=wind
    private int maxHp; //player's max hp
    private float maxMana; //player's max mana
    public float moveForce = 300f; // Amount of force added to move the player left and right.
    public float maxSpeed = 6f;	// The fastest the player can travel in the x axis.
    public bool jump; //true if player is currently jumping?
    public int dotVal; //damage taken per second
    public int dotT; //"0" when player is not burned or poisoned, else duration of DoT
    public bool isDead; //true if player is dead, else false
    public int stunT; //"0" when player is not stunned, else duration of stun remaining
    public bool facingRight; //true if sprite is facing right
    private bool block; //true if player is currenty blocking
    public bool casting; //true if player is in casting animation
    public int playerNum; //the player and controller number
    public Animator anim; //all the animations this char can have
    public GameObject[] spells; //list of spells player has access to; 0=slash, 1=missile, 2=wall
    public Element[] elements; //list of elements player has access to
    public GameObject elementLoaded; 
//    public Image healthBar;
//    public Image manaBar;
    public RectTransform healthBarTrans;
    public RectTransform manaBarTrans;
	const float healthSize = 630f / 100; //size of 1 health unit in terms of health bar
	const float manaSize = 607f / 100; //size of 1 mana unit in terms of mana bar
    public Image elementL; 
    public Image elementR;
    public bool charging; //whether the player is currently charging a spell
    public int chargeLeft; //how many frames till the charge ends
    private bool uiSet = false;
    public int spellsCast; //number of spells a character has casted in this game
    public int timeAlive; //Counts the number of frames during which a player is alive
    private Spell currentSpell; //Spell the user is currently charging
    public SpriteRenderer aura;
    public Image playerPic;
    public SpriteRenderer damage;
    public PlayerColor color;
    public int controllerNum;
    
    

    //instantiate new instance of player char. @param playerNum determines start location
    public PlayerChar() {
        casting = false;
        maxHp = 100;
        maxMana = 100;
        es = 0;
        jump = false;
        dotVal = 0;
        hp = maxHp;
        mana = maxMana;
        isDead = false;
        stunT = 0;
        playerNum = 1;
        spells = new GameObject[4];
        elements = new Element[2];
        spellsCast=0;
        timeAlive=0;
    }
    

    public void Awake() {
        if(playerNum > 2 && GameInit.playerNum <= 2 && playerNum > GameInit.playerNum) {
            this.gameObject.SetActive(false);
            return;
        }
        Debug.Log("Player Num: "+playerNum);
        GameInit.players [playerNum - 1] = this;
		facingRight = true;
		color = GameInit.getPlayerColor(playerNum);
		controllerNum = GameInit.getControllerNum(playerNum);
//		GameInit i = new GameInit ();
        elements [0] = GameInit.getPlayerElement(playerNum, 0); //new Earth();
        elements [1] = GameInit.getPlayerElement(playerNum, 1);//new Air();
        
        

//        healthBarTrans = healthBar.GetComponent<RectTransform>() as RectTransform;
//        manaBarTrans = manaBar.GetComponent<RectTransform>() as RectTransform;

//		elementL.s
        
		
        /*
		* instantiate missile for player
		*/
		
        /*
		* instantiate wall for player
		*/
    }

    // Update is called once per frame
    public void Update() { 
        //Loads player UI at beginning of game
        if(!uiSet && GameInit.arena != null) {
            uiSet = true;
            GameInit.arena.setUI(playerNum);
            elementL.sprite = Resources.Load("UI Art Assets/mana/" + elements [0].getName() + "_element", typeof(Sprite)) as Sprite;
            elementR.sprite = Resources.Load("UI Art Assets/mana/" + elements [1].getName() + "_element", typeof(Sprite)) as Sprite;
            Debug.Log("color: "+color.ToString());
			playerPic.sprite = Resources.Load("UI Art Assets/mana/"+color.ToString()+"_figure", typeof(Sprite)) as Sprite;
			anim.runtimeAnimatorController = Resources.Load("Animation Controllers/"+color.ToString()+" Player", typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
        }
        
        if (!isDead) {
			timeAlive++;
            //turns off animator bools so cast animations do not repeat forever. 
            if(anim.GetCurrentAnimationClipState(0)!= null) {
                if(anim.GetCurrentAnimationClipState(0)[0].clip.name.EndsWith("Cast Slash"))
                    anim.SetBool("Slash", false);
                if (anim.GetCurrentAnimationClipState(0)[0].clip.name.EndsWith("Cast Missile 1"))
                    anim.SetBool("Missile", false);
                if (anim.GetCurrentAnimationClipState(0)[0].clip.name.EndsWith("Cast Wall"))
                    anim.SetBool("Wall", false);
            }
            
			float lt = XCI.GetAxis(XboxAxis.LeftTrigger, controllerNum); //true if left trigger is pushed, else false
			float rt = XCI.GetAxis(XboxAxis.RightTrigger, controllerNum); //true if right trigger is pushed, else false
			regenMana();
			if (mana > 100)
				mana = 100;
			if(stunT>0){stunT--;}			
			else{
                if(anim.GetBool("isHit"))
                    anim.SetBool("isHit", false);
                if(anim.GetBool("isBlockAndHit"))
					anim.SetBool("isBlockAndHit", false);
                    
//                if(!casting)
	            //Debug.Log (lt+" || "+rt);
	            //Code for casting Slash
				if (XCI.GetButtonDown(XboxButton.X, controllerNum)) {
	//                elementLoaded.SetActive(false);
	                if ((rt>.5 && lt>.5) || !(rt > .5 || lt > .5) || casting) {
	                    //Actually, punch, but for now, nothing
	                    //Debug.Log ("I'm here");
	                } else {
	                    bool justMade = false;
	                    if (spells [0] == null) {
	                        slashMaker();
	                        //Slash s=spells[0].GetComponent("Slash") as Slash;
	                        justMade = true;
	                        //s.prepSlash(this,spells[0]);
	                    }
	                    //			Debug.Log(spells[0].transform.position+" HI");
	                    Slash slash = spells [0].GetComponent("Slash") as Slash;
	                    if (lt > .5)
	                        slash.infuse(elements [0]);
	                    else if (rt > .5)
	                        slash.infuse(elements [1]);	
	                    Debug.Log("Slash's Element is " + slash.getElement().getName());
	                    if (mana < slash.getMana()) {
	                        playNoMana();
	                        slash.kill();
	                    }
	                    else {
							slash.sound.Play();
							reduceMana(slash);
	                        currentSpell=slash;
	                        casting = true;
	                        if (slash.casting) {
	                            return;
	                        }
							slash.charge();
					
	                        if (slash.facingRight && !facingRight)
	                            slash.Flip();
	                        else if (!slash.facingRight && facingRight)
	                            slash.Flip();
	                    }
	                }
	            }
				//Code for casting Missile
				else if (XCI.GetButtonDown(XboxButton.B, controllerNum)) {
	                //Debug.Log("FIRE DA MISSILES");
					if ((rt>.5 && lt>.5) || !(rt > .5 || lt > .5) || casting) {
	                    //Actually, dodgeroll, but for now, nothing
	                    //Debug.Log ("I'm here");
	                } 
	                else {
	                    bool justMade = false;
	                    if (spells [1] == null) {
	                        missileMaker();
	                        //Slash s=spells[0].GetComponent("Slash") as Slash;
	                        //justMade = true;
	                        //s.prepSlash(this,spells[0]);
	                    }
	                    //			Debug.Log(spells[0].transform.position+" HI");
	                    Missile missile = Instantiate(spells [1].GetComponent("Missile")) as Missile;
	                    if (lt > .5)
	                        missile.infuse(elements [0]);
	                    else if (rt > .5)
	                        missile.infuse(elements [1]);
	                    Debug.Log("Missile's Element is " + missile.getElement().getName());
	                    if (mana < missile.getMana()){
	                        playNoMana();
	                        missile.kill();
	                    }
	                    else {
							missile.sound.Play();
	                        reduceMana(missile);
	                        currentSpell=missile;
	                        casting = true;
	                        if (missile.casting) {
	                            return;
	                        }
	                      
							missile.charge();
	
	                        if (missile.facingRight && !facingRight)
	                            missile.Flip();
	                        else if (!missile.facingRight && facingRight)
	                            missile.Flip();
	                    }
	                }
	            } 
	            //code for wall
				else if (XCI.GetButtonDown(XboxButton.Y, controllerNum)) {
					if(!(rt > .5 || lt > .5)){
						setBlock(true);
						anim.SetFloat("Speed", 0f);
						anim.SetBool("isBlocking",true); //?
					}
					else if ((rt>.5 && lt>.5) || casting) {
						//Actually, block, but for now, nothing
						//Debug.Log ("I'm here");
					} 
					else {
						bool justMade = false;
						if (spells [2] == null) {
							wallMaker();
							//Slash s=spells[0].GetComponent("Slash") as Slash;
							//justMade = true;
							//s.prepSlash(this,spells[0]);
						}
						//			Debug.Log(spells[0].transform.position+" HI");
						Wall wall = Instantiate(spells [2].GetComponent("Wall")) as Wall;
						if (lt > .5)
							wall.infuse(elements [0]);
						else if (rt > .5)
							wall.infuse(elements [1]);
						Debug.Log("Missile's Element is " + wall.getElement().getName());
						if (mana < wall.getMana()){
							playNoMana();
							wall.kill();
						}
						else {
							wall.sound.Play();
							reduceMana(wall);
							currentSpell=wall;
							casting = true;
							if (wall.casting) {
								return;
							}
							
							wall.charge();
							
							/*if (wall.facingRight && !facingRight)
								wall.Flip();
							else if (!wall.facingRight && facingRight)
								wall.Flip();*/
						}
					}
				}
				else if(XCI.GetButtonUp(XboxButton.Y, controllerNum) && block){
					block=false;
					anim.SetBool("isBlocking",false);
				}
				else {
				            if (!(rt > .5 && lt > .5) && !casting && !charging) {
														
				                if (lt > .5) {
                                    aura.gameObject.SetActive(true);
                                    aura.color = elements[0].getColor();
                                }
                                else if (rt > .5) {
				                    aura.gameObject.SetActive(true);
                                    aura.color = elements[1].getColor();
                                }
				                else
                                    aura.gameObject.SetActive(false);
				            } else
                                aura.gameObject.SetActive(false);
				}
			}
		}
        //update health and mana bars
		healthBarTrans.sizeDelta = new Vector2(hp * healthSize, healthBarTrans.sizeDelta.y);
		manaBarTrans.sizeDelta = new Vector2(mana * manaSize, manaBarTrans.sizeDelta.y);
    }
	
    public void slashMaker() {
        GameObject slash = Instantiate(Resources.Load("Prefabs/Slash", typeof(GameObject)), transform.position, Quaternion.identity) as GameObject;
        Slash s = slash.GetComponent("Slash") as Slash;
        s.prepSlash(this, slash);
        //slash.transform.Translate(s.cast(),Space.World);
        spells [0] = slash;
//		Debug.Log("-.-");
    }
	
    public void missileMaker() {
        GameObject missile = Instantiate(Resources.Load("Prefabs/Missile", typeof(GameObject)), transform.position, Quaternion.identity) as GameObject;
        Missile m = missile.GetComponent("Missile") as Missile;
        m.prepMissile(this);
		m.GetComponent<MeshRenderer>().enabled=false;
		m.GetComponent<BoxCollider>().enabled=false;
        //slash.transform.Translate(s.cast(),Space.World);
        spells [1] = missile;
        //		Debug.Log("-.-");
    }
    
	public void wallMaker() {
		GameObject wall = Instantiate(Resources.Load("Prefabs/Wall", typeof(GameObject)), transform.position, Quaternion.identity) as GameObject;
		Wall w = wall.GetComponent("Wall") as Wall;
		w.prepWall(this);
		w.GetComponent<MeshRenderer>().enabled=false;
		w.GetComponent<BoxCollider>().enabled=false;
		w.gameObject.GetComponentInChildren<SpriteRenderer>().enabled=false;
		//slash.transform.Translate(s.cast(),Space.World);
		spells [2] = wall;
		//		Debug.Log("-.-");
	}
	
	// FixedUpdate is called once per physics step 
    public void FixedUpdate() {
        if (!isDead && stunT==0 && !block) {
            // Cache the contoller input input.
            if (casting)
                return;
			float rawHorizontal = XCI.GetAxis(XboxAxis.LeftStickX, controllerNum);
			float rawVertical = XCI.GetAxis(XboxAxis.LeftStickY, controllerNum);
            rawHorizontal = (rawHorizontal * 0.35f);
            Vector3 direction = new Vector3(rawHorizontal, 0f, rawVertical);
            float speed = (direction).magnitude;


            //		Debug.Log ("Test speed: "+speed);
            if ((speed * rigidbody.velocity).magnitude < maxSpeed)
                rigidbody.AddForce(direction * moveForce);

            //		if (rigidbody.velocity.magnitude > maxSpeed)
            //						rigidbody.velocity = direction * maxSpeed;

            anim.SetFloat("Speed", speed * 5);

            if (rawHorizontal < 0 && facingRight)
                Flip();
            else if (rawHorizontal > 0 && !facingRight)
                Flip();
        }

    }

    //Flips the direction the player is facing
    public void Flip() {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;
		
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    //check if the amount of incoming damage is greater than the player's current hp. if so, player is die
    //else, subtract player's hp by damage
    public void takeDamage(int dmg, Spell s) {
        Debug.Log("Taking " + dmg + " Damage as " + playerNum);
        playHit();
        int bd;
        if (hp == 0) {
            return;
        }
        if (!block)
            bd = dmg;
        else
            bd = dmg / 2;
        if (bd >= hp) {
            isDead = true;
            hp = 0;
            kill();
        } else {
            hp -= bd;
            Debug.Log("Player " + playerNum + ": " + hp);
        }
        if (s.getDotB()) {
            dotVal = s.getDotV();
            dotT = s.getDotT();
        }
        if(s.getKnock()==0){
			stunned(30);
			if(this.charging){
				Destroy(currentSpell);
			}
        }
        else{
			stunned(70+(s.getKnock()*7));
			if(this.charging){
				Destroy(currentSpell);
			}
		}
        if(block) {
            anim.SetBool("isBlockAndHit", true);
            anim.SetBool("isBlocking", false);
        }
        else
            anim.SetBool("isHit", true);
            
		damage.sprite = Resources.Load("UI Art Assets/damage/"+s.element.getName()+"_"+s.getName()+"_damage", typeof(Sprite)) as Sprite;
		damage.gameObject.SetActive(true);
		Damage d = damage.GetComponent<Damage>();
		d.Start();
		if (d.facingRight && !facingRight)
			d.Flip();
		else if (!d.facingRight && facingRight)
			d.Flip();
    }
	
    public void setBlock(bool b) {
        block = b;
    }

    public bool getBlock() {
        return block;
    }
	
    public void reduceMana(Spell s) {
        mana -= s.getMana();
    }
	
    public void kill() {
        isDead = true;
        anim.SetBool("Dead", true);
        transform.Rotate(0, 0, 90);
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        this.GetComponent<BoxCollider>().enabled=false;
    }
	
    public void regenMana() {
        if (mana < maxMana)
            mana += (float).1;
    }
	
    public void stunned(int d) {
        if (d <= stunT)
            return;
        else {
            stunT = d;
        }
    }
    
    //plays the out of mana sound effect
    public void playNoMana() {
        playSound("OutOfMana");
    }
    
    //plays the hit sound effect
    public void playHit() {
        playSound("Hit");
    }
    
    //plays a sound effect using the player's audio source
    private void playSound(string soundName) {
        AudioClip c = Resources.Load("Audio/"+soundName, typeof(AudioClip)) as AudioClip;
        AudioSource s = GetComponent<AudioSource>();
        s.clip = c;
        s.Play();
    }
}
