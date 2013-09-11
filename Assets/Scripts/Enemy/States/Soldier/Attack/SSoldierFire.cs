using UnityEngine;

//
// Zustand in dem der Soldat eine Kugel abfeuert.
//
public class SSoldierFire : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		//Bullet vom Prefab erstellen
		GameObject bullet = owner.Instantiate("pBullet", ((Soldier)owner).bulletSpawn);
		
		//Kollisionen zwischen diesem Gegner und dieser Kugel ignorieren
		owner.IgnoreCollision(bullet);
		
		//Setze den owner der Kugel
		bullet.GetComponent<PBullet>().owner = owner.gameObject;
		
		//Richtung der Patrone setzen
		bullet.GetComponent<PBullet>().heading = ((Soldier)owner).Heading;
		
		//Sound abspielen
		owner.PlaySound("shoot2");
		
		//zum Nachlade Zustand wechseln
		owner.AttackFSM.ChangeState(SSoldierReload.I);
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SSoldierFire instance;
	private SSoldierFire(){}
	public static SSoldierFire Instance{get{
			if(instance==null) instance = new SSoldierFire();
			return instance;
		}}
	public static SSoldierFire I{get{return Instance;}}
}
