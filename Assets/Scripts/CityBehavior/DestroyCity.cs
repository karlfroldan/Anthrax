using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCity : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer spriteRenderer;
    public Sprite noShieldSprite;
    public Sprite destroyedSprite;

    void Start()
    {
        // do nothing
    }

    // Update is called once per frame
    public void ShieldDestroyed()
    {
        Debug.Log("Destroying shield");
        Debug.Log("noShieldSprite: " + noShieldSprite.name);
        spriteRenderer.sprite = noShieldSprite;
    }

    public void CityDestroyed()
    {
        spriteRenderer.sprite = destroyedSprite;
    }
}
