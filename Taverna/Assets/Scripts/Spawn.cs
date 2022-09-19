using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spawn : MonoBehaviour
{
    public int jumpPower;
    public List<GameObject> SpawnList = new List<GameObject>();
    public List<GameObject> CurrList = new List<GameObject>();
    public List<Transform> breaksPoint = new List<Transform>();
  
    public Transform[] outPoint;
    public int need;
    public int current;
    public int spawnValue;
  
    public int turn;
    public GameObject prefab;
    [Header("For Crushing")]
    public bool isCrushGirl;
    public Animator crushingGirlAnim;
    
    
    [Header("For Bar")]
    public bool isBar;

    [Header("For Desk")]
    public Customer customer;
    public bool isDesk;

    public IEnumerator ComeObj(Player player,List<GameObject> list)
    {
        while (SpawnList.Count>0)
        {
            GameObject obj = SpawnList[0];
            SpawnList.Remove(obj);
            obj.transform.parent = player.slotList[player.cameObj];
            list.Add(obj);
            player.allList.Add(obj);
            obj.transform.DOLocalJump(Vector3.zero, 5, 0, 0.75f).SetEase(Ease.OutSine);
            //obj.transform.DOLocalRotate(Vector3.zero, 0.5f).SetEase(Ease.OutSine);
            obj.transform.DOLocalRotate(new Vector3(0, 540, 0), 0.75f, RotateMode.FastBeyond360);
            player.cameObj++;
            spawnValue--;
            if (isCrushGirl)
            {
                turn -= need;
            }
           
            
           
            yield return new WaitForSeconds(0.25f);
        }

        
      
    }
    public IEnumerator GoObj(Player player,List<GameObject> list,bool reduction)
    {
        
        while (list.Count > 0 && turn<breaksPoint.Count&& spawnValue < outPoint.Length)
        {
            if (isDesk)
            {
                customer.StartCoroutine(customer.Drink());
            }
            
            GameObject obj = list[list.Count-1];
         
            list.Remove(obj);
            player.allList.Remove(obj);
            CurrList.Add(obj);
            obj.transform.parent = breaksPoint[turn];
          
            obj.transform.DOLocalJump(new Vector3(0, 0, -1), jumpPower, 0, 0.75f).SetEase(Ease.OutSine);
            obj.transform.DOLocalRotate(new Vector3(0, 540, 0), 0.75f, RotateMode.FastBeyond360);
            if (reduction) obj.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutSine);
            player.cameObj--;
            current++;
            turn++;
           
           
            
            if (current >= need&&isCrushGirl)
            {
               
                StartCoroutine(SpawnObj(0,obj));
            }
            
            yield return new WaitForSeconds(0.25f);
        }
      
    }
    public IEnumerator SpawnObj(float waitTime,GameObject own)
    {
       
        if (spawnValue<outPoint.Length)
        {
            if (isCrushGirl)
            {
                crushingGirlAnim.SetBool("Work", true);
            }

            yield return new WaitForSeconds(waitTime);
            current -= need;
            GameObject obj = Instantiate(prefab, own.transform.position, Quaternion.Euler(0, 0, 0), outPoint[spawnValue]);
            SpawnList.Add(obj);
            obj.transform.DOLocalJump(Vector3.zero, jumpPower, 0, 0.5f).OnComplete(() => crushingGirlAnim.SetBool("Work", false));
            spawnValue++;
            yield return new WaitForSeconds(0.3f);
          
            CurrList.Remove(own.gameObject);
            Destroy(own.gameObject);
        }
    }
      
    public void FixedEdit(Player player)
    {
        for (int i =0; i < player.allList.Count; i++)
        {
            GameObject obj = player.allList[i];
            obj.transform.parent = player.slotList[i];
            obj.transform.DOLocalMove(Vector3.zero, 0.3f);
            
        }
    }
   
   
}
