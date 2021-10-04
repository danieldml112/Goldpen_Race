using UnityEditor;

public class ProjectManager : EditorWindow
{

    public class ProjectManagerWindow
    {
        public new void Repaint()
        {
            ProjectManager.GetWindow().Repaint();
        }
        public virtual void OnFocus(){}
        public virtual void OnGUI(){}
        public virtual void OnSelectionChange(){}
    }

    static ProjectManager window;
    static ProjectManagerWindow[][] windows =
    {
        new ProjectManagerWindow[] {new ProjectManagerWindow_Blackboard()}, 
        new ProjectManagerWindow[] {new ProjectManagerWindow_Scenes()}
    };
    
    [MenuItem("Tools/Project Manager")]
    public static void ShowWindow()
    {
        window = GetWindow(typeof(ProjectManager)) as ProjectManager;
    }

    public static ProjectManager GetWindow()
    {
        if (window == null)
            ShowWindow();
        
        return window;
    }

    void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        {
            foreach (ProjectManagerWindow[] _windows in windows)
            {
                EditorGUILayout.BeginHorizontal();
                {
                    foreach (ProjectManagerWindow _window in _windows)
                        _window.OnGUI();
                }
                EditorGUILayout.EndHorizontal();
            }
        }
        EditorGUILayout.EndVertical();
    }

    public void Repaint()
    {
        // foreach (ProjectManagerWindow[] _windows in windows)
        //     foreach (ProjectManagerWindow _window in _windows)
        //         _window.Repaint();
    }
    
    public void OnFocus()
    {
        foreach (ProjectManagerWindow[] _windows in windows)
            foreach (ProjectManagerWindow _window in _windows)
                _window.OnFocus();
    }
    
    public void OnSelectionChange()
    {
        foreach (ProjectManagerWindow[] _windows in windows)
            foreach (ProjectManagerWindow _window in _windows)
                _window.OnSelectionChange();
    }
    
}
