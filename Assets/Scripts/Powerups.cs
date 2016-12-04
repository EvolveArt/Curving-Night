using UnityEngine;

public class Powerups : MonoBehaviour {

    //private static Powerups instance;
    //public static Powerups Instance { get { return instance; } }

    [Header("Speed/Slow")]
    public float SpeedAmount = 0.25f;
    public float SpeedDuration = 3f; 
    public bool GSpeed = false;
    public bool GSlow = false;
    public bool RSpeed = false;
    public bool RSlow = false;

    [Header("Thin/Fat")]
    public float WidthAmount = .6f;
    public float ThinDuration = 5f;
    public bool Thin = false;
    public bool Fat = false;

    [Header("Reverse")]
    public bool Reverse = false;
    public float reverseDuration = 4f;

	void Start()
    {
        //instance = this;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(gameObject);

        if (GSpeed)
            Snake.Instance.SpeedUp(col.GetComponent<Snake>(), SpeedAmount, SpeedDuration);

        if (GSlow)
            Snake.Instance.SlowDown(col.GetComponent<Snake>(), SpeedAmount, SpeedDuration);

        if (Thin)
            Snake.Instance.Thin(col.GetComponentInParent<Tail>(), WidthAmount, ThinDuration);

        if (Fat)
            Snake.Instance.Fat(col.GetComponentInParent<Tail>(), WidthAmount, ThinDuration);

        if (RSpeed)
            Snake.Instance.SpeedOther(col.GetComponent<Snake>(), SpeedAmount, SpeedDuration);

        if (RSlow)
            Snake.Instance.SlowOther(col.GetComponent<Snake>(), SpeedAmount, SpeedDuration);

        if (Reverse)
            Snake.Instance.reverseCommands(col.GetComponent<Snake>(), reverseDuration);

    }
}
