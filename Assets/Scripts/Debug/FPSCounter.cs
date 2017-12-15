using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour {

    public int fps = 0;
    [SerializeField] private Text fpsUI;
    private float deltaTime;

	// Use this for initialization
	void Start ()
    {
        InvokeRepeating("FpsClear", 1f, 1f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1f / deltaTime;
        fpsUI.text = "" + fps + " fps";
    }
}
