﻿using UnityEngine;
using System.Collections;

public class SSpiderSeek : State<Enemy<Spider>> {
	
	
		
	public override void Enter(Enemy<Spider> owner){
		//anhalten
		owner.rigidbody.velocity = Vector3.zero;
		owner.rigidbody.angularVelocity = Vector3.zero;
		((Spider)owner).steering.Seek(false);
	}
	
	
	public override void Execute(Enemy<Spider> owner){
		//Texturrichtung
		bool right = owner.IsRight(owner.player.collider.bounds.center);
		if(right)
			owner.SetSprite(3);
		else owner.SetSprite(2);
		
		//Distanz zum Spieler ermitteln
		float distance = owner.DistanceToPlayer();
		//nahkampfreichweite
		if(distance <= Spider.f_seekRange){
			owner.MoveFSM.ChangeState(SSpiderStehen.Instance); //anhalten
			return;
		}
		
		//immer noch zu weit weg -> annähern
		((Spider)owner).steering.SetTarget(owner.player.collider.bounds.center);
		((Spider)owner).steering.Seek(true);
	}
	
	
	
	public override void Exit(Enemy<Spider> owner){
		//Seek aus
		((Spider)owner).steering.Seek(false);
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SSpiderSeek instance;
	private SSpiderSeek(){}
	public static SSpiderSeek Instance{get{
			if(instance==null) instance = new SSpiderSeek();
			return instance;
		}}
	
	
	
}