using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelButtonController : MonoBehaviour
{

    public int Id;
    private Animator anim;
    public SpriteRenderer spriteR;
    private bool selected = false;
    public Sprite icon;


    public int Damage;
    public float AttackSpeed;
    public int Health;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Selected()
    {
        selected = true;
        spriteR.sprite = icon;
        GetComponentInParent<DontDestroy>().weaponHasChanged(spriteR.sprite);
        Debug.Log(icon);
        Debug.Log(spriteR.sprite);
        PlayerAttack.instance.bulletDamage = Damage;
        PlayerAttack.instance.playerMaxHP = Health;
        PlayerAttack.instance.attackSpeed = AttackSpeed;
    }

    public void Deselected()
    {
        selected = false;
    }

    public void HoverEnter()
    {
        anim.SetBool("Hover", true);
    }

    public void HoverExit()
    {
        anim.SetBool("Hover", false);
    }

}
