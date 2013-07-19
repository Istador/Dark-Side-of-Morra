/**
 * ein einzelner Zustand
*/
public interface State<T> {
	
	
	/**
	 * Code der beim Betreten des Zustandes ausgeführt werden soll.
	*/
	void Enter(T owner);
	
	
	/**
	 * Code der bei jedem neuem Update ausgeführt werden soll.
	*/
	void Execute(T owner);
	
	
	/**
	 * Code der beim Verlassen des Zustandes ausgeführt werden soll.
	*/
	void Exit(T owner);
	
	
}
