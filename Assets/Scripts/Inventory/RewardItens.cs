using System;
using System.Collections;
using System.Collections.Generic;
using Combat;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RewardItens : MonoBehaviour
{
   private List<ItemScriptableObject> itensToReward =new List<ItemScriptableObject>();

   [SerializeField] private GameObject playButton;
   [SerializeField] private GameObject bau;
   [SerializeField] private List<Transform> itensSprite;
   [SerializeField] private List<Vector3> itensSpriteFinalPos;

   private void OnEnable()
   {
      playButton.SetActive(false);
      if (InventoryManager.instance)
      {
         for (int i = 0; i < 3; i++)
         {
            if (InventoryManager.instance.inventoryData.TryGetRandomItemPerProbability(out var item))
            {
               itensToReward.Add(item);
               InventoryManager.instance.inventoryData.AddItemToInventory(item);
               itensSprite[i].GetComponent<Image>().sprite = item.sprite;
            }
         }
      }

      bau.SetActive(true);

      StartCoroutine(SetItensAnim());
      IEnumerator SetItensAnim()
      {
         yield return new WaitForSeconds(1.10f);
         for (int i = 0; i < itensSprite.Count; i++)
         {
            Transform item = itensSprite[i];
            
            item.DOScale(1, 0.75f).OnStart(() =>
            {
               item.DOLocalMove(itensSpriteFinalPos[i],1f).SetEase(Ease.OutBack);
            }).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(0.5f);
         }
         
         yield return new WaitForSeconds(1.5f);
         playButton.SetActive(true);
      }
     
   }
   
}
