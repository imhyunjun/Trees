using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private List<Sprite> treeSpriteList = new List<Sprite>();

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        spriteRenderer.sprite = treeSpriteList[GameManager.instance.treeGrowStatus];
    }
}
