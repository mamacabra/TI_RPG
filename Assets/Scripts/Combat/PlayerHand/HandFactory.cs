﻿using UnityEngine;

namespace Combat
{
    public class HandFactory : MonoBehaviour
    {
        public GameObject cardPrefab;
        public static HandFactory Instance { get; private set; }

        private void Start()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            CreateCards();
        }

        private void OnDisable()
        {
            DestroyAllCards();
        }

        public void CreateCards()
        {
            DestroyAllCards();
            InstantiateHandCards();
        }

        private void InstantiateHandCards()
        {
            int i = 0;
            foreach (Member member in CombatManager.Instance.HeroParty.Members)
            {
                if (member == null || member.Character.IsDead || member.Character.HasActionPoints == false) continue;

                foreach (Card card in member.Hand)
                {
                    GameObject cardGameObject = Instantiate(cardPrefab, transform);

                    CardController cardController = cardGameObject.GetComponent<CardController>();
                    cardController.Setup(member, card, i);

                    i++;
                }
            }
        }

        private void DestroyAllCards()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
