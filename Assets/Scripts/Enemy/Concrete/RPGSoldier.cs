using UnityEngine;
using System.Collections;

public class RPGSoldier : MLeftRight<RPGSoldier> {
	
	
	
	/// <summary>
	/// Raketen nicht in der Mitte des Gegners spawnen lassen,
	/// sondern an eine sinnvolle Position verschieben.
	/// </summary>
	/// <value>
	/// Position in Weltkoordinaten an der die Rakete spawnen soll
	/// </value>
	public Vector3 bulletSpawn { get{
			return Pos + Heading * Width/10.0f + Vector3.up * Height/10.0f;
		}
	}
	
	
	
	/// <summary>
	/// Entfernung bei welcher der Spieler zu Dicht ist zum Starten von Raketen
	/// </summary>
	public static readonly float f_closeRange = 2.0f;
	
	/// <summary>
	/// Optimale Entfernung in der stehen geblieben wird. Untere Grenze
	/// </summary>
	public static readonly float f_optimum_min = 3.5f;
	
	/// <summary>
	/// Optimale Entfernung in der stehen geblieben wird. Obere Grenze
	/// </summary>
	public static readonly float f_optimum_max = 4.5f;
	
	/// <summary>
	/// Entfernung bei welcher der Spieler zu weit entfernt ist zum Starten von Raketen
	/// </summary>
	public static readonly float f_outOfRange = 6.0f;
	
	/// <summary>
	/// Nachladezeit:
	/// Die Zeit zwischen zwei Raketen die zum Nachladen veranschlagt wird.
	/// </summary>
	public static readonly float f_reloadTime = 3.0f; // 3 sekunden nachladen
	
	
	
	public RPGSoldier() : base(150) { //150 HP
		//Zustandsautomaten initialisieren
		MoveFSM.GlobalState = SRPGSPatrol.I;
		AttackFSM.CurrentState = SRPGSHoldFire.I;
		
		//Health-Globe Wahrscheinlichkeiten ändern
		f_HealthGlobeProbability = 0.6f; //60% drop, 40% kein drop
		f_HealthGlobeBigProbability = 0.6f; //60% big, 40% small
		// 0,6 * ( 0,6 * 50 + 0,4 * 10 ) = 20,4 HP on average
	}
	
	
	
	protected override void Start(){
		base.Start();
		
		//Sprite-Eigenschaften
		txtCols = 10;
		txtRows = 6;
		txtFPS = 6;
		
		//SpriteController einschalten
		Animated = true;
		
		//Geschwindigkeit setzen
		MaxSpeed = 5.0f;
		MaxForce = 5.0f;
	}
	
	
	
	protected override void Update(){
		//Sprite setzen
		Sprite = DetermineSprite();
		
		base.Update();
	}
	
	
	
	/// <summary>
	/// Ob der Spieler in Schussreichweite ist.
	/// Diese Methode gibt an ob jetzt geschossen werden kann
	/// </summary>
	public bool IsPlayerInFireRange{ get{
			float distance = DistanceToPlayer;
			return ( 
				   //nicht außer Reichweite
				   distance <= f_outOfRange
				   //nicht zu nah für Raketen
				&& distance >= f_closeRange
				   //LoS besteht
				&& LineOfSight(Player)
				   //in Blickrichtung
				&& IsPlayerInfront
			);
		}
	}
	
	
	
	/// <summary>
	/// Schaden erhalten. überschrieben um den Zustandsautomaten zu 
	/// informieren, sowie die Position zu merken
	/// </summary>
	public override void ApplyDamage(Vector3 damage){
		//Zustandsautomaten informieren, damit von Patrolieren zum Angreifen
		MessageDispatcher.I.Dispatch(this, "damage");
		
		//Position merken
		RememberNow();
		
		//Schaden anwenden
		base.ApplyDamage(damage);
	}
	
	
	
	/// <summary>
	/// Stellt fest welcher Sprite dargestellt werden soll
	/// </summary>
	private int DetermineSprite(){
		Vector3 h = Heading;
		Vector3 m = Moving;
		//keine Bewegung
		if(m == Vector3.zero){
			if(h == Vector3.left) return 2;
			else return 3;
		}
		//Bewegung == Blickrichtung
		else if(m == h){
			if(h == Vector3.left) return 0;
			else return 1;
		}
		//Bewegung != Blickrichtung
		else {
			if(h == Vector3.left) return 5;
			else return 4;
		}
	}
	
	
	
}
