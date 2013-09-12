using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	// debug vars
	public bool godMode = false;

	// movement
	public float gravity		= 0.8f;
	public float runSpeed		= 10;
	public float climbSpeed		= 5;
	public float jumpPower		= 10;

	private Vector2 velocity	= Vector2.zero;
	private Vector2 moveDirection = Vector2.zero;
	private bool  InputJump		= false;
	private bool  isOnLadder	= false;
	private bool movementAllowed = true;
	public bool  lookRight		= true;
	
	//steuerung von außen
	public bool CanShoot = true;
	public bool CanMove = true;
	
	// animation
	public int columnSize		= 10;
	public int rowSize			= 15;
	public int colFrameStart	=  0;
	// public int rowFrameStart	=  0; wurde ersetzt durch den Übergabewert animType vom enum
	public int totalFrames		= 10;
	public int framesPerSecond	= 12;
	public float damageEffectPause = 0.1f;
	
	private CharacterController characterController;

	// audio
	public AudioClip shootSound;
	public AudioClip jumpSound;
	public AudioClip hitSound;

	// health
	public const int MAX_HEALTH = 100;
	private int currentHealth = 100;

	// property health
	public int Health
	{
		get {return currentHealth;}
		private set {
			//Wert verändern
			currentHealth = value;
			//Health in bestimmten Wertebereich halten
			Utility.MinMax(ref currentHealth, 0, MAX_HEALTH);
		}
	}

	// shoot
	public GameObject bullet;
	public Transform bulletSpawn;
	private bool	  InputShoot = false;
	private bool shootingAllowed = true;
	public float shootingDelay = 0.5f;
	
	void  Start ()
	{
		characterController = GetComponent<CharacterController>();
		currentHealth = MAX_HEALTH;
	}

	void  Update ()
	{
		if(CanMove)
		{
			InputCheck();
			Move();
			Animate();
		}
		if(CanShoot) Shoot();
		CheckForDeath();
	}

	void  InputCheck ()
	{
		// horizontale Achse abgreifen
		velocity.x = Input.GetAxis("Horizontal") * runSpeed;
		// vertikale Achse abgreifen
		velocity.y = Input.GetAxis("Vertical") * climbSpeed;

		// "gucken, in welche Richtung er guckt"
		if (velocity.x > 0)
		{
			lookRight = true;
		}
		if (velocity.x < 0)
		{
			lookRight = false;
		}

		// "gucken, ob er springt"
		if (Input.GetButtonDown("Jump"))
		{
			InputJump = true;
		}
		else
		{
			InputJump = false;
		}

		// "gucken ob er schießt"
		if (Input.GetButtonDown("Fire1"))
		{
			InputShoot = true;
		}
		else
		{
			InputShoot = false;
		}
	}

	void  Move ()
	{
		// wenn er aufm Boden ist
		if (characterController.isGrounded)
		{
			// und gedrückt wurde, dass er springen soll
			if (InputJump)
			{
				// dann wandel die jumpPower in die moveDirection,
				moveDirection.y = jumpPower;
				// spiele den jumpSound ab
				audio.PlayOneShot(jumpSound, 1);
			}
		}

		// wenn er auf ner Leiter ist
		if (isOnLadder)
		{
			// wandel die vertikale Achse in moveDirection
			moveDirection.y = velocity.y;
		}
		else // ansonsten ziehe die gravity wieder ab
		{
			if (moveDirection.y > -20) // aber nur bis -20, da wir sonst irgendwann einen Speichermsprung haben
			{
				moveDirection.y -= gravity;
			}
		}

		// horizontale Bewegung ist immer die Achse
		moveDirection.x = velocity.x;

		// prüfen wegen dem DamageEffect, da darf der Spieler sich für kurze Zeit nicht bewegen.
		if (movementAllowed)
		{
			// Bewegen des Spielercharakters anhand der vorher festgestellten moveDirection
			characterController.Move(moveDirection * Time.deltaTime);
		}
	}

	// Prüfen ob der Spieler auf ner Leiter ist
	void  OnTriggerEnter ( Collider characterController  )
	{
		if (characterController.gameObject.CompareTag ("Ladder"))
		{
			isOnLadder = true;
			// character is on the trigger
		}
	}

	// Prüfen ob der Spieler auf ner Leiter ist
	void  OnTriggerExit ( Collider characterController  )
	{
		if (characterController.gameObject.CompareTag ("Ladder"))
		{
			isOnLadder = false;
			// character is out of the trigger
		}
	}

	void Shoot ()
	{
		// wenn gedrückt wurde und es nicht zu schnell hintereinander ist
		if (InputShoot && shootingAllowed)
		{
			// und n bullet prefab drin ist
			if (bullet)
			{
				//Spawnpunkt für Bullet, im Lokalem Koordinatensystem des Spielers
				Vector3 pos = bulletSpawn.localPosition * 2; // *2, weil scale vom player = 2
				
				//Spiegel entlang der X-Achse wenn Spieler nach links guckt
				if(!lookRight) pos.x = -pos.x;
				
				//zu globalen Koordinatensystem
				pos = transform.position + pos; //Spieler position
				
				//Bullet erstellen
				Instantiate(bullet, pos, bulletSpawn.rotation);	
				// Schießen aussetzen
				shootingAllowed = false;
				// und Coroutine starten um Schießen wieder zu erlauben
				StartCoroutine(DelayShooting(shootingDelay));
			}
			else
			{
				Debug.Log("No Bullet! Please assign in Inspector!");
			}
			
			// und Sound abspielen
			audio.PlayOneShot(shootSound, 1); 
		}
	}

	// abwarten und dann schießen wieder erlauben
	IEnumerator DelayShooting(float delay)
	{
		yield return new WaitForSeconds(delay);
		shootingAllowed = true;
	}

	void  Animate ()
	{
		// wenn der spieler steht
		if (velocity.x == 0)
		{
			// und nach rechts guckt
			if(lookRight)
			{
				// animier ihn stehend und nach rechts guckend
				anim((int)AnimationTypes.stayRight);
			}
			else
			{	
				// oder eben sonst nach links guckend
				anim((int)AnimationTypes.stayLeft);
			}
		}
		// wenn er sich nun aber nach links bewegt
		else if (velocity.x < 0)
		{
			// dann animier ihn nach links laufend
			anim((int)AnimationTypes.moveLeft);
		}
		else
		{
			// oder eben nach rechts laufend
			anim((int)AnimationTypes.moveRight);
		}

		// wenn er den Boden nicht berührt
		if (!characterController.isGrounded)
		{
			// und nach rechts guckt
			if (lookRight)
			{
				// animier ihn nach rechts springend
				anim((int)AnimationTypes.jumpRight);
			}
			else
			{
				// oder eben nach links springend
				anim((int)AnimationTypes.jumpLeft);
			}
		}
		
		// altes Code-Schnipsel als wir noch keine Grafiken hatten, hat die Dummy-Grafik gespiegelt um so links/rechts zu simulieren
		/*if(!lookRight){
			//Textur vertikal spiegeln
			Vector2 tmp = renderer.material.mainTextureScale;
			tmp = new Vector2(-tmp.x, tmp.y);
			renderer.material.mainTextureScale = tmp;
		}*/
	}

	// die ganzen Animationstypen als enum
	public enum AnimationTypes
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

	// aufrufen des spritecontrollers
	public void  anim (int animType)
	{
		SpriteController spritePlay;
		spritePlay = GetComponent<SpriteController>();
		// enum an spritePlay.animate übergeben an gegebener Stelle
		spritePlay.animate(columnSize, rowSize, colFrameStart, animType, totalFrames, framesPerSecond);
	}

	/// <summary>
	/// Schaden erhalten, der die HP verringert, und zum Tode führen kann
	/// </summary>
	/// <param name='damage'>
	/// Schaden der dem Spieler zugefügt wird
	/// </param>
	void ApplyDamage(Vector3 damage)
	{
		// wenn nicht gerade der godMode an ist
		if (!godMode)
		{
			// runde den damage
			int dmg = Mathf.RoundToInt(damage.magnitude);
			// mach ne Debug Ausgabe
			Debug.Log(name+"<"+tag+">("+GetInstanceID()+"): "+dmg+" dmg received");
		
			// HP verringern
			Health -= dmg;
				
			// Geräusch
			float volume = 0.2f + 0.8f * ((float)dmg)/30.0f; //ab 30 dmg volle lautstärke, dadrunter abhängig vom schaden
			audio.PlayOneShot(hitSound, volume); 

			// Blinken und kurz Bewegung aussetzen
			StartCoroutine(DamageEffect());


		}
	}
	
	// Blinken und kurz Bewegung aussetzen
	IEnumerator DamageEffect()
	{
		renderer.enabled = false;
		movementAllowed = false;
		yield return new WaitForSeconds(damageEffectPause);
		movementAllowed = true;
		renderer.enabled = true;
		yield return new WaitForSeconds(damageEffectPause);
		renderer.enabled = false;
		yield return new WaitForSeconds(damageEffectPause);
		renderer.enabled = true;
		yield return new WaitForSeconds(damageEffectPause);
		renderer.enabled = false;
		yield return new WaitForSeconds(damageEffectPause);
		renderer.enabled = true;
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
		Health += hp;
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
		MessageDispatcher.I.EmptyQueue();
		Application.LoadLevel(Application.loadedLevel);
	}

	
}