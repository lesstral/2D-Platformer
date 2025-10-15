using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEditor.SearchService;
using Unity.VisualScripting;
using NUnit.Framework.Internal;
using UnityEngine.U2D;
using PlasticPipe.PlasticProtocol.Messages;
using UnityEngine.UIElements;


public class GridTool : EditorWindow
{

    private Vector3 startPos;
    private Vector3 endPos;

    public List<GameObject> test = new List<GameObject>();
    private List<List<Vector3>> positionList = new List<List<Vector3>>();
    private bool isDragging = false;
    private float gridCellSize = 0.32f;
    private Color gridColour = Color.red;
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
        GUILayout.Label("Keep prefab empty to have layer-by-layer filling");
        testPrefab = (GameObject)EditorGUILayout.ObjectField(
            "MonoFill prefab:",         // label
            testPrefab,        // current value
            typeof(GameObject),    // type allowed
            true                   // allow scene objects
        );

        if (GUILayout.Button("Undo"))
        {
            if (test.Count > 0)
            {
                foreach (GameObject gameObject in test)
                {

                    DestroyImmediate(gameObject);
                }
            }
            startPos = Vector3.zero;
            endPos = Vector3.zero;
            test.Clear();
            positionList.Clear();
        }
        if (GUILayout.Button("Generate"))
        {
            GenerateGrid(positionList);
            startPos = Vector3.zero;
            endPos = Vector3.zero;
        }
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        Event e = Event.current;

        if (e.type == EventType.MouseDown && e.button == 0)
        {
            positionList.Clear();
            test.Clear();
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
            GeneratePositions(startPos, endPos, positionList);
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
    private void GeneratePositions(Vector3 corner1, Vector3 corner2, List<List<Vector3>> posList)
    {
        posList.Clear();
        corner1 = new Vector3(SnapTo(corner1.x, gridCellSize), SnapTo(corner1.y, gridCellSize), 0);
        corner2 = new Vector3(SnapTo(corner2.x, gridCellSize), SnapTo(corner2.y, gridCellSize), 0);

        Vector3 centerOffset = new Vector3(gridCellSize / 2f, gridCellSize / 2f, 0);

        Debug.Log("generated");
        Vector3 min = Vector3.Min(corner1, corner2);
        Vector3 max = Vector3.Max(corner1, corner2);

        int columnCount = Mathf.RoundToInt((max.x - min.x) / gridCellSize);
        int rowsCount = Mathf.RoundToInt((max.y - min.y) / gridCellSize);

        for (int y = 0; y < rowsCount; y++)
        {
            List<Vector3> columnPosList = new List<Vector3>();

            for (int x = 0; x < columnCount; x++)
            {
                Vector3 pos = new Vector3(min.x + x * gridCellSize, max.y - (y + 1) * gridCellSize, 0) + centerOffset;
                columnPosList.Add(pos);

            }
            posList.Add(columnPosList);
        }
    }
    private void GenerateGrid(List<List<Vector3>> posList)
    {

        test.Clear();
        GameObject gridParent = new GameObject("GridParent");
        int i = 1;
        foreach (List<Vector3> row in posList)
        {
            GameObject rowParent = new GameObject("RowParent" + i);
            rowParent.transform.position = row[0];
            test.Add(rowParent);
            i++;
            foreach (Vector3 pos in row)
            {
                GameObject block = Instantiate(testPrefab, pos, Quaternion.identity);
                block.transform.SetParent(rowParent.transform, true);

            }
            rowParent.transform.SetParent(gridParent.transform, true);
        }
    }

    private float SnapTo(float value, float snap)
    {
        return Mathf.Round(value / snap) * snap;
    }

}

