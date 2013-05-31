#pragma strict
var damage:float;
var xLimit: float;

function Update () {}
	function OnCollisionEnter(hit: Collision){
	if(hit.gameObject.CompareTag("Enemy")){
		 var target = hit.gameObject;
    	// Apply damage to target object
    	target.collider.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
    	Destroy(gameObject);
	}
	if (transform.position.x <= xLimit)
	{
	Destroy(gameObject);	 // remove the object from the scene
	}
}	
