using UnityEngine;
using System.Collections;

public abstract class ImmovableEnemy<T> : Enemy<T> {
	
	public ImmovableEnemy(int maxHealth) : base(maxHealth){
		
	}
	
}
