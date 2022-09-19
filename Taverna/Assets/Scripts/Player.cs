using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Money")]
    public int money;
    public TextMeshProUGUI moneyText;


    [Header("SLOT")]
    public int slotAmount;
    public GameObject slot;
    public Transform slotParent;
    float yAxis;
    public int cameObj;
    public List<Transform> slotList = new List<Transform>();

   
    [Header("Spawn")]
    public Spawn spawn;
    private Player player;
   
    [Header("List")]
    public List<GameObject> allList = new List<GameObject>();
    public List<GameObject> grapeList = new List<GameObject>();
    public List<GameObject> varilList = new List<GameObject>();
    public List<GameObject> glassList = new List<GameObject>();

   
    void Start()
    {
        player = GetComponent<Player>();
        moneyText.text = money.ToString()+"$";
        for (int i = 0; i < slotAmount; i++)
        {

            SpawnSlot();


        }

    }

  
    void SpawnSlot()
    {
        GameObject obj = Instantiate(slot, slotParent);
        obj.transform.localPosition = new Vector3(0, yAxis, 0);
        yAxis+=0.75f;
        obj.GetComponent<HingeJoint>().connectedBody = obj.transform.parent.GetChild(slotParent.childCount-2).GetComponent<Rigidbody>();
        slotList.Add(obj.transform);
    }
    bool oneTime;
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Vineyard")
        {
            spawn = other.GetComponent<Spawn>();
            StartCoroutine(spawn.ComeObj(player,grapeList));
        }
        if (other.tag== "Crushing")
        {
            spawn = other.GetComponent<Spawn>();
            StartCoroutine(spawn.GoObj(player,grapeList,false));
        }
        if (other.tag=="OutPointCrush")
        {
            spawn = other.transform.parent.GetComponent<Spawn>();
            StartCoroutine(spawn.ComeObj(player, varilList));
        }
        if (other.tag=="Bar")
        {
            spawn = other.transform.GetComponent<Spawn>();
            StartCoroutine(spawn.GoObj(player, varilList,false));
        }
        if (other.tag == "Stand")
        {
            spawn = other.transform.parent.GetComponent<Spawn>();
            StartCoroutine(spawn.ComeObj(player, glassList));
        }
        if (other.tag == "Desk")
        {
            spawn = other.transform.GetComponent<Spawn>();
            StartCoroutine(spawn.GoObj(player, glassList,true));
           //StartCoroutine(spawn.customer.Drink());
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Vineyard")
        {
            spawn = other.transform.GetComponent<Spawn>();
            spawn.FixedEdit(player);
        }
        if (other.tag == "Crushing")
        {
            spawn = other.transform.GetComponent<Spawn>();
            spawn.FixedEdit(player);
        }
        if (other.tag == "OutPointCrush")
        {
            spawn = other.transform.parent.GetComponent<Spawn>();
            spawn.FixedEdit(player);
        }
        if (other.tag == "Bar")
        {
            spawn = other.transform.GetComponent<Spawn>();
            spawn.FixedEdit(player);

        }
        if (other.tag == "Stand")
        {
            spawn = other.transform.parent.GetComponent<Spawn>();
            spawn.FixedEdit(player);
        }
        if (other.tag == "Desk")
        {
            spawn = other.transform.GetComponent<Spawn>();
            spawn.FixedEdit(player);

        }


    }
   
}

    
