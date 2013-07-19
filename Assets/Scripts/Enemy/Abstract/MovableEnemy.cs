using UnityEngine;
using System.Collections;

public abstract class MovableEnemy<T> : Enemy<T> {
	
	public MovableEnemy(int txtCols, int txtRows, int txtFPS, int maxHealth) : base(txtCols, txtRows, txtFPS, maxHealth){
		
	}
	
}
