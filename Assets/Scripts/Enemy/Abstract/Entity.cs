using UnityEngine;
using System.Collections;

public abstract class Entity : MonoBehaviour {
	public abstract int health {get; protected set;}
	public abstract int maxHealth {get; protected set;}
	public abstract float healthFactor {get; protected set;}
}
