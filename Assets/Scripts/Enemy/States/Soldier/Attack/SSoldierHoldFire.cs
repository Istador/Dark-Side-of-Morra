using UnityEngine;

/// 
/// In diesem Zustand verweilt der Soldat bis er in der Lage ist zu schießen.
/// Das Nachladen ist bereits beendet, es wird nur darauf gewartet, dass der
/// Spieler sichtbar und in Reichweite ist
/// 
public class SSoldierHoldFire : State<Enemy<Soldier>> {
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		//auf Spieler kann geschossen werden und Soldat befindet sich nicht auf einer Leiter
		if(((Soldier)owner).IsPlayerInFireRange && ! ((Soldier)owner).IsOnLadder)
			//zum Feuern Zustand wechseln
			owner.AttackFSM.ChangeState(SSoldierFire.I);
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SSoldierHoldFire instance;
	private SSoldierHoldFire(){}
	public static SSoldierHoldFire Instance{get{
			if(instance==null) instance = new SSoldierHoldFire();
			return instance;
		}}
	public static SSoldierHoldFire I{get{return Instance;}}
}
