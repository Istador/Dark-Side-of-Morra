// this code is mostly from http://answers.unity3d.com/questions/8480/how-to-scrip-a-saveload-game-option.html

using UnityEngine;

using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using System;
using System.Runtime.Serialization;
using System.Reflection;

// === This is the info container class ===
[Serializable ()]
public class SaveData : ISerializable
{

	// === Values ===
	// Edit these during gameplay
	public static int levelReached = 2;
	// === /Values ===
	
	// The default constructor. Included for when we call it during Save() and Load()
	public SaveData (){}

	// This constructor is called automatically by the parent class, ISerializable
	// We get to custom-implement the serialization process here
	public SaveData (SerializationInfo info, StreamingContext ctxt)
	{
		// Get the values from info and assign them to the appropriate properties. Make sure to cast each variable.
		// Do this for each var defined in the Values section above
		try{
			//stürzt ab wenn levelReached in der Datei noch nicht vorhanden ist
			levelReached = (int)info.GetValue("levelReached", typeof(int));
		} catch(SerializationException){}
	}

	// Required by the ISerializable class to be properly serialized. This is called automatically
	public void GetObjectData (SerializationInfo info, StreamingContext ctxt)
	{
		// Repeat this for each var defined in the Values section
		info.AddValue("levelReached", levelReached);
	}
}

// === This is the class that will be accessed from scripts ===
public class SaveLoad
{
	
	private static bool loaded = false;
	

	public static string currentFilePath = "PlayerProgress.game";    // Edit this for different save files
	
	
	
	// Call this to write data
	public static void Save () { // Overloaded
		Save (currentFilePath);
	}
	
	
	
	private static void Save (string filePath)
	{
		SaveData data = new SaveData();

		Stream stream = File.Open(filePath, FileMode.Create);
		BinaryFormatter bformatter = new BinaryFormatter();
		bformatter.Binder = new VersionDeserializationBinder(); 
		bformatter.Serialize(stream, data);
		stream.Close();
	}
 
	
	
	// Call this to load from a file into "data"
	public static void Load ()  {  // Overloaded
		Load(currentFilePath); 
	}
	
	
	
	private static void Load (string filePath) 
	{
		//nur ein einziges mal laden können
		if(!loaded){
			loaded = true;
			 
			try{
				/*SaveData data = new SaveData();*/
				//stürzt ab wenn die Datei noch nicht vorhanden ist
				Stream stream = File.Open(filePath, FileMode.Open);
				BinaryFormatter bformatter = new BinaryFormatter();
				bformatter.Binder = new VersionDeserializationBinder(); 
				/*data = (SaveData)*/bformatter.Deserialize(stream);
				stream.Close();
			} catch(FileNotFoundException){
				//Datei erstellen
				Save(filePath);
			}
		}
	}

}

// === This is required to guarantee a fixed serialization assembly name, which Unity likes to randomize on each compile
// Do not change this
public sealed class VersionDeserializationBinder : SerializationBinder 
{ 
	public override Type BindToType( string assemblyName, string typeName )
	{ 
		if ( !string.IsNullOrEmpty( assemblyName ) && !string.IsNullOrEmpty( typeName ) ) 
		{ 
			Type typeToDeserialize = null; 

			assemblyName = Assembly.GetExecutingAssembly().FullName; 

			// The following line of code returns the type. 
			typeToDeserialize = Type.GetType( String.Format( "{0}, {1}", typeName, assemblyName ) ); 

			return typeToDeserialize; 
		}

		return null; 
	}
}