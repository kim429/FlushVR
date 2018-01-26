using UnityEngine;

public abstract class InteractableObject : MonoBehaviour {

    [Header("Interactable Object")]
    [Tooltip("Time needed to activate this component")]
    public float activationDuration = 4f;

    [Tooltip("How long have we looked at the object")]
    public float hitDuration;

    [Tooltip("This will be set to \"True\" when the \"IsActivated\" is called")]
    [SerializeField] protected bool active = true;

    // This method will be called from the Gaze Interections script
    public virtual void IsActivated()
    {
        active = true;
        hitDuration = 0;
        Gaze.controller.reticleAnimator.SetBool("isCompleted", true);
    }

    protected ActionIndicator indicator;

	// When we are enabled
    public virtual void Start()
    {
        indicator = Instantiate(UI.current.aIndicatorPrefab, transform);
    }

	// Every frame
    public virtual void Update()
    {
        UpdateGazeMeter();
    }

	// Updates our GazeMeter
    private void UpdateGazeMeter()
    {
        if (!Gaze.controller.IsGazingAt(this))
        {
            hitDuration -= Time.deltaTime * 2;
        }
    }

    // This will increase when we are looking at the object
    public float HitDuration
    {
        get
        {
            return hitDuration;
        }

        set
        {
            if (value > activationDuration)
            {
                hitDuration = activationDuration;
            }
            else if (value < 0)
            {
                hitDuration = 0;
            }
            else
            {
                hitDuration = value;
            }
        }
    }
}