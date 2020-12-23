using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderMirror : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private SpriteRenderer reflected;

    private readonly int width = 123;
    private readonly int height = 324;

    private RenderTexture mRenderTexture;
    private Sprite sprite;

    private void Update()
    {
        if (mRenderTexture == null)
        {
            mRenderTexture = new RenderTexture(width, height, 32);
            cam.targetTexture = mRenderTexture;
            cam.Render();
        }

        if (mRenderTexture != null)
        {
            RenderTexture.active = mRenderTexture;
            Texture2D virtualPhoto = new Texture2D(width, height, TextureFormat.RGBA32, false);
            virtualPhoto.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            virtualPhoto.Apply();

            RenderTexture.active = null;
            cam.targetTexture = null;

            if (sprite != null) Destroy(sprite);

            sprite = Sprite.Create(virtualPhoto, new Rect(Vector2.zero, new Vector2(width, height)), new Vector2(0.5f, 0.5f));
            reflected.sprite = sprite;

            mRenderTexture = null;
        }
    }

}
