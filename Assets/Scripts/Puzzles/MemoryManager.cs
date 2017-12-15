using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryManager : MonoBehaviour {

    [SerializeField] private List<MemoryCard> cards;
    [SerializeField] List<MemoryCard> flippedCards;
	// Use this for initialization
	void Awake ()
    {
        foreach (MemoryCard childCard in gameObject.GetComponentsInChildren<MemoryCard>())
        {
            cards.Add(childCard);
        }
	}
	
	public void AddToList (MemoryCard newCard)
    {
        if (flippedCards.Contains(newCard))
        {
            return;
        }
        else
        {
            if (flippedCards.Count >= 2)
            {
                flippedCards.Clear();
            }

            flippedCards.Add(newCard);

            if (flippedCards.Count == 2 && flippedCards[0].thisCard == flippedCards[1].thisCard)
            {
                foreach (MemoryCard matchCard in flippedCards)
                {
                    cards.Remove(matchCard);
                    Destroy(matchCard.transform.gameObject);
                }
                flippedCards.Clear();
            }
            if (cards.Count == 0)
            {
                MemoryCleared();
            }
        }
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i] == null)
            {
                cards.Remove(cards[i]);
            }
        }
    }

    private void MemoryCleared()
    {
        return;
    }
}
