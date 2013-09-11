using UnityEngine;

/// 
/// In diesem Zustand bewegt sich die Spinne auf den Spieler zu
/// 
public class SSpiderSeek : State<Enemy<Spider>> {
	
	
	
	public override void Execute(Enemy<Spider> owner){
		//Texturrichtung
		if( owner.IsRight(owner.Player) )
			owner.Sprite = 3;
		else owner.Sprite = 2;
		
		//Spieler in Nahkampfreichweite
		if(owner.DistanceToPlayer <= Spider.f_seekRange){
			//anhalten
			owner.MoveFSM.ChangeState(SSpiderStehen.I); 
			return;
		}
		
		//immer noch zu weit weg -> weiter annähern
		((Spider)owner).Steering.DoSeek(owner.PlayerPos);
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SSpiderSeek instance;
	private SSpiderSeek(){}
	public static SSpiderSeek Instance{get{
			if(instance==null) instance = new SSpiderSeek();
			return instance;
		}}
	public static SSpiderSeek I{get{return Instance;}}
}
