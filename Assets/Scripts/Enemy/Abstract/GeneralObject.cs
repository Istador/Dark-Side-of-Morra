﻿using UnityEngine;
using System.Collections;

/// 
/// Abstrakte Oberklasse für alle Spiel-Objekte im Spiel
/// 
/// Animation über Sprite Controller
/// 
/// Jede Menge nützliche Hilfsmethoden
/// 
public abstract class GeneralObject : MonoBehaviour, MessageReceiver {
	
	
	//Konstruktor
	
	public GeneralObject(){
		//zu verwendendes Sprite für den Sprite-Controller setzen
		Sprite = 0;
		
		//Sprite-Eigenschaften
		txtCols = 1;
		txtRows = 1;
		txtFPS = 1;
	}
	
	
	// Update
	
	/// <summary>
	/// Update Methode die in unregelmäßigen Zeitabständen aufgerufen wird.
	/// 
	/// Animation.
	/// </summary>
	protected virtual void Update() {
		//Sprite Controller zum animieren der Textur wenn vorhanden
		Animate();
	}
	
	
	
	// HandleMessage
	
	/// <summary>
	/// Diesem Objekt wird eine Nachricht zugestellt.
	/// </summary>
	/// <returns>
	/// ob die Nachricht angenommen wurde
	/// Default: false
	/// </returns>
	/// <param name='msg'>
	/// Die Nachricht
	/// </param>
	public virtual bool HandleMessage(Telegram msg){
		return false;
	}
	
	
	
	/// <summary>
	/// Zufallsgenerator des Systems für int und double
	/// </summary>
	public static readonly System.Random rnd = new System.Random();
	
	
	
	// Sichtbarkeit
	
	/// <summary>
	/// Ob dieses Objekt sichtbar ist oder nicht.
	/// </summary>
	/// <value>
	/// <c>true</c> wenn sichtbar; ansonsten, <c>false</c>.
	/// </value>
	public bool Visible {
		get{return renderer.enabled;}
		set{renderer.enabled = value;}
	}
	
	
	
	/// Sprite Controller / Animation
	
	/// <summary>
	/// Zugriff auf den Sprite Controller dieses Objektes
	/// </summary>
	/// <value>
	/// The sprite cntrl.
	/// </value>
	public SpriteController SpriteCntrl {
		get{
			//falls noch nicht vorhanden
			if(_SpriteCntrl == null){
				//hole die Referenz
				_SpriteCntrl = GetComponent<SpriteController>();
				//falls Komponente noch nicht existiert
				if(_SpriteCntrl == null)
					//erstelle sie
					_SpriteCntrl = gameObject.AddComponent<SpriteController>();
			}
			return _SpriteCntrl;
		}
	}
	private SpriteController _SpriteCntrl; //Instanzvariable die von der Property verwendet wird
	
	/// <summary>
	/// Ob dieses Objekt von einem Sprite Controller animiert wird.
	/// </summary>
	/// <value>
	/// <c>true</c> wenn animier; ansonsten, <c>false</c>.
	/// </value>
	public bool Animated {
		get{return _SpriteCntrl == null || SpriteCntrl.enabled; }
		set{ if(_SpriteCntrl != null || value) SpriteCntrl.enabled = value; }
	}
	
	/// <summary>
	/// Ob die Animation für einen Frame ausgesetzt werden soll
	/// </summary>
	/// <value>
	/// <c>true</c> wenn die Animation ausgesetzt werden soll; ansonsten, <c>false</c>.
	/// </value>
	public bool SkipAnimation {get; set;}
	
	/// <summary>
	/// Benutze den Sprite Controller um die Textur zu animieren
	/// </summary>
	private void Animate(){
		if(Animated && !SkipAnimation)
			SpriteCntrl.animate(txtCols, txtRows, 0, Sprite, txtCols, txtFPS);
		SkipAnimation = false;
	}
	
	/// <summary>
	/// Die Zeile in der Textur die für die Animation verwendet werden soll
	/// </summary>
	public int Sprite {get; set;}
	/// <summary>
	/// Anzahl Spalten (Frames)
	/// </summary>
	public int txtCols {get; protected set;}
	/// <summary>
	/// Anzahl Zeilen (Zustände)
	/// </summary>
	public int txtRows {get; protected set;}
	/// <summary>
	/// Frames per Second - Bilder die Sekunde
	/// </summary>
	public int txtFPS {get; protected set;}
	
	
	// Audio
	
