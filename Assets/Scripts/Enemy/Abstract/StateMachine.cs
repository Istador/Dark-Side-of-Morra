public class StateMachine<T> {
	private T owner;
	private State<T> globalState;
	private State<T> currentState;
	private State<T> previousState;

	/**
	 * Konstruktor
	 * Parameter: Besitzendes Objekt des Zustandsautomaten
	*/
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
	//nur zum initilaisieren verwenden
	public void SetGlobalState(State<T> state){globalState = state;}
	//nur zum initilaisieren verwenden
	public void SetCurrentState(State<T> state){currentState = state;}
	//nur zum initilaisieren verwenden
	public void SetPreviousState(State<T> state){previousState = state;}
	
	
	/**
	 * Ändert den aktuellen Zustand des Automatens
	 * ruft Exit() des alten und Enter() des neuen Zustands auf
	*/
	public void ChangeState(State<T> state){
		previousState = currentState;
		currentState = state;
		
		if(previousState!=null) previousState.Exit(owner);
		if(currentState!=null) currentState.Enter(owner);
	}
	
	/**
	 * Kehrt zum vorigem Zustand zurück
	 * ruft Exit() des aktuellen und Enter() des vorigen Zustandes auf
	*/
	public void RevertToPreviousState(){
		ChangeState(previousState);
	}
	
	/**
	 * Update Methode die bei jedem Frame aufgerufen wird
	 * deligiert an die Zustände
	*/
	public void Update(){
		if(globalState!=null) globalState.Execute(owner);
		if(currentState!=null) currentState.Execute(owner);
	}
	
	/**
	 * Ist der Zustandsautomat in dem Zustand?
	*/
	public bool isInState(State<T> state){
		return currentState == state || (currentState!=null && currentState.Equals(state));
	}
}
