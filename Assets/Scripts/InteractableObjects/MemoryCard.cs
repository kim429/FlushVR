using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    DOOR,
    KEY,
    LOCK,
    PLUG,
    ROLL,
    STAR
}
public class MemoryCard : InteractableObject {
    [SerializeField] private MemoryObject memoryObject;
    public CardType type;
    private Animator anim;

    public override void IsActivated()
    {
        base.IsActivated();
        anim.SetBool("isFlipped", true);
        if (!memoryObject.cards.Contains(this))
            memoryObject.cards.Add(this);
    }

    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        memoryObject.unsolvedCards.Add(this);
    }

    public void Unflip()
    {
        anim.SetBool("isFlipped", false);
    }

    public void Cleared()
    {
        memoryObject.unsolvedCards.Remove(this);
        gameObject.SetActive(false);
    }
}