	/// <summary>
	/// Spielt einen Sound an der aktuellen Position des Objektes ab
	/// </summary>
	/// <param name='sound'>
	/// Sound der abgespielt werden soll.
	/// </param>
	public void PlaySound(AudioClip sound){
		AudioSource.PlayClipAtPoint(sound, Pos);
	}
	
	
	
	// Layer für die Unity Kollisionsabfragen
	
	public enum Layer {
		/// <summary>
		/// Keine Layer.
		/// </summary>
		Nothing	= 0,
		
		/// <summary>
		/// Alle Layer.
		/// </summary>
		All = ~0,
		
		/// <summary>
		/// Layer 8: Level.
		/// Alles Wodurch sich Spieler, Gegner und Projektile nicht hindurch bewegen dürfen
		/// </summary>
		Level = 1<<8,
		
		/// <summary>
		/// Layer 9: Entities.
		/// Gegner und Spieler.
		/// Kollidieren mit dem Level, Projektilen und Leitern, aber nicht mit anderen Entities.
		/// </summary>
		Entity = 1<<9,
		
		/// <summary>
		/// Layer 10: Projektile.
		/// Kollidieren mit dem Level, Entities und anderen Projektilen.
		/// </summary>
		Projektile	= 1<<10,
		
		/// <summary>
		/// Layer 11: Player Collider.
		/// Kollidiert nur mit Entities.
		/// </summary>
		PlayerCollider = 1<<11,	
		
		/// <summary>
		/// Layer 12: Leitern.
		/// Kollidieren nur mit Entities.
		/// </summary>
		Ladder = 1<<12
	}
	
	
	
	// Linecast
	
	public bool Linecast(Vector3 pos, Layer layerMask = Layer.All){
		return Linecast(Pos, pos, layerMask);
	}
	public bool Linecast(GameObject obj, Layer layerMask = Layer.All){
		return Linecast(Pos, obj.collider.bounds.center, layerMask);
	}
	public bool Linecast(GeneralObject obj, Layer layerMask = Layer.All){
		return Linecast(Pos, obj.Pos, layerMask);
	}
	public bool Linecast(Vector3 pos, out RaycastHit hitInfo, Layer layerMask = Layer.All){
		return Linecast(Pos, pos, out hitInfo, layerMask);
	}
	public bool Linecast(GameObject obj, out RaycastHit hitInfo, Layer layerMask = Layer.All){
		return Linecast(Pos, obj.collider.bounds.center, out hitInfo, layerMask);
	}
	public bool Linecast(GeneralObject obj, out RaycastHit hitInfo, Layer layerMask = Layer.All){
		return Linecast(Pos, obj.Pos, out hitInfo, layerMask);
	}
	
	/// <summary>
	/// Returns true if there is any collider intersecting the line between start and end.
	/// </summary>
	/// <param name='layerMask'>
	/// Layer mask is used to selectively ignore colliders when casting a ray.
	/// </param>
	public bool Linecast(Vector3 start, Vector3 end, Layer layerMask = Layer.All){
		return Physics.Linecast(start, end, (int)layerMask);
	}
	/// <summary>
	/// Linecast the specified start, end, hitInfo and layerMask.
	/// </summary>
	/// <param name='hitInfo'>
	/// If true is returned, hitInfo will contain more information about where the collider was hit.
	/// </param>
	/// <param name='layerMask'>
	/// Layer mask is used to selectively ignore colliders when casting a ray.
	/// </param>
	public bool Linecast(Vector3 start, Vector3 end, out RaycastHit hitInfo, Layer layerMask = Layer.All){
		return Physics.Linecast(start, end, out hitInfo, (int)layerMask);
	}
	
	
	
	// Position
	
	/// <summary>
	/// Position des Objektes in der Spielwelt basierend auf der Collidermittelpunkt.
	/// </summary>
	public Vector3 Pos { get{return collider.bounds.center;} }
	
	
	
	// Positionsbezogene Methoden
	
	// Rechts
	
