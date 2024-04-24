using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFlashComponent : MonoBehaviour
{
    [SerializeField]
    private float timeBetwenColors = 0.075f;
    Color originalColor = Color.white;
    Color redColor = Color.red;
    
    public IEnumerator FlashRed(float redTime)
    {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>(); ;
        for (float t = 0; t < redTime; t += Time.deltaTime)
        {
            if(spriteRenderer == null)
            {
                yield return null;
            }

            if(spriteRenderer != null  && spriteRenderer.color == redColor)
            {
               
                    spriteRenderer.color = originalColor;

            }
            else
            {
                if (spriteRenderer != null)
                {
                    spriteRenderer.color = redColor;
                }
            }
            t += timeBetwenColors;
            yield return new WaitForSeconds(timeBetwenColors);
        }
        spriteRenderer.color = originalColor;
    }
}
