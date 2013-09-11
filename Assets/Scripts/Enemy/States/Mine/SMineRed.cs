using UnityEngine;

/**
 * Zustand in der die Mine rot blinkt, und nach einer Sekunde explodiert.
 * 
*/
public class SMineRed : State<Enemy<Mine>> {
	
	
	
	public override void Enter(Enemy<Mine> owner){
		//Sprite auswählen
		owner.Sprite = 2; //rot blinken
		
		//sich selbst eine Nachricht in X Sekunden schicken zum explodieren
		MessageDispatcher.I.Dispatch(owner, "minetimer", Mine.f_tickDuration);
	}
	
	
	
	public override void Execute(Enemy<Mine> owner){
		//Ticken
		
		//wenn der Frame zu dem getickt werden soll angezeigt wird
		if(owner.SpriteCntrl.index == 0){
			//wenn zu diesem Frame noch nicht getickt wurde
			if(! ((Mine)owner).ticked ){
				//Sound abspielen
				owner.PlaySound("minetick");
				//Merken das schon getickt wurde
				((Mine)owner).ticked = true;
			}
		} else
			//Tick-Merker zurücksetzen
			((Mine)owner).ticked = false;
	}
	
	
	
	public override bool OnMessage(Enemy<Mine> owner, Telegram msg){
		switch(msg.message){
			//Die Zeit im rotem Zustand ist um
			case "minetimer":
				//Explodieren
				owner.AttackFSM.ChangeState(null);
				return true;
			default:
				return false;
		}
	}
	
	
	
	public override void Exit(Enemy<Mine> owner){
		//Mine ausblenden
		owner.Visible = false;
		
		//Schaden an allen Objekten in Reichweite machen
		float maxrange = Mine.f_explosionRadius;
		float maxdmg = Mine.f_explosionDamage;
		
		//höhe an die Position des Spielers anpassen
		Vector3 explosionsursprung = new Vector3(
			owner.Pos.x,
			owner.Pos.y - owner.Height/2.0f + owner.Player.collider.bounds.size.y/2.0f,
			owner.Pos.z
		);
		
		//Kollision mit Entity (inkl. Spieler) oder Projektile
		int layer = (int)GeneralObject.Layer.Explosion;
		
		//wer Kollidiert alles mit der runden Explosion
		Collider[] cs = Physics.OverlapSphere(explosionsursprung, maxrange, layer);
		foreach(Collider c in cs){
			//Entfernung des Objektes zum Explosionszentrum berechnen
			float range = Mathf.Abs(Vector3.Distance(explosionsursprung, c.bounds.center));
			
			//innerhalb der Reichweite
			if(range >= 0.0f && range <= maxrange){
				//Schaden proportional zur Entfernung berechnen
				int dmg = (int)( maxdmg * (1.0f - range/maxrange) );
				
				//Schadensmeldung verschicken
				if(dmg > 0) owner.DoDamage(c, dmg);
			}
		}
		
		//Explosionsanzeige
		GameObject explosion = owner.Instantiate("prefab Explosion", explosionsursprung);
		
		//Explosionsgeräusch
		owner.PlaySound("explode");
		
		//Explosion nach 1 sekunden entfernen
		Object.Destroy(explosion, 1.0f);
		
		//Mine zerstören
		owner.Death();
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SMineRed instance;
	private SMineRed(){}
	public static SMineRed Instance{get{
			if(instance==null) instance = new SMineRed();
			return instance;
		}}
	public static SMineRed I{get{return Instance;}}
}
