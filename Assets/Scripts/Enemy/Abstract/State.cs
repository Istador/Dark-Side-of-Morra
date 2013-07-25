/**
 * ein einzelner Zustand
*/
public abstract class State<T> {
	
	
	/**
	 * Code der beim Betreten des Zustandes ausgeführt werden soll.
	*/
	public abstract void Enter(T owner);
	
	
	/**
	 * Code der bei jedem neuem Update ausgeführt werden soll.
	*/
	public abstract void Execute(T owner);
	
	
	/**
	 * Code der beim Verlassen des Zustandes ausgeführt werden soll.
	*/
	public abstract void Exit(T owner);
	
	
	/**
	 * Code der beim Erhalt eines Telegrams ausgeführt werden soll
	*/
	public virtual bool OnMessage(T owner, Telegram msg){
		return false;
	}
	
	
}
