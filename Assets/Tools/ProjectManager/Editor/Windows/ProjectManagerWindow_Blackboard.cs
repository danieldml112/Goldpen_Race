using UnityEditor;
using UnityEngine;

public class ProjectManagerWindow_Blackboard : ProjectManager.ProjectManagerWindow
{

	GameObject blackboardGO;

	public override void OnFocus()
	{
		blackboardGO = Resources.Load<GameObject>("Blackboard");
	}

	public override void OnGUI()
	{
		if (blackboardGO == null)
			GUI.enabled = false;
		
		if (GUILayout.Button("Blackboard"))
		{
			Selection.activeGameObject = blackboardGO;
			Repaint();
		}
		
		GUI.enabled = true;
	}
	
}
