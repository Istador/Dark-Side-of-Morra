// generate a prefab from selection

#pragma strict

@MenuItem("Project Tools / Make Prefab #&p")

static function MakePrefab()
{
	// selection from the scene view
	var selectedObjects : GameObject[] = Selection.gameObjects;

	// loop throug our selection
	for (var go : GameObject in selectedObjects)
	{
		// store the name of our selection
		var name : String = go.name;
		// create the path for the prefab
		var localPath : String = "Assets/" + name + ".prefab";
		// check for object in project
		if (AssetDatabase.LoadAssetAtPath(localPath, GameObject))
		{
			// check for user choice
			if (EditorUtility.DisplayDialog("Prefab already exists!", "Do you want to overwrite the existing prefab?", "Yes", "No"))
			{
				// creating a new prefab
				createNew(go, localPath);
			}
		}
		else
		{
			// creating a new prefab
			createNew(go, localPath);
		}
	}
}

// create a new prefeb
static function createNew(selectedObject : GameObject, localPath : String)
{
	// store prefab
	var prefab : UnityEngine.Object = PrefabUtility.CreateEmptyPrefab(localPath);
	// set prefab to selected prefab
	PrefabUtility.ReplacePrefab(selectedObject, prefab);
	// remove the selected object
	DestroyImmediate(selectedObject);
	// replace object with prefab
	var clone : GameObject = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
	// refreshing asset database
	AssetDatabase.Refresh();
}