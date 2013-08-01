using UnityEngine;
using System.Collections;

/// 
/// Abstrakte Oberklasse für unbewegliche Gegner
/// 
public abstract class ImmovableEnemy<T> : Enemy<T> {
	
	
	
	/// <summary>
	/// Initializes a new instance of the <see cref="ImmovableEnemy`1"/> class.
	/// </summary>
	/// <param name='maxHealth'>
	/// Maximale Trefferpunkte des Gegners. Bei 0 HP stirbt der Gegner.
	/// </param>
	public ImmovableEnemy(int maxHealth) : base(maxHealth){
		
	}
	
	
	
}
