using UnityEngine;
using DG.Tweening;

public class GhostTrail : MonoBehaviour
{
    public Transform move;
    public SpriteRenderer original;
  //  private SpriteRenderer sr;
    public Transform ghostsParent;
    public Color trailColor;
    public Color fadeColor;
    public float ghostInterval;
    public float fadeTime;

    private void Start()
    {
        //anim = FindObjectOfType<AnimationScript>();
       // move = FindObjectOfType<Movement>();
      //  sr = GetComponent<SpriteRenderer>();
    }

    public void ShowGhost()
    {
        StartCoroutine(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>().ProcessShake(15f, 0.24f,1.4f));
        Sequence s = DOTween.Sequence();

        for (int i = 0; i < ghostsParent.childCount; i++)
        {
            Transform currentGhost = ghostsParent.GetChild(i);
            s.AppendCallback(()=> currentGhost.position = move.transform.position);
            s.AppendCallback(() => currentGhost.GetComponent<SpriteRenderer>().flipX = (move.lossyScale.x < 0 ? true:false) );
            s.AppendCallback(()=>currentGhost.GetComponent<SpriteRenderer>().sprite = original.sprite);
            s.Append(currentGhost.GetComponent<SpriteRenderer>().material.DOColor(trailColor, 0));
            s.AppendCallback(() => FadeSprite(currentGhost));
            s.AppendInterval(ghostInterval);
        }
    }

    public void FadeSprite(Transform current)
    {
        current.GetComponent<SpriteRenderer>().material.DOKill();
        current.GetComponent<SpriteRenderer>().material.DOColor(fadeColor, fadeTime);
    }

}
