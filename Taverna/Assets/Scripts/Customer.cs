using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    NavMeshAgent meshAgent;
    public Transform deskParent;
    public Transform target;
   
    public Transform breakPoint;
    Animator anim;
    float yRot;
    Customer customer;
    Vector3 startPos;
   
    void Start()
    {
        transform.position = new Vector3(-30, 0, -30);
        meshAgent = GetComponent<NavMeshAgent>();
        startPos = transform.position;
        customer = GetComponent<Customer>();
        anim =transform.GetChild(0).GetComponent<Animator>();


        int child = Random.Range(0, 2) == 0 ? 0 : 1;
        target = deskParent.GetChild(child);


        if (child == 0) yRot = 0;
        else yRot = 180;

       
        
        target.parent.parent.GetChild(1).GetComponent<Money>().customer = transform;
        meshAgent.destination = target.position;
       

    }
    bool oneTime;
  
    private void Update()
    {
        
        if (meshAgent.remainingDistance <= 0)
        {

            if (!oneTime)
            {
                Event();
                oneTime = true;
            }
          

        }



    }

    private void Event()
    {
        anim.SetBool("Sit", true);
        transform.rotation = Quaternion.Euler(0, yRot, 0);
        Transform obj = target.transform.parent.parent;
        obj.GetComponent<BoxCollider>().enabled = true;
        obj.GetComponent<Spawn>().breaksPoint.Add(breakPoint);
        obj.GetComponent<Spawn>().customer = customer;
       
        


    }
    public IEnumerator Drink()
    {
        Transform obj = target.transform.parent.parent;
        obj.GetComponent<BoxCollider>().enabled = false;
        anim.SetBool("Drink",true);
        yield return new WaitForSeconds(7);
        anim.SetBool("Go", true);
        StartCoroutine(target.parent.parent.GetChild(1).GetComponent<Money>().MoneyPay());
        yield return new WaitForSeconds(3f);
        Destroy(breakPoint.GetChild(0).gameObject);
        anim.SetBool("Sit", false);
        meshAgent.destination = startPos;
       

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Door")
        {
           
            oneTime = false;
            meshAgent.destination = target.position;
            anim.SetBool("Drink", false);
            anim.SetBool("Go", false);
            anim.SetBool("Sit", false);

        }
    }

}
