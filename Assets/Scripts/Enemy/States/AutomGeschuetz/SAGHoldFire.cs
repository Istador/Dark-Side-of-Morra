using UnityEngine;

/// 
/// In diesem Zustand verweilt das Geschütz bis es in der Lage ist zu schießen.
/// Das Nachladen ist bereits beendet, es wird nur darauf gewartet, dass der
/// Spieler sichtbar und in Reichweite ist
/// 
public class SAGHoldFire : State<Enemy<AutomGeschuetz>> {
	
	
	
	public override void Execute(Enemy<AutomGeschuetz> owner){
		//auf Spieler kann geschossen werden
		if( ((AutomGeschuetz)owner).IsPlayerInFireRange ){
			//zum Feuern Zustand wechseln
			owner.AttackFSM.ChangeState(SAGFire.I);
		}
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SAGHoldFire instance;
	private SAGHoldFire(){}
	public static SAGHoldFire Instance{get{
			if(instance==null) instance = new SAGHoldFire();
			return instance;
		}}
	public static SAGHoldFire I{get{return Instance;}}
}
