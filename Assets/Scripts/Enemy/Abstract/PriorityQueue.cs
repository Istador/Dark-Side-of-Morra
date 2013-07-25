using System;
using System.Collections;
using System.Collections.Generic;

public class PriorityQueue<T> where T: IComparable<T> {
	
	private SortedList<T, T> list;
	private HashSet<T> menge;
	
	public PriorityQueue(){
		list = new SortedList<T,T>();
		menge = new HashSet<T>();
	}
	
	public PriorityQueue(params T[] args){
		foreach(T a in args)
			enqueue(a);
	}
	
	public void enqueue(T t){
		if(!menge.Contains(t)){
			list.Add(t, t);
			menge.Add(t);
		}
		
	}
	
	public T First(){
		if(list.Count > 0)
			return list.Values[0];
		return default(T);
	}
	public void RemoveFirst(){
		if(list.Count > 0){
			menge.Remove(First());
			list.RemoveAt(0);
		}
	}
	
	public T dequeue(){
		T t = First();
		RemoveFirst();
		return t;
	}
}
