#pragma strict
var damage:int;
var xLimit: float;
var lifetime:float;

;
function Update () {
	Destroy(gameObject, lifetime);
}
	function OnCollisionEnter(hit: Collision){
	if(hit.gameObject.CompareTag("Enemy")){
		 var target = hit.gameObject;
    	// Apply damage to target object
    	target.collider.SendMessage("ApplyDamage", (hit.collider.bounds.center - this.collider.bounds.center).normalized * damage , SendMessageOptions.DontRequireReceiver);
    	Destroy(gameObject);
	}
	if(hit.gameObject.CompareTag("Ground")){
		Destroy(gameObject);
	}
	
	
}	
