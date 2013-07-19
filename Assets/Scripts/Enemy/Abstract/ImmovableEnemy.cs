using UnityEngine;
using System.Collections;

public abstract class ImmovableEnemy : Enemy {
	
	public ImmovableEnemy(int txtCols, int txtRows, int txtFPS, int maxHealth) : base(txtCols, txtRows, txtFPS, maxHealth){
		
	}
	
}
