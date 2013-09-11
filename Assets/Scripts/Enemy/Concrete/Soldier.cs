using UnityEngine;
using System.Collections;

public class Soldier : MLeftRightClimb<Soldier> {
	
	
	
	/// <summary>
	/// Bullets nicht in der Mitte des Gegners spawnen lassen,
	/// sondern an eine sinnvolle Position verschieben.
	/// </summary>
	/// <value>
	/// Position in Weltkoordinaten an der die Rakete spawnen soll
	/// </value>
	public Vector3 bulletSpawn { get{
			return Pos + Heading * Width/3.0f + Vector3.up * Height/5.0f;
		}
	}
	
	
	
	public static AudioClip ac_shoot;
	
	
	
	/// <summary>
	/// Optimale Entfernung in der stehen geblieben wird. Untere Grenze
	/// </summary>
	public readonly float f_optimum_min =  1.5f + (float)rnd.NextDouble() * 2.0f;
	
	/// <summary>
	/// Optimale Entfernung in der stehen geblieben wird. Obere Grenze
	/// </summary>
	public readonly float f_optimum_max = 5.5f + (float)rnd.NextDouble() * 2.0f;
	
	/// <summary>
	/// Entfernung bei welcher der Spieler zu weit entfernt ist zum Starten von Raketen
	/// </summary>
	public readonly float f_outOfRange = 7.5f + (float)rnd.NextDouble();
	
	/// <summary>
	/// Nachladezeit:
	/// Die Zeit zwischen zwei Raketen die zum Nachladen veranschlagt wird.
	/// </summary>
	public readonly float f_reloadTime = 0.7f + (float)rnd.NextDouble() * 0.4f; // ~0,8 sekunden nachladen
	
	
	
	public Soldier() : base(200) {
		//Zustandsautomaten initialisieren
		MoveFSM.GlobalState = SSoldierPatrol.Instance;
		AttackFSM.CurrentState = SSoldierHoldFire.Instance;
	}
	
	
	
	protected override void Start(){
		base.Start();
		
		//Sprite-Eigenschaften
		txtCols = 10;
		txtRows = 10;
		txtFPS = 12;
		
		//SpriteController einschalten
		Animated = true;
		
		//Geschwindigkeit setzen
		MaxSpeed = 7.0f;
		MaxForce = 7.0f;
		
		//Audio-Referenz laden wenn noch nicht geschehen
		if(ac_shoot == null) ac_shoot = (AudioClip) Resources.Load("Sounds/shoot2");
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
				   //LoS besteht
				&& LineOfSight(Player)
				   //in Blickrichtung
				&& IsPlayerInfront
				   //Höhenunterschied nicht zu groß
				&& IsHeightOk(PlayerPos)
			);
		}
	}
	
	
	
	/// <summary>
	/// Schaden erhalten. überschrieben um den Zustandsautomaten zu 
	/// informieren, sowie die Position zu merken
	/// </summary>
	public override void ApplyDamage(Vector3 damage){
		//Zustandsautomaten informieren
		MessageDispatcher.I.Dispatch(this, "damage");
		
		//Position merken
		RememberNow();
		
		//Schaden anwenden
		base.ApplyDamage(damage);
	}
	
	
	
	/// <summary>
	/// Stellt fest welcher Sprite dargestellt werden soll
	/// </summary>
	public int DetermineSprite(){
		Vector3 h = Heading;
		Vector3 m = Moving;
		//auf Leiter
		if(IsOnLadder){
			if(m == Vector3.up) return 7;	//nach oben
			if(m == Vector3.down) return 8; //nach unten
			return 6;						//stehen/links/rechts
		}
		//keine Bewegung
		if(m == Vector3.zero){
			if(h == Vector3.left) return 2;	//nach links gucken
			else return 3;					//nach rechts gucken
		}
		//Bewegung == Blickrichtung
		if(m == h){ 
			if(h == Vector3.left) return 0;	//nach links
			else return 1;					//nach rechts
		}
		//Bewegung != Blickrichtung
		else {
			if(h == Vector3.left) return 4;	//nach rechts gehen, links gucken
			else return 5;					//nach links gehen, rechts gucken
		}
	}
	
	
	
}
