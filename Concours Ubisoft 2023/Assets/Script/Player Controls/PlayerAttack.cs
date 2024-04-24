using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bulletOriginal;
    public GameObject bullet;
    public float bulletSpeed = 15f;
    public float bulletDamage = 10;
    public int numberBullets = 1;
    public int bulletsAngleBonus = 1;
    public float attackSpeed = 0.2f;

    public float ennemyDamage = 10f;
    public float ennemHP = 30f;

    public float playerMaxHP = 100f;
    //private GameObject bulletCopy;

    // public GameObject Charcater0G;
    // public GameObject Charcater100G;


    public static PlayerAttack instance;

    private void Awake()
    {
        if (instance == null)
        {
            
            instance = this;
            DontDestroyOnLoad(this);

          /*  if (Directory.Exists(prefabPath))
            {
                Debug.Log("MHMMMMM");
            }
            GameObject source = Resources.Load(prefabPath) as GameObject;
           // GameObject objSource = (GameObject)PrefabUtility.InstantiatePrefab(source);
            bullet = PrefabUtility.SaveAsPrefabAsset(source, variantAssetPath);*/
            //  PrefabUtility.UnpackPrefabInstance(bullet, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
            /*if (!Directory.Exists("Assets/Prefabs"))
                AssetDatabase.CreateFolder("Assets", "Prefabs");
            if (Directory.Exists("Assets/Prefabs/TemporyryBullet.prefab"))
                Directory.Delete("Assets/Prefabs/TemporyryBullet.prefab");
            string localPath = "Assets/Prefabs/TemporyryBullet.prefab";

            // Make sure the file name is unique, in case an existing Prefab has the same name.
            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

            // Create the new Prefab and log whether Prefab was saved successfully.
            bool prefabSuccess;
            //bullet = load
            // PrefabUtility.UnpackPrefabInstance(bullet,PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
            bullet = PrefabUtility.SaveAsPrefabAssetAndConnect(bullet, localPath, InteractionMode.AutomatedAction, out prefabSuccess);
            if (prefabSuccess == true)
                Debug.Log("Prefab was saved successfully");
            else
                Debug.Log("Prefab failed to save" + prefabSuccess);

            bullet = Resources.Load(localPath) as GameObject;

            //bullet.transform.localScale= new Vector3  (1, 1, 1);*/
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            this.bulletDamage = instance.bulletDamage;
            this.bulletSpeed = instance.bulletSpeed;
            this.playerMaxHP = instance.playerMaxHP;
            Destroy(instance.gameObject);
            instance = this;
        }
    }

    private void Start()
    {
        string prefabPath = "Assets/Resources/prefabs/Bullet.prefab";
        string variantAssetPath = "Assets/Resources/prefabs/TemporaryBullet.prefab";
        //File.Delete(variantAssetPath);
       // GameObject prefabRef = Resources.Load("prefabs/Bullet.prefab") as GameObject;//(GameObject)AssetDatabase.LoadMainAssetAtPath(prefabPath);
       // GameObject instanceRoot = Instantiate(bulletOriginal);//(GameObject)PrefabUtility.InstantiatePrefab(prefabRef);
        //bullet = instanceRoot;

        bullet.transform.localScale = new Vector3(1f, 1f, 1f);
        bullet.GetComponent<Rigidbody2D>().mass = 0;
        bullet.GetComponent<Rigidbody2D>().gravityScale = 0;
        // bullet
        //bullet = PrefabUtility.SaveAsPrefabAsset(instanceRoot, variantAssetPath);

        /*BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(variantAssetPath);
        bf.Serialize(file, instanceRoot);
        file.Close();
        BinaryFormatter bf2 = new BinaryFormatter();
        FileStream file2 = File.Open(variantAssetPath, FileMode.Open);
        GameObject obj2 = (GameObject)bf2.Deserialize(file2);
        file.Close();
        bullet = obj2;*/

       // Destroy(instanceRoot);
    }
}
