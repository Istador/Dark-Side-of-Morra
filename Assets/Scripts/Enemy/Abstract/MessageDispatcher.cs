using UnityEngine;
using System.Collections;

public class MessageDispatcher {
	
	private PriorityQueue<Telegram> pq = new PriorityQueue<Telegram>();
	
	private void Discharge(Telegram msg){
		msg.receiver.HandleMessage(msg);
		
	}
	
	public void Dispatch(Telegram msg){
		if(msg.dispatchTime <= Time.time)
			Discharge(msg);
		else{
			pq.enqueue(msg);
		}
	}
	
	public void Dispatch(MessageReceiver sender, MessageReceiver receiver, string msg, float delay, Object extraInfo){
		Dispatch(new Telegram(sender, receiver, msg, Time.time+delay, extraInfo));
	}
	
	public void DispatchDelayedMessages(){
		float now = Time.time;
		Telegram t = pq.First();
		while(t!=null && t.dispatchTime <= now && t.dispatchTime >= 0.0f){
			Discharge(t);
			pq.RemoveFirst();
			t = pq.First();
		}
	}
	
	
	/**
	 * Singleton
	*/
	private static MessageDispatcher instance;
	private MessageDispatcher(){}
	public static MessageDispatcher Instance{get{
			if(instance==null) instance = new MessageDispatcher();
			return instance;
	}}
	
}