using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSprite : MonoBehaviour
{
    public Sprite tree0Sprite;
    public Sprite tree1Sprite;

    List<Sprite> treeSpriteList = new List<Sprite>();

    private void Awake()
    {
        treeSpriteList.Add(tree0Sprite);
        treeSpriteList.Add(tree1Sprite);
    }

    private void Update()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = treeSpriteList[GameManager.instance.treeGrowStatus];
    }
}
