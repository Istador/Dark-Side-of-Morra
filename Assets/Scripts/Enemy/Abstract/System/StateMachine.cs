/*
 * Zustandsautomat
 * 
 * Funktionalität:
 * - globaler Zustand
 * - aktueller Zustand
 * - voriger Zustand
 * - Zustandsübergänge
 * - Nachrichtensystem
 * 
 * Quelle:
 * Mat Buckland - Programming Game AI by Example
*/
public class StateMachine<T> : MessageReceiver {
	
	
	
	/// <summary>
	/// Besitzer dieses Zustandsautomatens
	/// </summary>
	private T owner;
	
	/// <summary>
	/// Der globale Zustand des Automatens
	/// </summary>
	private State<T> globalState;
	
	/// <summary>
	/// Der aktuelle Zustand des Automatens
	/// </summary>
	private State<T> currentState;
	
	/// <summary>
	/// Der vorherige Zustand (vor dem aktuellen Zustand)
	/// </summary>
	private State<T> previousState;
	
	
	
	/// <summary>
	/// Initializes a new instance of the <see cref="StateMachine`1"/> class.
	/// </summary>
	/// <param name='owner'>
	/// Besitzer dieses Zustandsautomatens
	/// </param>
	public StateMachine(T owner){
		this.owner = owner;
	}
	
	
	
	/*
	 * Getter
	*/
	public State<T> GetGlobalState(){return globalState;}
	public State<T> GetCurrentState(){return currentState;}
	public State<T> GetPreviousState(){return previousState;}
	
	
	
	/*
	 * Setter
	*/ 
	/// <summary>
	/// ! nur zum Initialisieren verwenden !
	/// </summary>
	public void SetGlobalState(State<T> state){globalState = state;}
	/// <summary>
	/// ! nur zum Initialisieren verwenden !
	/// </summary>
	public void SetCurrentState(State<T> state){currentState = state;}
	/// <summary>
	/// ! nur zum Initialisieren verwenden !
	/// </summary>
	public void SetPreviousState(State<T> state){previousState = state;}
	
	
	
	/// <summary>
	/// Ändert den aktuellen Zustand des Automatens.
	/// ruft Exit() des alten und Enter() des neuen Zustands auf.
	/// </summary>
	/// <param name='state'>
	/// der neue Zustand zu dem gewechselt werden soll.
	/// </param>
	public void ChangeState(State<T> state){
		previousState = currentState;
		currentState = state;
		
		if(previousState!=null) previousState.Exit(owner);
		if(currentState!=null) currentState.Enter(owner);
	}
	
	
	
	/// <summary>
	/// Ändert den globalen Zustand des Automatens.
	/// ruft Exit() des alten und Enter() des neuen Zustands auf.
	/// </summary>
	/// <param name='state'>
	/// der neue Zustand zu dem gewechselt werden soll.
	/// </param>
	public void ChangeGlobalState(State<T> state){
		if(globalState!=null) globalState.Exit(owner);
		globalState = state;
		if(globalState!=null) globalState.Enter(owner);
	}
	
	
	
	/// <summary>
	/// Kehrt zum vorigem Zustand zurück.
	/// ruft Exit() des aktuellen und Enter() des vorigen Zustandes auf.
	/// </summary>
	public void RevertToPreviousState(){
		ChangeState(previousState);
	}
	
	
	
	/// <summary>
	/// Methode um die Zustandsmaschine zu "starten".
	/// Ruft die Enter-Methode des aktuellen Zustandes auf.
	/// </summary>
	public void Start(){
		if(currentState!=null) currentState.Enter(owner);
	}
	
	
	
	/// <summary>
	/// Update Methode die bei jedem Frame aufgerufen wird.
	/// Deligiert an die jeweiligen Zustände.
	/// </summary>
	public void Update(){
		if(globalState!=null) globalState.Execute(owner);
		if(currentState!=null) currentState.Execute(owner);
	}
	
	
	
	/// <summary>
	/// Ist der Zustandsautomat in dem angefragten Zustand?
	/// </summary>
	/// <returns>
	/// ob der Zustandsautomat in den angefragten Zustand ist.
	/// </returns>
	/// <param name='state'>
	/// Der Zustand der überprüft werden soll.
	/// </param>
	public bool isInState(State<T> state){
		return currentState == state || (currentState!=null && currentState.Equals(state));
	}
	
	
	
	/// <summary>
	/// Nachrichten an den aktuellen Zustand weitergeben.
	/// Gibt die Nachricht an den globalen Zustand, wenn der aktuelle Zustand
	/// die Nachricht nicht verarbeiten kann.
	/// </summary>
	/// <returns>
	/// ob die Nachricht verarbeitete werden konnte.
	/// </returns>
	/// <param name='msg'>
	/// Die Nachricht die verarbeitet werden soll.
	/// </param>
	public bool HandleMessage(Telegram msg){
		//wenn der aktuelle Zustand die Nachricht verarbeiten kann
		if(currentState!=null && currentState.OnMessage(owner, msg))
			return true;
		//falls aktueller zustand nicht kann, dann globaler
		else if(globalState!=null && globalState.OnMessage(owner, msg))
			return true;
		//Nachricht unverarbeitet
		return false;
	}
	
	
	
}
