using UnityEngine;
using System.Collections;

public class SRPGSSeek : State<Enemy<RPGSoldier>> {
	
	
	
	public override void Enter(Enemy<RPGSoldier> owner){}
	
	
	
	public override void Execute(Enemy<RPGSoldier> owner){
		//Distanz zum Spieler ermitteln
		Vector3 pos = ((RPGSoldier)owner).LastKnownPosition();
		float distance = owner.DistanceTo(pos);
		
		
		Vector3 right = owner.collider.bounds.center + Vector3.right * owner.collider.bounds.size.x/1.995f;
		Vector3 rightdown = right + Vector3.down * owner.collider.bounds.size.y/1.995f;
		Vector3 left = owner.collider.bounds.center + Vector3.left * owner.collider.bounds.size.x/1.995f;
		Vector3 leftdown = left + Vector3.down * owner.collider.bounds.size.y/1.995f;
		
		bool seekright = ((RPGSoldier)owner).IsRight(pos);
		
		if(seekright){
			Debug.DrawLine(owner.collider.bounds.center, right, Color.red);
			Debug.DrawLine(right, rightdown, Color.blue);
		} else{
			Debug.DrawLine(owner.collider.bounds.center, left, Color.red);
			Debug.DrawLine(left, leftdown, Color.blue);
		}
		
		int layer = 1<<8; //Layer 8: Level (also  Kollision mit Level-Geometrie)
		if( (seekright && 
			(    Physics.Linecast(owner.collider.bounds.center, right, layer)
			  || !Physics.Linecast(right, rightdown, layer)
			)
			)||(
			!seekright &&
			(    Physics.Linecast(owner.collider.bounds.center, left, layer)
			  || !Physics.Linecast(left, leftdown, layer)
			)
			)
		){
			((RPGSoldier)owner).steering.Seek(false);
		} else {
			((RPGSoldier)owner).steering.Seek(true);
			((RPGSoldier)owner).steering.SetTarget(owner.player.collider.bounds.center);
		}
		
		
		//optimale position erreicht
		if(distance <= (RPGSoldier.f_optimum_min + RPGSoldier.f_optimum_max)/2.0f )
			owner.MoveFSM.ChangeState(SRPGSStay.Instance);
	}
	
	
	
	public override void Exit(Enemy<RPGSoldier> owner){}
	
	
	
	/**
	 * Singleton
	*/
	private static SRPGSSeek instance;
	private SRPGSSeek(){}
	public static SRPGSSeek Instance{get{
			if(instance==null) instance = new SRPGSSeek();
			return instance;
		}}
	
	
	
}