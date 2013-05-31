#pragma strict
var maxHealth : float = 100;
public var currentHealth : float;

function Start () {
	currentHealth = maxHealth;
}

function ApplyDamage ( Damage : float ){
	
	if (currentHealth < 0) {
		return;
	
	}
	currentHealth -= Damage;
	
	if (currentHealth == 0) {
		Destroy(gameObject);
	}
	
}
