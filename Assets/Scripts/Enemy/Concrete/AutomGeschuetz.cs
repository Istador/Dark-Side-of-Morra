using UnityEngine;
using System.Collections;

/// 
/// Das Automatische Geschütz befindet sich stationär an einer Position und 
/// verschießt auf den Spieler Raketen, wenn sich dieser in einer bestimmten
/// Schussreichweite befindet.
/// 
public class AutomGeschuetz : ImmovableEnemy<AutomGeschuetz> {
	
	
	
	/// <summary>
	/// Entfernung bei welcher der Spieler zu Dicht ist zum Starten von Raketen
	/// </summary>
	public static readonly float f_closeRange = 2.0f;
	
	/// <summary>
	/// Entfernung bei welcher der Spieler zu weit entfernt ist zum Starten von Raketen
	/// </summary>
	public static readonly float f_outOfRange = 7.0f;
	
	/// <summary>
	/// Nachladezeit in Sekunden:
	/// Die Zeit zwischen zwei Raketen die zum Nachladen veranschlagt wird.
	/// </summary>
	public static readonly float f_reloadTime = 3.0f; // 3 sekunden nachladen
	
	
	
	/// <summary>
	/// Position im lokalem Koordinatensystem des Geschützes, an der die 
	/// Raketen gespawnt werden sollen.
	/// </summary>
	public Vector3 bulletSpawn { get{
			return Pos 
				+ Heading * Width/4.0f
				+ Vector3.up * Height/8.0f
			;
		}
	}
	
	
	
	public AutomGeschuetz() : base(250) { //250 HP
		//Angriffs Zustandsautomat initialisieren
		AttackFSM.CurrentState = SAGHoldFire.I;
		
		//Health-Globe Wahrscheinlichkeiten ändern
		f_HealthGlobeProbability = 0.8f; //80% drop, 20% kein drop
		f_HealthGlobeBigProbability = 0.8f; //80% big, 20% small
		// 0,8 * ( 0,8 * 50 + 0,2 * 10 ) = 33,6 HP on average
	}
	
	
	
	protected override void Update(){
		base.Update();
		
		Vector2 tmp = gameObject.renderer.material.mainTextureScale;
		
		//Der Spieler ist Rechts vom Geschütz
		if( IsRight(PlayerPos) )
			//Textur vertikal spiegeln
			tmp = new Vector2( -Mathf.Abs(tmp.x), tmp.y);
		else
			//Textur nicht vertikal spiegeln
			tmp = new Vector2( Mathf.Abs(tmp.x), tmp.y);
		
		gameObject.renderer.material.mainTextureScale = tmp;
	}
	
	
	
	//Überschreiben um beim Tod zu explodieren
	public override void Death(){
				
		//Explosionsanzeige
		GameObject explosion = Instantiate("prefab Explosion");
		
		//nach 0.5 sekunden explosion entfernen
		UnityEngine.Object.Destroy(explosion, 0.5f); 
		
		//Explosionsgeräusch
		PlaySound("explode");
		
		base.Death();
	}
	
	
	
	/// <summary>
	/// Richtung zum Spieler
	/// </summary>
	public Vector3 Heading{ get{
			if(IsRight(Player))
				return Vector3.right;
			else
				return Vector3.left;
		}
	}
	
	
	
	/// <summary>
	/// Ob der Spieler in Schussreichweite ist.
	/// </summary>
	public bool IsPlayerInFireRange{get{
			//Distanz zum Spieler
			float distance = DistanceTo(Player);
			
			return (
				//kleiner als maximum
			   	distance <= f_outOfRange
				//größer als minimum
				&& distance >= f_closeRange
				//Line of Sight
				&& LineOfSight(Player)
			);
		}
	}
	
	
	
}
