using UnityEngine;
using System.Collections;

public abstract class ImmovableEnemy<T> : Enemy<T> {
	
	public ImmovableEnemy(int txtCols, int txtRows, int txtFPS, int maxHealth) : base(txtCols, txtRows, txtFPS, maxHealth){
		
	}
	
}
