using UnityEngine;
using System.Collections;

public class SSoldierFire : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		//Bullet vom Prefab erstellen
		GameObject bullet = (GameObject) UnityEngine.Object.Instantiate(Resources.Load("pBullet"), owner.transform.position, owner.transform.rotation);
		
		//Richtung der Patrone setzen
		bullet.GetComponent<PBullet>().heading = ((Soldier)owner).Heading();
		
		//Sound abspielen
		owner.audio.PlayOneShot(((Soldier)owner).ac_shoot);
		
		//Kollisionen zwischen diesem Gegner und dieser Rakete ignorieren
		Physics.IgnoreCollision(owner.collider, bullet.collider);
		Physics.IgnoreCollision(bullet.collider, owner.collider);
		
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
