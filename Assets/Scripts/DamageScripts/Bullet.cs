using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	public int damage;
	public float bulletSpeed = 10;
	public float lifetime = 10;
	public GameObject explosion; // explosion
	public AudioClip fxSound;	// sound on explosion
	private bool lookRight = false;

	private GameObject player;
	private PlayerController playerController;

	// animation
	public int columnSize		= 5;
	public int rowSize			= 1;
	public int colFrameStart	=  0;
	// public int rowFrameStart	=  0; wurde ersetzt durch den Übergabewert animType vom enum
	public int totalFrames		= 5;
	public int framesPerSecond	= 12;

	void Start()
	{		
		player = GameObject.FindWithTag("Player");
		playerController = player.GetComponent<PlayerController>();
		lookRight = playerController.lookRight;
	
		//nicht mit dem Spieler kollidieren
		Physics.IgnoreCollision(collider, player.collider);
		Physics.IgnoreCollision(player.collider, collider);
	}

	void  Update()
	{
		Destroy(gameObject, lifetime);
		Move();
		Animate();
	}

	void Move()
	{
		if (lookRight)
		{
			transform.Translate(Vector3.right * bulletSpeed * Time.deltaTime);
		} else
		{
			transform.Translate(Vector3.left * bulletSpeed * Time.deltaTime);
		}
	}

	void Animate()
	{
		if (lookRight)
		{
			anim((int)AnimationTypes.shootRight);	
		}
		else
		{
			anim((int)AnimationTypes.shootLeft);
		}
		
	}

	enum AnimationTypes
	{
		shootRight = 0,
		shootLeft  = 1
	}

	void anim (int animType)
	{
		SpriteController spritePlay;
		spritePlay = GetComponent<SpriteController>();
		// enum an spritePlay.animate übergeben an gegebener Stelle
		spritePlay.animate(columnSize, rowSize, colFrameStart, animType, totalFrames, framesPerSecond);
	}
	
	void OnTriggerEnter(Collider hit){
		OnTrigger(hit);
	}
	
	void OnTriggerStay(Collider hit){
		OnTrigger(hit);
	}
	
	void OnTriggerExit(Collider hit){
		OnTrigger(hit);
	}
	
	void OnTrigger(Collider hit){
		// beim Treffen von Gegner wird die Funktion ApplyDamage aufgerufen
		hit.gameObject.SendMessage("ApplyDamage", (hit.collider.bounds.center - this.collider.bounds.center).normalized * damage , SendMessageOptions.DontRequireReceiver);
		
		//Bullet zerstören
		Destroy(gameObject);
		
		//kurz eine kleine Explosion anzeigen
		if(explosion){
			Object e = Instantiate(explosion, transform.position, transform.rotation);
			Destroy(e, 0.2f); //nach 0,2 Sekunden Explosion weg
			// TODO Sound abspielen beseitigen
			//audio.PlayClipAtPoint(fxSound, transform.position);
		}
	}	
	

}