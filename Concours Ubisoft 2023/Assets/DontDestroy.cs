using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public GameObject weapon;
   // public bool weaponHasChanged = false;
    public GameObject Player0G;
    public GameObject Player100G;

    public SpriteRenderer spriteMenu;
    // Update is called once per frame
    void Update()
    {
        /*if (weaponHasChanged) {

            Debug.Log(weapon);
            DontDestroyOnLoad(weapon);
            weaponHasChanged = false;
        }*/
    }
    private void Start()
    {
        GameObject go0G = Player0G.transform.Find("Character0G/WeaponParent/Weapon/weaponSprite").gameObject;
        spriteMenu.sprite = go0G.GetComponent<SpriteRenderer>().sprite;
    }

    public void weaponHasChanged(Sprite spriteWeapon)
    {
        GameObject go0G = Player0G.transform.Find("Character0G/WeaponParent/Weapon/weaponSprite").gameObject;
        go0G.GetComponent<SpriteRenderer>().sprite = spriteWeapon;
        GameObject go100G = Player100G.transform.Find("Character/WeaponParent/Weapon/weaponSprite").gameObject;
        go100G.GetComponent<SpriteRenderer>().sprite = spriteWeapon;
    }

}
