using UnityEngine;
using System.Collections;

public class Snake : MonoBehaviour {

    private static Snake instance;
    public static Snake Instance { get { return instance; } }

    Tail[] snakes = new Tail[0];

    float speed;
    public float baseSpeed = 2f;
    public float rotationSpeed = 200f;
    public string PlayerName = "Player";

    public string leftCMD = "";
    public string rightCMD = "";
    bool lKeyPressed = false;
    bool rKeyPressed = false;
    float rotation = 0;
    

    [HideInInspector]
    public bool isDead = false;

    private bool commandsReversed = false;

	void Start()
    {
        instance = this;
        speed = baseSpeed;

        snakes = FindObjectsOfType(typeof(Tail)) as Tail[];
    }

	void Update () {

        if(GetComponentInParent<Tail>().invicible == false && !isDead)
            KillIfOutmap();

        if (isDead)
        {
            rotationSpeed = 0f;
            speed = 0f;
        }
	}
	
    void KillIfOutmap()
    {
        if (transform.position.x > RandomSpawn.mapWidth || transform.position.x < -RandomSpawn.mapWidth  || transform.position.y > RandomSpawn.mapHeight || transform.position.y < -RandomSpawn.mapHeight )
                GameManager.Instance.KillPlayer(this.gameObject);
    }

	// Update is called once per frame
	void FixedUpdate () {
        lKeyPressed = Input.GetKey(leftCMD);
        rKeyPressed = Input.GetKey(rightCMD);

        if(lKeyPressed)
        {
            rotation = Mathf.Lerp(0f, 1f, Time.time / 2f);
        } else if(rKeyPressed)
        {
            rotation = -(Mathf.Lerp(0f, 1f, Time.time / 2f));
        } else
        {
            rotation = 0;
        }

        transform.Translate(Vector2.up * speed * Time.fixedDeltaTime, Space.Self);
        if (!commandsReversed)
        {
            transform.Rotate(Vector3.forward * rotation * rotationSpeed * Time.fixedDeltaTime);
        }
        else
        {
            transform.Rotate(Vector3.forward * -rotation * rotationSpeed * Time.fixedDeltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Solid" && !GetComponentInParent<Tail>().invicible)
        {
            GameManager.Instance.KillPlayer(this.gameObject);
        }
    }

    public void SpeedUp(Snake player, float amount, float duration)
    {
        float ToSpeed = player.speed * amount;
        player.speed += ToSpeed;
        StartCoroutine(MinusSpeed(player, duration, ToSpeed));
    }

    public void SlowDown(Snake player, float amount, float duration)
    {
        float ToSlow = player.speed * amount;
        player.speed -= ToSlow;
        StartCoroutine(PlusSpeed(player, duration, ToSlow));
    }

    public void Thin(Tail player, float width, float duration)
    {
        float ToThin = player.snakeWidth * width;
        player.snakeWidth -= ToThin;
        StartCoroutine(PlusWidth(player, duration, ToThin));
    }

    public void Fat(Tail player, float width, float duration)
    {
        foreach (Tail snake in snakes)
        {
            if (snake != player)
            {
                float ToFat = snake.snakeWidth * width;
                snake.snakeWidth += ToFat;
                StartCoroutine(MinusWidth(snake, duration, ToFat));
            }
        }
    }

    public void SpeedOther(Snake player, float amount, float duration)
    {
        foreach (Tail tail in snakes)
        {
            Snake snake = tail.GetComponentInChildren<Snake>();
             
            if (snake != player)
            {
                float ToSpeed = snake.speed * amount;
                snake.speed += ToSpeed;
                StartCoroutine(MinusSpeed(snake, duration, ToSpeed));
            }
        }
    }

    public void SlowOther(Snake player, float amount, float duration)
    {
        foreach (Tail tail in snakes)
        {
            Snake snake = tail.GetComponentInChildren<Snake>();

            if(snake != player)
            {
                float ToSlow = snake.speed * amount;
                snake.speed -= ToSlow;
                StartCoroutine(PlusSpeed(snake, duration, ToSlow));
            }

        }
    }

    IEnumerator MinusSpeed(Snake player, float time, float ToReset)
    {
        yield return new WaitForSecondsRealtime(time);
        if (!isDead)
            player.speed -= ToReset;           
    }

    IEnumerator PlusSpeed(Snake player, float time, float ToReset)
    {
        yield return new WaitForSecondsRealtime(time);
        if (!isDead)
            player.speed += ToReset;
    }

    IEnumerator MinusWidth(Tail player, float time, float ToWidth)
    {
        yield return new WaitForSecondsRealtime(time);
        if (!isDead)
            player.snakeWidth -= ToWidth;
    }

    IEnumerator PlusWidth(Tail player, float time, float ToWidth)
    {
        yield return new WaitForSecondsRealtime(time);
        if (!isDead)
            player.snakeWidth += ToWidth;
    }

    public void reverseCommands(Snake player, float duration)
    {
        foreach (Tail tail in snakes)
        {
            Snake snake = tail.GetComponentInChildren<Snake>();

            if (snake != player)
            {
                snake.commandsReversed = true;
                StartCoroutine(ReverseCommands(snake, duration));
            }

        }
    }

    IEnumerator ReverseCommands(Snake player, float time)
    {
        yield return new WaitForSeconds(time);
        player.commandsReversed = false;
    }
}
