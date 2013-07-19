/**
 * Zustand in der die Mine nichts tut, als zu überprüfen ob
 * der Player in Range ist
 * 
*/
public class SMineIdle : State<Enemy<Mine>> {
	
	
	public void Enter(Enemy<Mine> owner){
		owner.SetSprite(0);
	}
	
	
	public void Execute(Enemy<Mine> owner){
		//TODO: Check-Distanz zum Spieler
		if(false){
			owner.AttackFSM.ChangeState(SMineIdle.Instance);
		}
	}
	
	
	public void Exit(Enemy<Mine> owner){}
	
	
	/**
	 * Singleton
	*/
	private static SMineIdle instance;
	private SMineIdle(){}
	public static SMineIdle Instance{get{
			if(instance==null) instance = new SMineIdle();
			return instance;
		}}
}
