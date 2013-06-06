#pragma strict

var prefabBullet: Transform;


function Update () {
	Transform.translate(0,0 Time*deltaTime);
	var instanceBullet = Instantiate(prefabBullet, transform.position, Quaternion.identity);
	instanceBullet.Transform.translate(0,0 Time*deltaTime); 
}