using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryObject : InteractableObject
{
    [SerializeField]
    private Animator anim;
    public List<MemoryCard> cards = new List<MemoryCard>();
    public List<MemoryCard> unsolvedCards = new List<MemoryCard>();

    [SerializeField] private GameObject grayBox;
    [SerializeField] private GameObject colorBox;

    public override void IsActivated()
    {
        base.IsActivated();
        anim.SetBool("isOpen", true);
        StartCoroutine(CardUpdate());
    }

    public override void Update()
    {
        base.Update();
    }

    public IEnumerator CardUpdate()
    {
        while (true)
        {
            if (cards.Count >= 2)
            {
                yield return new WaitForSeconds(1);
                if (cards[0].type == cards[1].type)
                {
                    foreach (MemoryCard card in cards)
                    {
                        card.Cleared();
                    }
                }
                else
                {
                    foreach (MemoryCard card in cards)
                    {
                        card.Unflip();
                    }
                }
                cards.Clear();
            }

            if (unsolvedCards.Count == 0)
            {
                grayBox.SetActive(false);
                colorBox.transform.position = grayBox.transform.position;
                colorBox.SetActive(true);
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
