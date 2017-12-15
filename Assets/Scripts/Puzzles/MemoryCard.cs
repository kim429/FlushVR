using UnityEngine;

public enum Card
{
    KING,
    QUEEN,
    SHEEP,
    DRAGON,
    HORSE,
    COW,
    CHICKEN,
    BASILISK
}
public class MemoryCard : InteractableObject {

    public Card thisCard = Card.BASILISK;
    float test = 15;
    public bool actifist = false;
    [SerializeField] private MemoryManager meMa = null;

    private void Awake()
    {
        meMa = GetComponentInParent<MemoryManager>();
    }

    public override void IsActivated()
    {
        base.IsActivated();
        actifist = true;
        meMa.AddToList(gameObject.GetComponent<MemoryCard>());
    }
}
