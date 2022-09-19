using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Barman : MonoBehaviour
{
    public GameObject barman;
    public Player player;
    public int valueBarman;
    private MeshRenderer meshRenderer;

    
    private void OnMouseDown()
    {
        if (player.money>= valueBarman)
        {
            player.money -= valueBarman;
            player.moneyText.text = player.money.ToString()+"$";
            barman.SetActive(true);
            barman.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutElastic);
            gameObject.SetActive(false);
        }
      
    }
}
