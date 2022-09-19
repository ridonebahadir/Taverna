using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class FillArea : MonoBehaviour
{
    public Image image;
    private Sequence sequence;
    private Guid uid;
    public Spawn spawn;
    public Transform DesktopVaril;
    public Animator garsonAnim;
    public GameObject barmanCanvas;
    public GameObject barman;
    
    private void Start()
    {
        barmanCanvas.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            if (!barman.gameObject.activeSelf)
            {
                barmanCanvas.SetActive(true);
            }
           
            Fill();
           
        }
        
      
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag=="Player")
        {
          
            barmanCanvas.SetActive(false);
            DOTween.Kill(uid);
            sequence = null;
            image.DOFillAmount(0, 0.5f);
        }
     
    }
    bool oneTime;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag=="Barman")
        {
            if (!oneTime)
            {
              
                Fill();
                
            }
        }
    }
    public IEnumerator GoVaril(GameObject obj)
    {


        
        obj.transform.DOJump(DesktopVaril.transform.position, 10, 0, 0.5f).OnComplete(() => {

            StartCoroutine(spawn.SpawnObj(0, obj));
            image.DOFillAmount(0, 0.5f);
            spawn.turn--;
          
        });
       
        obj.transform.DOLocalRotate(new Vector3(0, 900, 0), 0.5f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
            yield return new WaitForSeconds(2);
        if (barman.activeSelf)
        {
            oneTime = false;
            garsonAnim.SetBool("Work", false);
        }
      

    }

    private void Fill()
    {
        if (spawn.CurrList.Count > 0 && spawn.spawnValue < spawn.outPoint.Length)
        {
            if (barman.activeSelf)
            {
                oneTime = true;
                garsonAnim.SetBool("Work", true);
            }
           
            GameObject obj = spawn.CurrList[spawn.CurrList.Count - 1];

            sequence = DOTween.Sequence();
            sequence.Append(image.DOFillAmount(1, 3).OnComplete(() =>
             StartCoroutine(GoVaril(obj))

            ));
            uid = System.Guid.NewGuid();
            sequence.id = uid;

        }
    }
}
