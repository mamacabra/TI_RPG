using System;
using System.Collections;
using System.Collections.Generic;
using Combat;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RewardItens : MonoBehaviour
{
   private List<ItemScriptableObject> itensToReward = new List<ItemScriptableObject>();

   [SerializeField] private GameObject playButton;
   [SerializeField] private GameObject bau;
   [SerializeField] private List<Transform> itensSprite;
   [SerializeField] private List<Vector3> itensSpriteFinalPos;

   private void Start()
   {
      GetItens();

      playButton.SetActive(false);
     

      StartCoroutine(SetItensAnim());

      IEnumerator SetItensAnim()
      {
         if(itensToReward.Count != 0){
          bau.SetActive(true);
            yield return new WaitForSeconds(1.10f);
            for (int i = 0; i < itensToReward.Count; i++)
            {
               Transform item = itensSprite[i];

               item.DOScale(1, 0.75f)
                  .OnStart(() => { item.DOLocalMove(itensSpriteFinalPos[i], 1f).SetEase(Ease.OutBack); })
                  .SetEase(Ease.OutBack);
               yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(1.5f);
         }
         else{
            // Make more things..
         }

         playButton.SetActive(true);
      }

   }

   private int count = 0;
   private int attenpts = 0;

   void GetItens()
   {
      if (InventoryManager.instance)
      {
         if (InventoryManager.instance.inventoryData.TryGetRandomItemPerProbability(out var item))
         {
            itensToReward.Add(item);
            InventoryManager.instance.inventoryData.AddItemToInventory(item);
            itensSprite[count].GetComponent<Image>().sprite = item.sprite;
            count++;
         }
         else
            attenpts++;
         
         if(attenpts >= 3) return;
         if(count >=3) return;
         GetItens();
      }
   }
}
