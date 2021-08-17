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
        //como essa funcao e utilizada em todos os objetos que se movem, ele tenta pegar ou um boxCollider2D ou um capsuleCollider2D
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


    //essa funcao e chamada quando o sprite do mudar, arrumando o boxCollider2D e o capsuleCollider2D para o tamanho do sprite
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
