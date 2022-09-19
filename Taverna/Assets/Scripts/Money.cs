using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Money : MonoBehaviour
{
    public GameObject money;
    public Transform moneyPoint;
    private Spawn spawn;
    public Transform customer;
    float yAxis;
    public List<GameObject> moneyList = new List<GameObject>();
    private BoxCollider boxCollider;
  
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        spawn = GetComponent<Spawn>();
        
    }
    public IEnumerator MoneyPay()
    {
       
        for (int i = 0; i < 10; i++)
        {
            GameObject obj = Instantiate(money, customer.position,Quaternion.Euler(-90,0,0));
            moneyList.Add(obj);
            obj.transform.parent = moneyPoint;
            obj.transform.DOLocalJump(new Vector3(0, yAxis, 0), 5, 0, 0.3f);    
            yAxis+=0.3f;
            yield return new WaitForSeconds(0.1f);
        }
        boxCollider.enabled = true;
    }
    public IEnumerator GoMoney(GameObject other)
    {
        boxCollider.enabled = false;
        Player player = other.GetComponent<Player>();
        int a = moneyList.Count;
        for (int i = 0; i < a; i++)
        {
           
           GameObject obj = moneyList[0];
            moneyList.Remove(obj);
            obj.transform.parent = other.transform;
            obj.transform.DOLocalJump(new Vector3(0, 2, 0), 5, 0, 0.3f).OnComplete(() =>
            {

                Destroy(obj);

                player.money++;
                player.moneyText.text = player.money.ToString() + "$";


            });
            yield return new WaitForSeconds(0.01f);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            if (moneyList.Count>0)
            {
                  StartCoroutine(GoMoney(other.gameObject));
            }
         
        }
    }
}
