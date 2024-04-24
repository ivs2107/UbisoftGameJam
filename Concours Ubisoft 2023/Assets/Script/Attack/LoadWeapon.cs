using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadWeapon : MonoBehaviour
{
    public GameObject prefabWeapon;
    public SpriteRenderer weapon;
    // Start is called before the first frame update
    void Start()
    {
        weapon.sprite = prefabWeapon.transform.Find("Character/WeaponParent/Weapon/weaponSprite").GetComponent<SpriteRenderer>().sprite;
    }
}

    
