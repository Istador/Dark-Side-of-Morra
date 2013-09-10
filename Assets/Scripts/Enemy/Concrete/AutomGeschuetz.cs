using UnityEngine;
using System.Collections;

public class AutomGeschuetz : ImmovableEnemy<AutomGeschuetz> {
	
	
	
	/// <summary>Explosionsgeräusch</summary>
	public static AudioClip ac_explosion;
	
	
	
	/// <summary>
	/// Entfernung bei welcher der Spieler zu Dicht ist zum Starten von Raketen
	/// </summary>
	public static readonly float f_closeRange = 2.0f;
	
	/// <summary>
	/// Entfernung bei welcher der Spieler zu weit entfernt ist zum Starten von Raketen
	/// </summary>
	public static readonly float f_outOfRange = 7.0f;
	
	/// <summary>
	/// Nachladezeit:
	/// Die Zeit zwischen zwei Raketen die zum Nachladen veranschlagt wird.
	/// </summary>
	public static readonly double d_reloadTime = 3.0; // 3 sekunden nachladen
	
	
	
	public Vector3 bulletSpawn { get{
			return collider.bounds.center 
				+ Heading() * collider.bounds.size.x/4.0f
				+ Vector3.up * collider.bounds.size.y/8.0f
			;
		}
	}
	
	
	
	public AutomGeschuetz() : base(250) { //250 HP
		AttackFSM.CurrentState = SAGHoldFire.Instance;
		
		f_HealthGlobeProbability = 0.8f; //80% drop, 20% kein drop
		f_HealthGlobeBigProbability = 0.8f; //80% big, 20% small
		// 0,8 * ( 0,8 * 50 + 0,2 * 10 ) = 33,6 HP on average
	}
	
	
	
	protected override void Start(){
		base.Start();
		
		if(ac_explosion == null) ac_explosion = (AudioClip) Resources.Load("Sounds/explode");
	}
	
	
	
	protected override void Update(){
		base.Update();
		
		//Spieler rechts vom Geschütz
		if(IsRight(PlayerPos)){
			//Textur vertikal spiegeln
			Vector2 tmp = gameObject.renderer.material.mainTextureScale;
			tmp = new Vector2(-tmp.x, tmp.y);
			gameObject.renderer.material.mainTextureScale = tmp;
		}
	}
	
	
	
	//Überschreiben um beim Tod zu explodieren
	public override void Death(){
				
		//Explosionsanzeige
		GameObject explosion = (GameObject) UnityEngine.Object.Instantiate(Resources.Load("prefab Explosion"), transform.position, transform.rotation);
		UnityEngine.Object.Destroy(explosion, 0.5f); //nach 0.5 sekunden explosion weg
		
		//Explosionsgeräusch
		AudioSource.PlayClipAtPoint(ac_explosion, collider.bounds.center);
		
		base.Death();
	}
	
	
	
	public Vector3 Heading(){
		if(IsRight(Player))
			return Vector3.right;
		else
			return Vector3.left;
	}
	
	
	
	/// <summary>
	/// Ob der Spieler in Schussreichweite ist.
	/// </summary>
	public bool IsPlayerInFireRange(){
		float distance = DistanceToPlayer();
		return ( 
			   distance <= f_outOfRange
			&& distance >= f_closeRange
			&& LineOfSight(Player)
		);
	}
}
