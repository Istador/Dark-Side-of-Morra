using UnityEngine;
/**
 * Zustand in der die Mine stirbt, und explodiert
 * 
*/
public class SMineDead : State<Enemy<Mine>> {
	
	
	
	public override void Enter(Enemy<Mine> owner){
		owner.Visible = false; //Mine ausblenden
		
		//Schaden an allen in Reichweite machen
		float maxrange = Mine.f_explosionRadius;
		float maxdmg = Mine.f_explosionDamage;
		GameObject player = GameObject.FindWithTag("Player");
		
		//höhe an die Position des Spielers anpassen
		Vector3 explosionsursprung = new Vector3(
			owner.transform.position.x,
			owner.transform.position.y - owner.renderer.bounds.size.y/2.0f+player.collider.bounds.size.y/2.0f,
			owner.transform.position.z
		);
				
		//wer Kollidiert alles mit der runden Explosion
		Collider[] cs = Physics.OverlapSphere(explosionsursprung, maxrange);
		foreach(Collider c in cs){
			if(c.gameObject.tag == "Enemy" || c.gameObject.tag == "Player" ){
				//Entfernung des Objektes zum Explosionszentrum
				float range = Mathf.Abs(Vector3.Distance(explosionsursprung, c.bounds.center));
				//float range = Mathf.Abs(Vector3.Distance(explosionsursprung, c.gameObject.transform.position));
				//float range = Mathf.Abs(owner.transform.position.x - c.gameObject.transform.position.x);
				if(range >= 0.0f && range <= maxrange){
					//Schaden proportional zur Entfernung berechnen
					int dmg = (int)( maxdmg * (1.0f - range/maxrange) );
					
					//Schadensmeldung verschicken
					if(dmg > 0) 
						owner.DoDamage(c, dmg);
				}
			}
		}
		
		
		//Explosionsanzeige
		GameObject explosion = owner.Instantiate("prefab Explosion", explosionsursprung);
		
		//scale explosion
		//explosion.particleEmitter.minSize = 0.5f;
		//explosion.particleEmitter.maxSize = 2.0f;
		//explosion.GetComponent<ParticleRenderer>().lengthScale = 2.0f;
		
		//Explosionsgeräusch
		owner.PlaySound("explode");
		
		Object.Destroy(explosion,1.0f); //nach 1 sekunden explosion entfernen
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
