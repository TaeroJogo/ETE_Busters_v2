using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderUpdater : MonoBehaviour
{

    //make a variable that can be two types, CapsuleCollider2D and BoxCollider2D


    Sprite currentSprite;
    CapsuleCollider2D capsuleColl;
    BoxCollider2D boxColl;
    SpriteRenderer spr;

    void Start()
    {
        capsuleColl = gameObject.GetComponentInChildren<CapsuleCollider2D>();
        boxColl = gameObject.GetComponentInChildren<BoxCollider2D>();

        spr = gameObject.GetComponentInChildren<SpriteRenderer>();
        UpdateCollider();
    }

    void Update()
    {
        if (currentSprite != spr.sprite)
        {
            currentSprite = spr.sprite;
            UpdateCollider();
        }
    }

    void UpdateCollider()
    {
        if (capsuleColl != null)
        {
            capsuleColl.size = spr.sprite.bounds.size;
            capsuleColl.offset = spr.sprite.bounds.center;
        }
        else if (boxColl != null)
        {
            boxColl.size = spr.sprite.bounds.size;
            boxColl.offset = spr.sprite.bounds.center;
        }
    }
}
