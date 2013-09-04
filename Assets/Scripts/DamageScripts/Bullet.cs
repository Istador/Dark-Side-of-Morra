using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	public int damage;
	public float bulletSpeed = 10;
	public float lifetime = 10;
	public Transform explosion; // explosion
	public AudioClip fxSound;	// sound on explosion
	private bool lookRight;

	public GameObject player;
	PlayerController playerController;

	// animation
	public int columnSize		= 10;
	public int rowSize			= 15;
	public int colFrameStart	=  0;
	// public int rowFrameStart	=  0; wurde ersetzt durch den Übergabewert animType vom enum
	public int totalFrames		= 10;
	public int framesPerSecond	= 12;

	void Start ()
	{
		playerController = player.GetComponent<PlayerController>();
		lookRight = playerController.lookRight;
	}

	void  Update ()
	{
		Destroy(gameObject, lifetime);
		Move();
		Animate();
	}

	void Move()
	{
		// TODO dementsprechend nach links oder rechts bewegen
		if (lookRight)
		{
			transform.Translate(Vector3.right * bulletSpeed * Time.deltaTime);
		}
	}

	void Animate()
	{
		anim((int)AnimationTypes.shootRight);
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

	void  OnTriggerEnter ( Collider hit  )
	{
		// beim Treffen auf den Spieler wird beim Player die Funktion ApplyDamage aufgerufen und die Bullet zerstört
		if (hit.gameObject.CompareTag("Enemy"))
		{
			GameObject target = hit.gameObject;
			// Apply damage to target object
			target.collider.SendMessage("ApplyDamage", (hit.collider.bounds.center - this.collider.bounds.center).normalized * damage , SendMessageOptions.DontRequireReceiver);
			Destroy(gameObject);
		}
		
		// beim Treffen auf ein Levelelement wird die Bullet zerstört
		if (hit.gameObject.CompareTag("Ground"))
		{
			Destroy(gameObject);
		}
		
		if (explosion)
		{
			Instantiate(explosion, transform.position, transform.rotation);
			// TODO Fehler beseitigen
			//audio.PlayClipAtPoint(fxSound, transform.position);
		}
	}	

}