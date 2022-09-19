using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Vineyard : MonoBehaviour
{
    public Transform[] grapes;
    public GameObject grape;
    public Transform grapesParent;
  
    
    public Spawn spawn;
   
    
    IEnumerator Growth(int value)
    {
       
        for (int i = 0; i < value; i++)
        {
            GameObject obj = Instantiate(grape, grapes[i].transform.position,Quaternion.Euler(-90,0,0), grapesParent);
            obj.transform.DOScale(new Vector3(1, 1, 1), 3).SetEase(Ease.OutSine).OnComplete(() => {
                spawn.SpawnList.Add(obj);
                
            });
            yield return new WaitForSeconds(1.5f);
        }
       
        
      


    }
    private void OnTriggerExit(Collider other)
    {
       
            if (other.tag == "Player")
            {

                switch (grapesParent.childCount)
                {
                    case 0:
                        StartCoroutine(Growth(2));
                        
                        break;
                    case 1:
                        StartCoroutine(Growth(1));
                      
                        break;


                }


            }
        }
       
    
    
}