	/// <summary>
	/// Ob die Position rechts von diesem Objekt ist.
	/// </summary>
	public bool IsRight(Vector3 pos){
		return Vector3.Dot((pos - Pos), Vector3.right) > 0.0f;
	}
	/// <summary>
	/// Ob ein anderes Objekt rechts von diesem Objekt ist.
	/// </summary>
	public bool IsRight(GameObject obj){
		return IsRight(obj.collider.bounds.center);
	}
	/// <summary>
	/// Ob ein anderes Objekt rechts von diesem Objekt ist.
	/// </summary>
	public bool IsRight(Collider obj){
		return IsRight(obj.gameObject);
	}
	/// <summary>
	/// Ob ein anderes Objekt rechts von diesem Objekt ist.
	/// </summary>
	public bool IsRight(Collision obj){
		return IsRight(obj.gameObject);
	}
	/// <summary>
	/// Ob ein anderes Objekt rechts von diesem Objekt ist.
	/// </summary>
	public bool IsRight(GeneralObject obj){
		return IsRight(obj.Pos);
	}
	
	// Links
	
	/// <summary>
	/// Ob die Position links von diesem Objekt ist.
	/// </summary>
	public bool IsLeft(Vector3 pos){
		return ! IsRight(pos);
	}
	/// <summary>
	/// Ob ein anderes Objekt rechts von diesem Objekt ist.
	/// </summary>
	public bool IsLeft(GameObject obj){
		return ! IsRight(obj);
	}
	/// <summary>
	/// Ob ein anderes Objekt links von diesem Objekt ist.
	/// </summary>
	public bool IsLeft(Collider obj){
		return ! IsRight(obj);
	}
	/// <summary>
	/// Ob ein anderes Objekt links von diesem Objekt ist.
	/// </summary>
	public bool IsLeft(Collision obj){
		return ! IsRight(obj);
	}
	/// <summary>
	/// Ob ein anderes Objekt rechts von diesem Objekt ist.
	/// </summary>
	public bool IsLeft(GeneralObject obj){
		return ! IsRight(obj);
	}
	
	// Oben
	
	/// <summary>
	/// Ob die Position über diesem Objekt ist.
	/// </summary>
	public bool IsOver(Vector3 pos){
		return Vector3.Dot((pos - Pos), Vector3.up) > 0.0f;
	}
	/// <summary>
	/// Ob ein anderes Objekt über diesem Objekt ist.
	/// </summary>
	public bool IsOver(GameObject obj){
		return IsOver(obj.collider.bounds.center);
	}
	/// <summary>
	/// Ob ein anderes Objekt über diesem Objekt ist.
	/// </summary>
	public bool IsOver(Collider obj){
		return IsOver(obj.gameObject);
	}
	/// <summary>
	/// Ob ein anderes Objekt über diesem Objekt ist.
	/// </summary>
	public bool IsOver(Collision obj){
		return IsOver(obj.gameObject);
	}
	/// <summary>
	/// Ob ein anderes Objekt über diesem Objekt ist.
	/// </summary>
	public bool IsOver(GeneralObject obj){
		return IsOver(obj.Pos);
	}
	
	// Unten
	
	/// <summary>
	/// Ob die Position unter diesem Objekt ist.
	/// </summary>
	public bool IsUnder(Vector3 pos){
		return ! IsOver(pos);
	}
	/// <summary>
	/// Ob ein anderes Objekt unter diesem Objekt ist.
	/// </summary>
	public bool IsUnder(GameObject obj){
		return ! IsOver(obj);
	}
	/// <summary>
	/// Ob ein anderes Objekt unter diesem Objekt ist.
	/// </summary>
	public bool IsUnder(Collider obj){
		return ! IsOver(obj);
	}
	/// <summary>
	/// Ob ein anderes Objekt unter diesem Objekt ist.
	/// </summary>
	public bool IsUnder(Collision obj){
		return ! IsOver(obj);
	}
	/// <summary>
	/// Ob ein anderes Objekt unter diesem Objekt ist.
	/// </summary>
	public bool IsUnder(GeneralObject obj){
		return ! IsOver(obj);
	}
	
	// Entfernung
	
