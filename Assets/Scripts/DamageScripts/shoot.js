#pragma strict
var prefabBullet:Transform;
var bulletForce:float;
var xLimit: float = -10;

function Start () {

}

function Update () {
	
	if(Input.GetButtonDown("Jump")){
		var instanceBullet = Instantiate(prefabBullet, transform.position, Quaternion.identity);
		instanceBullet.rigidbody.AddForce(transform.right * bulletForce, ForceMode.Impulse);
	}
	if (transform.position.x >= xLimit)
	{
	Destroy(instanceBullet);	 // remove the object from the scene
	}
}
