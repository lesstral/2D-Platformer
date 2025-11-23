using UnityEngine;
using System.Collections.Generic;
using Unity.Cinemachine;

[System.Serializable]
public class ParallaxLayer
{
    public List<Transform> objects = new List<Transform>();
    [Tooltip("Parallax multiplier, 0 = no movement, 1 = full movement with camera")]
    public float parallaxFactor = 0.5f;
}
public class ParallaxBackground : MonoBehaviour

{
    [Header("Camera Settings")]
    public Transform cameraTransform;
    private Vector3 previousCameraPosition;

    [Header("Layers")]
    public ParallaxLayer sky = new ParallaxLayer { parallaxFactor = 0.1f };
    public ParallaxLayer clouds = new ParallaxLayer { parallaxFactor = 0.3f };
    public ParallaxLayer background = new ParallaxLayer { parallaxFactor = 0.5f };
    public ParallaxLayer objects = new ParallaxLayer { parallaxFactor = 0.6f };
    public ParallaxLayer foreground = new ParallaxLayer { parallaxFactor = 0.8f };

    [Header("Optional")]
    public bool verticalParallax = false;

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
        previousCameraPosition = cameraTransform.position;
    }

    void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - previousCameraPosition;

        ApplyParallax(sky, deltaMovement);
        ApplyParallax(clouds, deltaMovement);
        ApplyParallax(background, deltaMovement);
        ApplyParallax(objects, deltaMovement);
        ApplyParallax(foreground, deltaMovement);

        previousCameraPosition = cameraTransform.position;
    }

    void ApplyParallax(ParallaxLayer layer, Vector3 deltaMovement)
    {
        if (layer.objects == null) return;

        foreach (Transform obj in layer.objects)
        {
            if (obj == null) continue;

            Vector3 movement = new Vector3(deltaMovement.x * layer.parallaxFactor,
                                           verticalParallax ? deltaMovement.y * layer.parallaxFactor : 0,
                                           0);
            obj.position += movement;
        }
    }
}
