using UnityEngine;

// 
// In diesem Zustand führt die Spinne einen Nachkampfangriff aus.
// Die Angriffsanimation wird manuell gesteuert.
// Während der Angriffsanimation kann die Spinne sich nicht bewegen oder drehen
// Der Angriff verursacht Flächenschaden vor der Spinne, sobald die Animation
// vorbei ist.
// 
public class SSpiderAngreifen : State<Enemy<Spider>> {
	
	
		
	public override void Enter(Enemy<Spider> owner){
		//Gravitation ausschalten (falls sie auf der Rampe steht)
		owner.constantForce.enabled = false;
		
		//Ist der Spieler rechts?
		bool right = owner.IsRight(owner.Player);
		
		//Angriffsrichtung merken für den Angriff
		((Spider)owner).v_attackVector = ( right ? Vector3.right : Vector3.left );
		
		//Texturrichtung für den Angriff festlegen
		if(right)
			owner.Sprite = 5;
		else owner.Sprite = 4;
	}
	
	
	
	public override void Execute(Enemy<Spider> owner){
		//nicht vom SpriteController animieren lassen, sondern selbst
		owner.SkipAnimation = true;
		
		//Ist dies der letzte Frame?
		if( ((Spider)owner).AttackFrame() ) { //Textur animieren
			//zum stehen Zustand
			owner.MoveFSM.ChangeState(SSpiderStehen.Instance);
		}
	}
	
	
	
	public override void Exit(Enemy<Spider> owner){
		//Layer 9 = Entity (inkl. Player)
		int layer = (int)GeneralObject.Layer.Entity;
		
		//nur in einem bestimmten Bereich Schaden verursachen
		Collider[] cs = OverlapSphere(owner.Pos + ((Spider)owner).v_attackVector, Spider.f_attackRange, layer);
		
		//für alle getroffenen Objekte
		foreach(Collider c in cs)
			//wurde der Spieler getroffen?
			if(c.gameObject.tag == "Player" )
				//Schadensmeldung verschicken
				owner.DoDamage(c, Spider.i_damage);
		
		//Gravitation wieder einschalten
		owner.constantForce.enabled = true;
	}
	
	
	
	private static Collider[] OverlapSphere(Vector3 position, float radius, int layer){
		Color color = Color.white;
		
		Debug.DrawLine(position, position + Vector3.up * radius, color);
		Debug.DrawLine(position, position + (Vector3.up + Vector3.right).normalized * radius, color);
		Debug.DrawLine(position, position + Vector3.right * radius, color);
		Debug.DrawLine(position, position + (Vector3.down + Vector3.right).normalized * radius, color);
		Debug.DrawLine(position, position + Vector3.down * radius, color);
		Debug.DrawLine(position, position + (Vector3.down + Vector3.left).normalized * radius, color);
		Debug.DrawLine(position, position + Vector3.left * radius, color);
		Debug.DrawLine(position, position + (Vector3.up + Vector3.left).normalized * radius, color);
		
		return Physics.OverlapSphere(position, radius, layer);
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SSpiderAngreifen instance;
	private SSpiderAngreifen(){}
	public static SSpiderAngreifen Instance{get{
			if(instance==null) instance = new SSpiderAngreifen();
			return instance;
		}}
	public static SSpiderAngreifen I{get{return Instance;}}
}
