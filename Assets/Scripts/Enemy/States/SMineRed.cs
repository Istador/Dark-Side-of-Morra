/**
 * Zustand in der die Mine rot blinkt, und nach einer Sekunde explodiert.
 * 
*/
public class SMineRed : State<Enemy<Mine>> {
	
	
	public override void Enter(Enemy<Mine> owner){
		owner.SetSprite(2);
		MessageDispatcher.Instance.Dispatch(owner, owner, "minetimer", 2.0f, null);
		
		//TODO: Soundgeräusch
	}
	
	public override void Execute(Enemy<Mine> owner){}
	
	
	public override void Exit(Enemy<Mine> owner){}
	
	public override bool OnMessage(Enemy<Mine> owner, Telegram msg){
		switch(msg.message){
			case "minetimer":
				owner.AttackFSM.ChangeState(SMineDead.Instance);
				return true;
			default:
				return false;
		}
	}
	
	
	/**
	 * Singleton
	*/
	private static SMineRed instance;
	private SMineRed(){}
	public static SMineRed Instance{get{
			if(instance==null) instance = new SMineRed();
			return instance;
		}}
}