	/// <summary>
	/// Entfernung dieses Objektes zu einer anderen Position
	/// </summary>
	/// <returns>
	/// Die absolute Distanz zur Position
	/// </returns>
	/// <param name='obj'>
	/// Die Position zu der die Distanz ermittelt werden soll
	/// </param>
	public float DistanceTo(Vector3 pos){
		return Mathf.Abs(Vector3.Distance(Pos, pos));
	}
	/// <summary>
	/// Entfernung dieses Objektes zu einem anderem Objekt
	/// </summary>
	/// <returns>
	/// Die absolute Distanz zum Objekt
	/// </returns>
	/// <param name='obj'>
	/// Das Objekt zu dem die Distanz ermittelt werden soll
	/// </param>
	public float DistanceTo(GameObject obj){
		return DistanceTo(obj.collider.bounds.center);
	}
	/// <summary>
	/// Entfernung dieses Objektes zu einem anderem Objekt
	/// </summary>
	/// <returns>
	/// Die absolute Distanz zum Objekt
	/// </returns>
	/// <param name='obj'>
	/// Das Objekt zu dem die Distanz ermittelt werden soll
	/// </param>
	public float DistanceTo(Collider obj){
		return DistanceTo(obj.gameObject);
	}
	/// <summary>
	/// Entfernung dieses Objektes zu einem anderem Objekt
	/// </summary>
	/// <returns>
	/// Die absolute Distanz zum Objekt
	/// </returns>
	/// <param name='obj'>
	/// Das Objekt zu dem die Distanz ermittelt werden soll
	/// </param>
	public float DistanceTo(Collision obj){
		return DistanceTo(obj.gameObject);
	}
	/// <summary>
	/// Entfernung dieses Objektes zu einem anderem Objekt
	/// </summary>
	/// <returns>
	/// Die absolute Distanz zum Objekt
	/// </returns>
	/// <param name='obj'>
	/// Das Objekt zu dem die Distanz ermittelt werden soll
	/// </param>
	public float DistanceTo(GeneralObject obj){
		return DistanceTo(obj.Pos);
	}
	
	// Line Of Sight
	
	/// <summary>
	/// Ob die Position für dieses Objekt sichtbar ist (Line of Sight)
	/// </summary>
	/// <returns>
	/// false: wenn zw. Objekt und Ziel eine Wand oder Platform ist
	/// </returns>
	/// <param name='pos'>
	/// Die Position zu der LoS geprüft werden soll
	/// </param>
	public bool LineOfSight(Vector3 pos){
		return ! Linecast(Pos, pos, Layer.Level);
	}
	/// <summary>
	/// Ob das Objekt für dieses Objekt sichtbar ist (Line of Sight).
	/// </summary>
	/// <returns>
	/// false: wenn zw. Objekt und Ziel eine Wand oder Platform ist
	/// </returns>
	/// <param name='obj'>
	/// Das Objekt zu dem LoS geprüft werden soll
	/// </param>
	public bool LineOfSight(GameObject obj){
		RaycastHit hit; //wenn kollision, dann steht hier womit
		//Ob etwas zwischen den Objekten ist
		if(Linecast(Pos, obj.collider.bounds.center, out hit, Layer.Level))
			//ob das getroffene das gewünschte Objekt ist
			return hit.collider.gameObject == obj;
		//nichts zwischen den Objekten
		return true;
	}
	/// <summary>
	/// Ob das Objekt für dieses Objekt sichtbar ist (Line of Sight).
	/// </summary>
	/// <returns>
	/// false: wenn zw. Objekt und Ziel eine Wand oder Platform ist
	/// </returns>
	/// <param name='obj'>
	/// Das Objekt zu dem LoS geprüft werden soll
	/// </param>
	public bool LineOfSight(GeneralObject obj){
		return LineOfSight(obj);
	}
	
	
	
	// Trefferpunkte anderer Objekte verändern
	
	// Schaden
	
