using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	// movement
	public float gravity		= 0.8f;
	public float runSpeed		= 10;
	public float climbSpeed		= 5;
	public float jumpPower		= 10;

	private Vector2 velocity	= Vector2.zero;
	private Vector2 moveDirection = Vector2.zero;
	private bool  InputJump		= false;
	private bool  isOnLadder	= false;
	private bool  lookRight		= true;

	// animation
	public int columnSize		= 10;
	public int rowSize			= 15;
	public int colFrameStart	=  0;
	// public int rowFrameStart	=  0; wurde ersetzt durch den Übergabewert animType vom enum
	public int totalFrames		= 10;
	public int framesPerSecond	= 12;
	
	private CharacterController characterController;

	// audio
	public AudioClip shootSound;
	public AudioClip jumpSound;
	public AudioClip hitSound;

	// health
	const int MAX_HEALTH = 100;
	public int currentHealth;

	void  Start ()
	{
		characterController = GetComponent<CharacterController>();
		currentHealth = MAX_HEALTH;
	}

	void  Update ()
	{
		InputCheck();
		Move();
		Animate();
		CheckForDeath();
	}

	void  InputCheck ()
	{
		velocity.x = Input.GetAxis("Horizontal") * runSpeed;
		velocity.y = Input.GetAxis("Vertical") * climbSpeed;

		if (velocity.x > 0)
		{
			lookRight = true;
		}

		if (velocity.x < 0)
		{
			lookRight = false;
		}

		if (Input.GetButtonDown("Jump"))
		{
			InputJump = true;
		}
		else
		{
			InputJump = false;
		}
	}

	void  Move ()
	{
		if (characterController.isGrounded)
		{
			if (InputJump)
			{
				moveDirection.y = jumpPower;
				audio.clip = jumpSound;
				audio.Play();
			}
		}

		if (isOnLadder)
		{
			moveDirection.y = velocity.y;
		}
		else
		{
			if (moveDirection.y > -20)
			{
				moveDirection.y -= gravity;
			}
		}

		moveDirection.x = velocity.x;

		characterController.Move(moveDirection * Time.deltaTime);	
	}

	void  OnTriggerEnter ( Collider characterController  )
	{
		if (characterController.gameObject.CompareTag ("Ladder"))
		{
		
			isOnLadder = true;
			// character is on the trigger
		}
	}

	void  OnTriggerExit ( Collider characterController  )
	{
		if (characterController.gameObject.CompareTag ("Ladder"))
		{
			isOnLadder = false;
			// character is out of the trigger
		}
	}

	void  Animate ()
	{
		if (velocity.x == 0)
		{
			if(lookRight)
			{
				anim((int)AnimationTypes.stayRight);
			}
			else
			{
				anim((int)AnimationTypes.stayLeft);
			}
		}
		else if (velocity.x < 0)
		{
			anim((int)AnimationTypes.moveLeft);
		}
		else
		{
			anim((int)AnimationTypes.moveRight);
		}

		if (!characterController.isGrounded)
		{
			if (lookRight)
			{
				anim((int)AnimationTypes.jumpRight);
			}
			else
			{
				anim((int)AnimationTypes.jumpLeft);
			}
		}
	}

	enum AnimationTypes
	{
		// Zahl ist für rowFrameStart, d.h. Auswahl der Reihe in welcher die Sprites liegen.
		stayRight	=  0,
		stayLeft	=  1,
		moveRight	=  2,
		moveLeft	=  3,
		jumpRight	=  4,
		jumpLeft	=  5,
		attackRight	=  6,
		attackLeft	=  7,
		anim1Right	=  8,
		anim1Left	=  9,
		anim2Right	= 10,
		anim2Left	= 11,
		other1		= 12,
		other2		= 13,
		other3		= 14
	}

	void  anim (int animType)
	{
		SpriteController spritePlay;
		spritePlay = GetComponent<SpriteController>();
		// enum an spritePlay.animate übergeben an gegebener Stelle
		spritePlay.animate(columnSize, rowSize, colFrameStart, animType, totalFrames, framesPerSecond);
	}
	
	
	// TODO param mit damage und dmg richtig kommentieren

	/// <summary>
	/// Schaden erhalten, der die HP verringert, und zum Tode führen kann
	/// </summary>
	/// <param name='damage'>
	/// Schaden der dem Spieler zugefügt wird
	/// </param>
	void ApplyDamage(Vector3 damage){
		int dmg = Mathf.RoundToInt(damage.magnitude);
		Debug.Log(name+"<"+tag+">("+GetInstanceID()+"): "+dmg+" dmg received");
		
		// HP verringern
		currentHealth -= dmg;
		//Geräusch
		
		float volume = 0.2f + 0.8f * ((float)dmg)/30.0f; //ab 30 dmg volle lautstärke, dadrunter abhängig vom schaden
		audio.PlayOneShot(hitSound, volume); 
		
	}
	
	
	
	/// <summary>
	/// Health erhalten. Heilt den Spieler.
	/// </summary>
	/// <param name='hp'>
	/// Wert um den der Spieler geheilt werden soll
	/// </param>
	void ApplyHealth(int hp){
		Debug.Log(name+"<"+tag+">("+GetInstanceID()+"): "+hp+" hp received");
		
		// HP erhöhen
		currentHealth += hp;
	}
	

	/// <summary>
	/// Überprüft diverse Parameter und ruft bei Tod die Funtkion zum Neuladen des Levels auf.
	/// </summary>
	/// <param name='dead'>
	/// Zustand ob tod oder nicht
	/// </param>
	void CheckForDeath()
	{
		if (currentHealth <= 0)
		{
			ReloadLevel();
		}
		if (transform.position.y <= -10)
		{
			ReloadLevel();
		}

	}
	
	/// <summary>
	/// Läd das aktuelle Level neu
	/// </summary>
	void ReloadLevel()
	{
		MessageDispatcher.Instance.EmptyQueue();
		Application.LoadLevel(Application.loadedLevel);
	}

	
}