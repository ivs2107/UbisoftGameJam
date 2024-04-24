using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// deals with the healthBar.
/// </summary>
public class healthBarComponent : MonoBehaviour
{
    [SerializeField]
    private RectTransform redBar;
    [SerializeField]
    private RectTransform trailBar;
    [SerializeField]
    private RectTransform background;
    [SerializeField]
    private float hpLossSpeed = 0.05f;

    [SerializeField]
    private bool hpBarShake = true;

    [Range(0f,1f)]
    [SerializeField]
    private float hpBarShakeIntensity = 1;

    private float currentHp;
    private float hpTarget;
    private float redBarInitialWidth;
    private float trailBarInitialWidth;
    private float backgroundInitialWidth;
    private float redBarMaxWidth;
    private float trailBarMaxWidth;
    private float redBlackDiff;


    private float yMovement = 10;
    private float xMovement = 10;
    private float shakeDuration = 0.4f;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    private void Start()
    {
        hpTarget = 1;
        currentHp = 1;
        trailBarInitialWidth = trailBar.sizeDelta.x;
        backgroundInitialWidth = background.sizeDelta.x;
        redBarInitialWidth = redBar.sizeDelta.x;
        initialPosition = transform.localPosition;
        redBarMaxWidth = redBarInitialWidth;
        redBlackDiff = backgroundInitialWidth - redBarInitialWidth;
    }
    private void Update()
    {
        if (hpTarget < currentHp)
        {
            //max
            float hpLossThisFrame = Mathf.Clamp(currentHp - hpTarget, 0f, hpLossSpeed*Time.deltaTime);
            trailBar.sizeDelta = new Vector2(redBarMaxWidth * (currentHp - hpLossThisFrame), redBar.sizeDelta.y);
            currentHp -= hpLossThisFrame;
        }
    }

    public void setWidth(float hpPercentage)
    {
        redBar.sizeDelta = new Vector2(redBarMaxWidth * hpPercentage, redBar.sizeDelta.y);
        hpTarget = hpPercentage;

        if (hpBarShake)
        {
            StartCoroutine(ShakeCoroutine(1 - hpPercentage));
        }
    }
    
    public void setNewMaxWidth(float scale)
    {
        hpTarget = 1;
        currentHp = 1;
        redBarMaxWidth = redBarInitialWidth * scale;
        redBar.sizeDelta = new Vector2(redBarMaxWidth, redBar.sizeDelta.y);
        trailBar.sizeDelta = new Vector2(trailBarInitialWidth * scale, trailBar.sizeDelta.y);
        background.sizeDelta = new Vector2((backgroundInitialWidth * scale) - (redBlackDiff * (scale - 1)), background.sizeDelta.y);

    }
    private IEnumerator ShakeCoroutine(float intensity)
    {
        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * xMovement * intensity;
            float y = Random.Range(-1f, 1f) * yMovement * intensity;

            transform.localPosition = initialPosition + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = initialPosition;
    }
}