	/// <summary>
	/// Einem anderen Objekt Schaden verursachen
	/// </summary>
	/// <param name='other'>
	/// Das andere Objekt das Schaden bekommen soll
	/// </param>
	/// <param name='damage'>
	/// Der Schadenswert
	/// </param>
	public void DoDamage(GameObject other, int damage){
		Vector3 dmg = (other.collider.bounds.center - Pos).normalized * damage;
		other.SendMessage("ApplyDamage", dmg, SendMessageOptions.DontRequireReceiver);
	}
	/// <summary>
	/// Einem anderen Objekt Schaden verursachen
	/// </summary>
	/// <param name='other'>
	/// Das andere Objekt das Schaden bekommen soll
	/// </param>
	/// <param name='damage'>
	/// Der Schadenswert
	/// </param>
	public void DoDamage(Collider other, int damage){
		DoDamage(other.gameObject, damage);
	}
	/// <summary>
	/// Einem anderen Objekt Schaden verursachen
	/// </summary>
	/// <param name='other'>
	/// Das andere Objekt das Schaden bekommen soll
	/// </param>
	/// <param name='damage'>
	/// Der Schadenswert
	/// </param>
	public void DoDamage(Collision other, int damage){
		DoDamage(other.gameObject, damage);
	}
	/// <summary>
	/// Einem anderen Objekt Schaden verursachen
	/// </summary>
	/// <param name='other'>
	/// Das andere Objekt das Schaden bekommen soll
	/// </param>
	/// <param name='damage'>
	/// Der Schadenswert
	/// </param>
	public void DoDamage(GeneralObject other, int damage){
		DoDamage(other.gameObject, damage);
	}
	
	// Heilung
	
	/// <summary>
	/// Einem anderem Objekt Lebenspunkte heilen
	/// </summary>
	/// <param name='other'>
	/// Das Spielobjekt das Lebenspunkte bekommen soll
	/// </param>
	/// <param name='hp'>
	/// Anzahl Lebenspunkte um die das Objekt geheilt werden soll
	/// </param>
	public void DoHeal(GameObject other, int hp){
		other.SendMessage("ApplyHealth", hp, SendMessageOptions.DontRequireReceiver);
	}
	/// <summary>
	/// Einem anderem Objekt Lebenspunkte heilen
	/// </summary>
	/// <param name='other'>
	/// Das Spielobjekt das Lebenspunkte bekommen soll
	/// </param>
	/// <param name='hp'>
	/// Anzahl Lebenspunkte um die das Objekt geheilt werden soll
	/// </param>
	public void DoHeal(Collider other, int hp){
		DoHeal(other.gameObject, hp);
	}
	/// <summary>
	/// Einem anderem Objekt Lebenspunkte heilen
	/// </summary>
	/// <param name='other'>
	/// Das Spielobjekt das Lebenspunkte bekommen soll
	/// </param>
	/// <param name='hp'>
	/// Anzahl Lebenspunkte um die das Objekt geheilt werden soll
	/// </param>
	public void DoHeal(Collision other, int hp){
		DoHeal(other.gameObject, hp);
	}
	/// <summary>
	/// Einem anderem Objekt Lebenspunkte heilen
	/// </summary>
	/// <param name='other'>
	/// Das Spielobjekt das Lebenspunkte bekommen soll
	/// </param>
	/// <param name='hp'>
	/// Anzahl Lebenspunkte um die das Objekt geheilt werden soll
	/// </param>
	public void DoHeal(GeneralObject other, int hp){
		DoHeal(other.gameObject, hp);
	}
	
	
	
	// Instantiate
	
	/// <summary>
	/// Instanziiert ein neues GameObject an einer gewünschten Position
	/// </summary>
	/// <param name='obj'>
	/// Der Name des Prefabs das erstellt werden soll
	/// </param>
	/// <param name='pos'>
	/// Position an der das neue Objekt sein soll.
	/// </param>
	public GameObject Instantiate(Object obj, Vector3 pos){
		return (GameObject) Object.Instantiate(obj, pos, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f) );
	}
	
	/// <summary>
	/// Instanziiert ein neues GameObject
	/// </summary>
	/// <param name='obj'>
	/// Der Name des Prefabs das erstellt werden soll
	/// </param>
	public new GameObject Instantiate(Object obj){
		return Instantiate(obj, Pos);
	}
	
	/// <summary>
	/// Instanziiert ein neues GameObject von einem Prefab-Namen an einer gewünschten Position.
	/// </summary>
	/// <param name='obj'>
	/// Der Name des Prefabs das erstellt werden soll
	/// </param>
	/// <param name='pos'>
	/// Position an der das neue Objekt sein soll.
	/// </param>
	public GameObject Instantiate(string name, Vector3 pos){
		return Instantiate(Resources.Load(name), pos);
	}
	
	/// <summary>
	/// Instanziiert ein neues GameObject von einem Prefab-Namen.
	/// </summary>
	/// <param name='obj'>
	/// Der Name des Prefabs das erstellt werden soll
	/// </param>
	public GameObject Instantiate(string name){
		return Instantiate(name, Pos);
	}
	
	
	
}