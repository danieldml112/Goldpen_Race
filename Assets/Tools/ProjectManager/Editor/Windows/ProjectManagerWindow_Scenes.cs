using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ProjectManagerWindow_Scenes : ProjectManager.ProjectManagerWindow
{

	string[] scenePaths = new string[0];
	Vector2 scrollPosition;
	
	public override void OnFocus()
	{
		scenePaths = new HashSet<string>(EditorBuildSettings.scenes.Select(s => s.path)).ToArray();;
	}

	public override void OnGUI()
	{
		GUILayout.BeginVertical("box");
		{
			GUIStyle labelStyle = new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter};
			GUILayout.Label("Scenes", labelStyle);
			
			scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUIStyle.none, GUIStyle.none);
			foreach (string scenePath in scenePaths)
			{
				if (GUILayout.Button(Path.GetFileNameWithoutExtension(scenePath)))
				{
					if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
					{
						EditorSceneManager.OpenScene(scenePath);
						Repaint();
					}
				}
			}
			GUILayout.EndScrollView();
		}
		GUILayout.EndVertical();
	}
	
}
