using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffects : MonoBehaviour
{


    public void CallItemHPUp()
    {
        GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<HealthComponent>().ChangeMaxHp(60);
    }
    public GameObject Turret;
    public GameObject IceCream;
    public GameObject Barbeque;
    public void CallTurret()
    {
        ObjectManager.instance.currentObject = Turret;
    }
    public void CallIceCream()
    {
        ObjectManager.instance.currentObject = IceCream;
    }

    public void CallBarbeque()
    {
        ObjectManager.instance.currentObject = Barbeque;
    }
    public void CallAddDamage()
    {
        PlayerAttack.instance.bulletDamage += 10;
    }
    public void CallAddSpeedBullet()
    {
        PlayerAttack.instance.bulletSpeed *= 1.5f;
        PlayerAttack.instance.bullet.transform.localScale = Vector3.Scale(PlayerAttack.instance.bullet.transform.localScale, new Vector3(0.75f, 0.75f, 0.75f));
    }

    public void CallBigger()
    {
        PlayerAttack.instance.bullet.transform.localScale = Vector3.Scale(PlayerAttack.instance.bullet.transform.localScale, new Vector3(1.5f, 1.5f, 1.5f));
        PlayerAttack.instance.bulletSpeed *= 0.6f;
        PlayerAttack.instance.bulletDamage *= 1.5f;
    }

    public void CallGravity()
    {
        PlayerAttack.instance.bullet.GetComponent<Rigidbody2D>().gravityScale = 0.3f;
    }

    public void CallKnockback()
    {
        PlayerAttack.instance.bullet.GetComponent<Rigidbody2D>().mass = 1;
        PlayerAttack.instance.bulletDamage += 20f;
    }

    public void CallDoubleTrouble()
    {
        PlayerAttack.instance.numberBullets++;
    }

    public void CallReallyfast()
    {
        PlayerAttack.instance.bulletDamage /= 2;
        PlayerAttack.instance.bulletSpeed *= 2.5f;
        PlayerAttack.instance.attackSpeed *= 0.1f;
        PlayerAttack.instance.bullet.transform.localScale = Vector3.Scale(PlayerAttack.instance.bullet.transform.localScale, new Vector3(0.5f, 0.5f, 0.5f));

    }


    public void CallHPminus()
    {

        GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<HealthComponent>().maxHp -= 30;
    }

    public void CallTripleTroubleSPeed()
    {
        PlayerAttack.instance.numberBullets++;
        PlayerAttack.instance.numberBullets++;
        //PlayerAttack.instance.bulletDamage /= 2;
        PlayerAttack.instance.bulletSpeed *= 0.7f;
        PlayerAttack.instance.attackSpeed *= 1.5f;
    }

    public void CallHPDamage()
    {
        GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<HealthComponent>().ChangeMaxHp(10);
        PlayerAttack.instance.bulletDamage += 10;
    }

    public void CallQuadShot()
    {
        PlayerAttack.instance.numberBullets += 4;
    }

    public void CallCandyBlue()
    {
        PlayerAttack.instance.bulletDamage *= 3;

    }

    public void CallCrepe()
    {
        GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<HealthComponent>().ChangeMaxHp(30);
        PlayerAttack.instance.attackSpeed *= 1.5f;
    }

    public void CallWine()
    {
        PlayerAttack.instance.bulletsAngleBonus += 2;
        PlayerAttack.instance.attackSpeed *= 1.5f;
    }

    public void CallPepper()
    {
        PlayerAttack.instance.bulletsAngleBonus += 4;
    }

    public void CallStrawberry()
    {
        PlayerAttack.instance.bulletsAngleBonus += 2;
        GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<HealthComponent>().ChangeMaxHp(30);
    }

}
