using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class LoadNewLevel : MonoBehaviour
{
    public bool first = true;
    // Start is called before the first frame update
    public ScoreManager scoreManager;
    public ProceduralManager proceduralMap;
    public GameObject Character0G;
    public GameObject Character;

    public Animator animator;


    private TMP_Text textName;
    private TMP_Text description;
    public GameObject itemText;

    public void Awake()
    {
        proceduralMap = GameObject.FindGameObjectWithTag("proceduralManager").GetComponent<ProceduralManager>();
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }

    public void Start()
    {
        itemText = GameObject.FindGameObjectWithTag("PanelItem");
        textName = itemText.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>();
        description = itemText.transform.GetChild(0).GetChild(3).GetComponent<TMP_Text>();

    }

    private void OnTriggerStay2D(Collider2D col)
    {
        trigger(col);
    }

    private async void trigger(Collider2D col)
    {
        if (col.tag == "Player" && first && GameManager.instance.hasSelected)
        {
            AudioManager.instance.Play("LevelFinished");
            

            //proceduralMap.load(Vector3.zero);//this.transform.parent.position);
            first = false;
            scoreManager.AddScore(ScoreManager.scores.scoreLoadScene,this.transform.position);
           
            animator.enabled = true;
            StartCoroutine(changeGravityPlayer(col));

            //  GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>().enabled = false;
            //  StartCoroutine()
            //GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>().enabled = true ;

        }
    }


    IEnumerator changeGravityPlayer(Collider2D col)
    {

        GameObject gc = GameObject.FindGameObjectWithTag("GameManager");
        GameObject gp = GameObject.FindGameObjectWithTag("Player");
        GameObject transformation = GameObject.FindGameObjectWithTag("Transformation");
        transformation.transform.position = new Vector3( gp.transform.position.x, gp.transform.position.y - 1, gp.transform.position.z) ;
        transformation.GetComponent<ParticleSystem>().Play();
        gp.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(1.0f);

        //chronometre

        scoreManager.chronemetreManager.time = 60f;
        scoreManager.chronemetreManager.clearedInTime = true;
        //----
        GameObject goInstantiate = null;
        bool isTopDown = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ChoiceManager>().isTopDown;
        if (isTopDown)
        {
            goInstantiate = Character0G;
        }
        else
        {
            goInstantiate = Character;
        }
        GameObject gnew= Instantiate(goInstantiate, gc.transform.position, gc.transform.rotation);
        gnew.transform.GetChild(0).position = gp.transform.position;
        gnew.transform.GetChild(0).rotation = gp.transform.rotation;
        GameObject.FindGameObjectWithTag("MainCinemachine").GetComponent<CinemachineVirtualCamera>().Follow = gnew.transform.GetChild(0);
        proceduralMap.load(Vector3.zero,isTopDown);
        if (proceduralMap.Stronger)
        {
            itemText.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("itemPanel", -1, 0f);
            textName.text = "WARNING";
            description.text = "ENEMIES ARE GETTING STRONGER";
        }
        Destroy(gc);
        Destroy(this.gameObject);


    }

}
