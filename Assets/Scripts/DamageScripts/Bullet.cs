using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	public int damage;
	public float xLimit;
	public float lifetime = 10;

	void  Update ()
	{
		Destroy(gameObject, lifetime);
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
		
		
	}	

}