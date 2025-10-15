using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEditor.SearchService;
using Unity.VisualScripting;
using NUnit.Framework.Internal;
using UnityEngine.U2D;
[System.Serializable]
public class GridRow
{
    public GameObject prefab;
    [HideInInspector]
    public List<Vector3> cellCentersList = new();
}
public class GridTool : EditorWindow
{

    private Vector3 startPos;
    private Vector3 endPos;
    public List<GridRow> rows = new List<GridRow>();
    public List<GameObject> test = new List<GameObject>();
    private bool isDragging = false;
    private float gridCellSize = 0.32f;
    private Color gridColour = Color.green;
    private SerializedObject serializedObject;
    private GameObject testPrefab;
    [MenuItem("Tools/GridTool")]

    private static void ShowWindow()
    {
        var window = GetWindow<GridTool>();
        window.titleContent = new GUIContent("GridTool");
        window.Show();
    }
    private void OnEnable()
    {
        serializedObject = new SerializedObject(this);
        SceneView.duringSceneGui += OnSceneGUI;




    }

    private void OnDisable()
    {

        SceneView.duringSceneGui -= OnSceneGUI;
    }
    private void OnGUI()
    {
        gridCellSize = EditorGUILayout.Slider("Grid Size", gridCellSize, 0.16f, 5f);
        gridColour = EditorGUILayout.ColorField("GridColour", gridColour);
        testPrefab = (GameObject)EditorGUILayout.ObjectField(
            "GameObject:",         // label
            testPrefab,        // current value
            typeof(GameObject),    // type allowed
            true                   // allow scene objects
        );
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rows"), true);
        if (GUILayout.Button("Click Me"))
        {

            rows.Clear();
        }
        if (GUILayout.Button("1"))
        {
            GenerateGrid(new Vector3(0, 0, 0), new Vector3(1, 2, 0));
            DrawGrid(new Vector3(0, 0, 0), new Vector3(1, 2, 0));
        }
        if (GUILayout.Button("DELETE"))
        {
            foreach (GameObject gameObject in test)
            {
                DestroyImmediate(gameObject);
            }
        }
        serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        Event e = Event.current;

        if (e.type == EventType.MouseDown && e.button == 0)
        {
            Debug.Log("Mouse Down");
            startPos = HandleUtility.GUIPointToWorldRay(e.mousePosition).origin;
            startPos.z = 0;
            isDragging = true;

            e.Use();
        }

        if (isDragging)
        {
            Debug.Log("Dragging");
            endPos = HandleUtility.GUIPointToWorldRay(e.mousePosition).origin;
        }

        if (e.type == EventType.MouseUp && e.button == 0)
        {
            Debug.Log("Mouse Up");
            isDragging = false;
            GenerateGrid(startPos, endPos);
            e.Use();

        }
        if (startPos != Vector3.zero && endPos != Vector3.zero)
        {
            DrawGrid(startPos, endPos);

            SceneView.RepaintAll();
        }

    }
    private void DrawGrid(Vector3 corner1, Vector3 corner2)
    {
        corner1 = new Vector3(SnapTo(corner1.x, gridCellSize), SnapTo(corner1.y, gridCellSize), 0);
        corner2 = new Vector3(SnapTo(corner2.x, gridCellSize), SnapTo(corner2.y, gridCellSize), 0);
        Handles.color = gridColour;
        Vector3[] pointsList = new Vector3[]
        {
            corner1,                          //upper-left
            new Vector3(corner2.x, corner1.y, 0), //upper-right
            corner2,                            //bottom-right
            new Vector3(corner1.x, corner2.y, 0), //bottom-left
            corner1

        };
        Vector3 min = Vector3.Min(corner1, corner2);
        Vector3 max = Vector3.Max(corner1, corner2);
        // draw vertical and horizontal lines with gridCellSize as an offset
        // until they hit outer lines
        for (float x = min.x; x <= max.x; x += gridCellSize)    // vertical lines
        {
            Handles.DrawLine(new Vector3(x, min.y, 0), new Vector3(x, max.y, 0));
        }
        for (float y = min.y; y <= max.y; y += gridCellSize)    // horizontal lines
        {
            Handles.DrawLine(new Vector3(min.x, y, 0), new Vector3(max.x, y, 0));
        }

        Handles.DrawPolyLine(pointsList);
    }
    private void GenerateGrid(Vector3 corner1, Vector3 corner2)
    {
        rows.Clear();
        corner1 = new Vector3(SnapTo(corner1.x, gridCellSize), SnapTo(corner1.y, gridCellSize), 0);
        corner2 = new Vector3(SnapTo(corner2.x, gridCellSize), SnapTo(corner2.y, gridCellSize), 0);
        corner1 = new Vector3(corner1.x + 0.16f, corner1.y + 0.16f, 0);
        corner2 = new Vector3(corner2.x + 0.16f, corner2.y + 0.16f, 0);

        Debug.Log("generated");
        Vector3 min = Vector3.Min(corner1, corner2);
        Vector3 max = Vector3.Max(corner1, corner2);
        for (float y = min.y; y <= max.y - gridCellSize; y += gridCellSize)
        {
            for (float x = min.x; x <= max.x - gridCellSize; x += gridCellSize)
            {
                test.Add(Instantiate(testPrefab, new Vector3(x, y, 0), Quaternion.identity));
            }
        }

        foreach (GridRow row in rows)
        {
            foreach (Vector3 cell in row.cellCentersList)
            {
                Debug.Log(cell);
            }
        }
    }

    private float SnapTo(float value, float snap)
    {
        return Mathf.Round(value / snap) * snap;
    }

}

