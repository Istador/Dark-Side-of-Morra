using UnityEngine;

// 
// Alle Leiter Zustände haben gemeinsam, dass sie als erstes anhalten
// 
public class SLState : State<Enemy<Soldier>> {
	
	public override void Enter(Enemy<Soldier> owner){
		//anhalten
		((Soldier)owner).StopMoving();
	}
	
}
