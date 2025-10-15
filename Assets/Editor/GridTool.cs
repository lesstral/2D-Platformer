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

    public List<GameObject> lastCreatedGrid = new List<GameObject>();
    private List<List<Vector3>> positionList = new List<List<Vector3>>();
    private List<GameObject> rowPrefabs = new List<GameObject>();
    private GameObject defaultPrefab;
    private GameObject gridParent;
    private const float MinGridSize = 0.16f;
    private const float MaxGridSize = 5f;

    private Color gridColour = Color.red;
    private bool isDragging = false;

    private float gridCellSize = 0.32f;

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
        defaultPrefab = (GameObject)EditorGUILayout.ObjectField(
            "MonoFill prefab:",
            defaultPrefab,
            typeof(GameObject),
            true
        );

        if (GUILayout.Button("Undo"))
        {
            UndoGrid();
        }
        if (GUILayout.Button("PlacePrefabs"))
        {
            if (startPos != Vector3.zero && endPos != Vector3.zero)
            {
                PlaceGridObjects(positionList);
                startPos = Vector3.zero;
                endPos = Vector3.zero;
                rowPrefabs.Clear();
            }
        }
        GUILayout.Label("Prefabs by row:", EditorStyles.boldLabel);
        for (int i = 0; i < positionList.Count; i++)
        {
            rowPrefabs[i] = (GameObject)EditorGUILayout.ObjectField(
                new GUIContent("Row " + (i + 1) + " Prefab"),
                rowPrefabs[i],
                typeof(GameObject),
                false
            );
        }

    }

    private void OnSceneGUI(SceneView sceneView)
    {
        Event e = Event.current;

        if (e.type == EventType.MouseDown && e.button == 0)
        {
            positionList.Clear();
            lastCreatedGrid.Clear();
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
            endPos.z = 0;
        }

        if (e.type == EventType.MouseUp && e.button == 0)
        {
            Debug.Log("Mouse Up");
            isDragging = false;
            GeneratePositions(startPos, endPos, positionList);
            if (defaultPrefab == null)
            {
                GeneratePrefabList();
            }
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
    private void PlaceGridObjects(List<List<Vector3>> posList)
    {

        lastCreatedGrid.Clear();
        GameObject gridParent = new GameObject("GridParent");
        this.gridParent = gridParent;
        int i = 1;
        int j = 0;
        foreach (List<Vector3> row in posList)
        {
            GameObject rowParent = new GameObject("RowParent" + i);
            rowParent.transform.position = row[0];
            lastCreatedGrid.Add(rowParent);
            GameObject currentPrefab = defaultPrefab == null ? rowPrefabs[j] : defaultPrefab;
            i++;
            j++;
            if (currentPrefab == null)
            {
                Debug.LogWarning($"Missing prefab for row {j + 1}, skipping row");
                continue;
            }

            foreach (Vector3 pos in row)
            {
                GameObject block = Instantiate(currentPrefab, pos, Quaternion.identity);
                block.transform.SetParent(rowParent.transform, true);

            }
            rowParent.transform.SetParent(gridParent.transform, true);
        }
    }

    private float SnapTo(float value, float snap)
    {
        return Mathf.Round(value / snap) * snap;
    }
    private void GeneratePrefabList()
    {
        if (rowPrefabs.Count != positionList.Count)
        {
            rowPrefabs.Clear();
            for (int i = 0; i < positionList.Count; i++)
            {
                rowPrefabs.Add(null);
            }
        }
    }
    private void UndoGrid()
    {
        if (gridParent != null)
        {
            Undo.DestroyObjectImmediate(gridParent);  // Handles children recursively via Undo
            gridParent = null;
        }
        startPos = endPos = Vector3.zero;
        ClearLists();
    }
    private void ClearLists()
    {
        positionList.Clear();
        rowPrefabs.Clear();
        lastCreatedGrid.Clear();
    }
}

