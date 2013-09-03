using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	public int damage;
	public float lifetime = 10;
	public Transform explosion; // explosion
	public AudioClip fxSound;	// sound on explosion

	public PlayerController playerController;

	void Start ()
	{
		playerController = GetComponent<PlayerController>();
	}

	void  Update ()
	{
		Destroy(gameObject, lifetime);
		// TODO auf lookRight von playerController prüfen
		// TODO dementsprechend nach links oder rechts bewegen
		// TODO Bullet Partikel durch etwas anderes ersetzen...
	}

	void  OnCollisionEnter ( Collision hit  )
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