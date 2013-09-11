using UnityEngine;

/// 
/// In diesem Zustand verweilt der RPG-Soldat bis er in der Lage ist zu schießen.
/// Das Nachladen ist bereits beendet, es wird nur darauf gewartet, dass der
/// Spieler sichtbar und in Reichweite ist
/// 
public class SRPGSHoldFire : State<Enemy<RPGSoldier>> {
	
	
	
	public override void Execute(Enemy<RPGSoldier> owner){
		//auf Spieler kann geschossen werden
		if( ((RPGSoldier)owner).IsPlayerInFireRange )
			//zum Feuern Zustand wechseln
			owner.AttackFSM.ChangeState(SRPGSFire.I);
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SRPGSHoldFire instance;
	private SRPGSHoldFire(){}
	private static SRPGSHoldFire Instance{get{
			if(instance==null) instance = new SRPGSHoldFire();
			return instance;
		}}
	public static SRPGSHoldFire I{get{return Instance;}}
}
