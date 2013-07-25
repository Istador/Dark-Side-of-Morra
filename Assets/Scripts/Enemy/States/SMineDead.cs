using UnityEngine;
/**
 * Zustand in der die Mine nichts tut, als zu überprüfen ob
 * der Player in Range ist
 * 
*/
public class SMineDead : State<Enemy<Mine>> {
	
	
	public override void Enter(Enemy<Mine> owner){
		owner.SetInvisible(); //Mine ausblenden
		
		//Schaden an allen in Reichweite machen
		float maxrange = ((Mine)owner).f_explosionRadius;
		float maxdmg = ((Mine)owner).f_explosionDamage;
		
		Collider[] cs = Physics.OverlapSphere(owner.transform.position, maxrange);
		foreach(Collider c in cs){
			if(c.gameObject.tag == "Enemy" || c.gameObject.tag == "Player" ){
				//Entfernung des Objektes zum Explosionszentrum
				//float range = Mathf.Abs(Vector3.Distance(owner.transform.position * Vector3(), c.gameObject.transform.position));
				float range = Mathf.Abs(owner.transform.position.x - c.gameObject.transform.position.x);
				if(range >= 0.0f && range <= maxrange){
					//Schaden proportional zur Entfernung berechnen
					int dmg = (int)( maxdmg * (1.0f - range/maxrange) );
					
					//Schadensmeldung verschicken
					if(dmg > 0) c.gameObject.SendMessage("ApplyDamage", dmg);
				}
			}
		}
		
		
		//Explosionsanzeige
		GameObject explosion = (GameObject) UnityEngine.Object.Instantiate(Resources.Load("prefab Explosion"), owner.transform.position, owner.transform.rotation);
		//scale explosion
		explosion.particleEmitter.minSize = 0.5f;
		explosion.particleEmitter.maxSize = 2.0f;
		explosion.GetComponent<ParticleRenderer>().lengthScale = 2.0f;
		
		UnityEngine.Object.Destroy(explosion,1.0f); //nach 1 sekunden explosion entfernen
	}
	
	
	public override void Execute(Enemy<Mine> owner){
		owner.AttackFSM.ChangeState(null);
	}
	
	
	public override void Exit(Enemy<Mine> owner){
		owner.Death(); //Mine zerstören
	}
	
	/**
	 * Singleton
	*/
	private static SMineDead instance;
	private SMineDead(){}
	public static SMineDead Instance{get{
			if(instance==null) instance = new SMineDead();
			return instance;
		}}
}
