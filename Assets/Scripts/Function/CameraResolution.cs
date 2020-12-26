using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    private Rect cameraRect;
    private Camera cam;
    private float ratio;

    [SerializeField] Vector2 wantScale;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        cameraRect = cam.rect;
                             
        //16:10과 차이가 얼마나 나는지 비교
        ratio = ((float)Screen.width / Screen.height) / ((float)wantScale.x / wantScale.y);
        Debug.LogError(ratio);

        if (ratio > 1f)
        {
            cameraRect.width = 1 / ratio;
            cameraRect.x = (1 - 1 / ratio) / 2f;
        }
        else if (ratio < 1f)
        {
            cameraRect.height = ratio;
            cameraRect.y = (1 - ratio) / 2f;
        }
        cam.rect = cameraRect;
    }
}
