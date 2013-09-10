using UnityEngine;
using System.Collections;

public class SSoldierFire : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		//Bullet vom Prefab erstellen
		GameObject bullet = (GameObject) UnityEngine.Object.Instantiate(Resources.Load("pBullet"), owner.transform.position, owner.transform.rotation);
		
		//Richtung der Patrone setzen
		bullet.GetComponent<PBullet>().heading = ((Soldier)owner).Heading;
		
		//Sound abspielen
		AudioSource.PlayClipAtPoint(Soldier.ac_shoot, owner.collider.bounds.center);
		
		//Kollisionen zwischen diesem Gegner und dieser Rakete ignorieren
		Physics.IgnoreCollision(owner.collider, bullet.collider);
		Physics.IgnoreCollision(bullet.collider, owner.collider);
		bullet.GetComponent<PBullet>().owner = owner.gameObject;
		
		//zum Nachlade Zustand wechseln
		owner.AttackFSM.ChangeState(SSoldierReload.Instance);
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
	
	
	
}
