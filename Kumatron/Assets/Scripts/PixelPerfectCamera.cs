using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectCamera : MonoBehaviour
{

    // Use this for initialization
    [Range(1, 4)]
    public float pixelScale = 0.3f;

    private int pixelsPerUnit = 100;
    private float halfScreen = 0.5f;

    private Camera _camera;

    void Awake()
    {
        if (_camera == null)
        {
            _camera = GetComponent<Camera>();
            _camera.orthographic = true;
        }
    }
    void Update()
    {
        _camera.orthographicSize = Screen.height * ((halfScreen / pixelsPerUnit) / pixelScale);
    }
}
