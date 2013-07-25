using System;
using UnityEngine;

public class Telegram : IComparable<Telegram> {
	
	//Absender der Nachricht
	public readonly MessageReceiver sender;
	
	//Empfänger der Nachricht
	public readonly MessageReceiver receiver;
	
	//die Nachricht selbst
	public readonly string message;
	
	//Zeitpunkt der Nachrichten-Auslieferung
	public readonly float dispatchTime;
	
	//Zusätzliche Daten der Nachricht
	public readonly object extraInfo;
	
	public Telegram(MessageReceiver sender, MessageReceiver receiver, string message, float dispatchTime, object extraInfo){
		this.sender = sender;
		this.receiver = receiver;
		this.message = message;
		this.dispatchTime = dispatchTime;
		this.extraInfo = extraInfo;
	}
	
	public Telegram(MessageReceiver receiver, string message, float dispatchTime, object extraInfo)
		: this(null, receiver, message, dispatchTime, extraInfo){}
	
	public Telegram(MessageReceiver receiver, string message, object extraInfo)
		: this(receiver, message, Time.time, extraInfo){}
	
	public Telegram(MessageReceiver receiver, string message, float dispatchTime)
		: this(receiver, message, dispatchTime, null){}
	
	
	
	public Telegram(MessageReceiver sender, MessageReceiver receiver, string message, object extraInfo)
		: this(sender, receiver, message, Time.time, extraInfo){}
	
	public Telegram(MessageReceiver sender, MessageReceiver receiver, string message)
		: this(sender, receiver, message, null){}
	
	
	
	public Telegram(MessageReceiver sender, MessageReceiver receiver, string message, float dispatchTime)
		: this(sender, receiver, message, dispatchTime, null){}
	
	
	
	public Telegram(MessageReceiver receiver, string message) : this(null, receiver, message){}
	
	public int CompareTo(Telegram other){
		return this.dispatchTime.CompareTo(other.dispatchTime);
	}
}
