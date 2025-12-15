using System;
using PlasticGui;
using UnityEditor;
using UnityEngine;

public class PlatformTool : EditorWindow
{
    Vector3 startPos;
    Vector3 endPos;
    Vector3 snappedPos;
    [SerializeField] GameObject platformPrefab;
    [SerializeField] float platformSpeed;
    [SerializeField] float platformPause;
    [SerializeField] bool startsInRandomDirection = false;
    [SerializeField] bool startsTowardsA = true;
    private bool horizontalMovement;
    bool isPlacing = false;
    private Color gridColour = Color.red;
    [MenuItem("Tools/PlatformTool")]
    private static void ShowWindow()
    {
        var window = GetWindow<PlatformTool>();
        window.titleContent = new GUIContent("PlatformTool");
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
        platformPause = EditorGUILayout.FloatField("Platform pause duration", platformPause);
        platformSpeed = EditorGUILayout.FloatField("Platform speed", platformSpeed);
        platformPrefab = (GameObject)EditorGUILayout.ObjectField(
            "Platform prefab:",
            platformPrefab,
            typeof(GameObject),
            true
        );
        startsInRandomDirection = EditorGUILayout.Toggle("Starts in Random Direction", startsInRandomDirection);
        startsTowardsA = EditorGUILayout.Toggle("Starts towards point A", startsTowardsA);
        startPos = EditorGUILayout.Vector3Field("Point A coordinates", startPos);
        endPos = EditorGUILayout.Vector3Field("Point B coordinates", endPos);
        if (GUILayout.Button("PlacePlatform"))
        {
            if (platformPrefab == null) Debug.LogError("Platform prefab is not attached");
            PlacePlatform();
        }
    }
    private void OnSceneGUI(SceneView sceneView)
    {
        Event e = Event.current;
        if (!isPlacing && e.type == EventType.MouseDown && e.button == 0)
        {
            startPos = HandleUtility.GUIPointToWorldRay(e.mousePosition).origin;
            startPos.z = 0;
            e.Use();
            isPlacing = true;
        }
        if (isPlacing)
        {
            endPos = HandleUtility.GUIPointToWorldRay(e.mousePosition).origin;
            endPos.z = 0;
        }
        if (isPlacing && e.type == EventType.MouseDown && e.button == 0)
        {
            isPlacing = false;
            e.Use();
        }
        if (startPos != Vector3.zero && endPos != Vector3.zero)
        {
            SetMovementDirection(startPos, endPos);
            endPos = SnapPoints(startPos, endPos);
            DrawLine(startPos, endPos);
            SceneView.RepaintAll();
        }
    }
    private void DrawLine(Vector3 point1, Vector3 point2)
    {
        Handles.color = gridColour;



        Handles.DrawLine(point1, point2);

    }
    private Vector3 SnapPoints(Vector3 point1, Vector3 point2)
    {


        if (horizontalMovement)
        {
            point2.y = point1.y;

        }
        else
        {
            point2.x = point1.x;

        }
        return point2;
    }
    private void SetMovementDirection(Vector3 point1, Vector3 point2)
    {
        horizontalMovement = Mathf.Abs(point2.x - point1.x) > Mathf.Abs(point2.y - point1.y);
    }
    private void PlacePlatform()
    {
        GameObject newPlatformGO = (GameObject)PrefabUtility.InstantiatePrefab(platformPrefab);
        newPlatformGO.transform.position = (startPos + endPos) / 2;
        Platform newPlatform = newPlatformGO.GetComponent<Platform>();
        newPlatform.SetMovementType(horizontalMovement);
        float pointA;
        float pointB;
        if (horizontalMovement)
        {
            pointA = startPos.x;
            pointB = endPos.x;

        }
        else
        {
            pointA = startPos.y;
            pointB = endPos.y;

        }
        newPlatform.SetStartingDirection(startsInRandomDirection, startsTowardsA);
        newPlatform.SetMovementPoints(pointA, pointB);
        newPlatform.SetPauseDuration(platformPause);
        newPlatform.SetMovementSpeed(platformSpeed);

    }
}

