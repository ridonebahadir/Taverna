using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class Cost : MonoBehaviour
{
    public float cost;
    public GameObject money;
    public List<GameObject> moneyList = new List<GameObject>();
    public Image image;
    bool run;
    public TextMeshProUGUI costText;
    public float imageAmount;
    private void Start()
    {
        imageAmount = 1 / cost;
        costText.text = cost.ToString()+"$";
        for (int i = 0; i < cost; i++)
        {
           GameObject obj = Instantiate(money, transform.position,Quaternion.Euler(-90,0,0));
            moneyList.Add(obj);
            obj.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            run = true;
            StartCoroutine(MoneyGo(other.transform));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            run = false;
           
        }
    }

    IEnumerator MoneyGo(Transform other)
    {
        Player player = other.GetComponent<Player>();
        while (moneyList.Count>0&&run&&player.money>0)
        {
           
            GameObject obj = moneyList[0];
            moneyList.Remove(obj);
            player.money--;
            player.moneyText.text = player.money.ToString() + "$";
            obj.transform.position = other.position;
            obj.SetActive(true);
            obj.transform.parent = transform;
            obj.transform.DOLocalJump(Vector3.zero, 5, 0, 0.3f).OnComplete(()=>obj.SetActive(false));
            costText.text = moneyList.Count.ToString() + "$";
            image.fillAmount += imageAmount;
            yield return new WaitForSeconds(0.1f);
        }
        if (moneyList.Count<=0)
        {
            transform.gameObject.SetActive(false);
            transform.parent.GetChild(1).gameObject.SetActive(true);
            transform.parent.GetChild(1).transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutElastic);
            
        }
      
    }
}
