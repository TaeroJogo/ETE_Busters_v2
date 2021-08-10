using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderUpdater : MonoBehaviour
{
    
    Sprite currentSprite;
    BoxCollider2D coll;
    SpriteRenderer spr;

    void Start() {
        coll = gameObject.GetComponentInChildren<BoxCollider2D>();
        spr = gameObject.GetComponentInChildren<SpriteRenderer>();
        UpdateCollider();
    }

    void Update()
    {
	    if(currentSprite != spr.sprite)
	    {
		    currentSprite = spr.sprite;
		    UpdateCollider();
	    }
    }

    void UpdateCollider()
    {
	    coll.size = spr.sprite.bounds.size;
	    coll.offset = spr.sprite.bounds.center;

    }
}
