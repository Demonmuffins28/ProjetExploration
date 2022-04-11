using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Light : MonoBehaviour
{
    /**
     * 1 = Vert
     * 2 = Rouge
     * 3 = Jaune
     */
    public int couleurLumiere;

    SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        switch (couleurLumiere)
        {
            case 1:
                sprite.color = Color.green;
                break;
            case 2:
                sprite.color = Color.red;
                break;
            case 3:
                sprite.color = Color.yellow;
                break;
            default:
                break;
        }
    }
}
