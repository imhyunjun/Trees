using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSprite : MonoBehaviour
{
    public Sprite tree0Sprite;
    public Sprite tree1Sprite;

    private SpriteRenderer spriteRenderer;
    private List<Sprite> treeSpriteList = new List<Sprite>();

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        treeSpriteList.Add(tree0Sprite);
        treeSpriteList.Add(tree1Sprite);
    }

    private void Update()
    {
        //gameObject.GetComponent<SpriteRenderer>().sprite = treeSpriteList[GameManager.Instance.treeGrowStatus];
        spriteRenderer.sprite = treeSpriteList[GameManager.instance.treeGrowStatus];
    }
}
