using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBombGameManager : MonoBehaviour
{
    public static TimeBombGameManager instance;
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    public MathPanel mathPanel;
    public ParticleSystem explosion;
    public ClockController clock;

    private float timeLeft = 60f;
    public float GetTimeLeft()
    {
        return timeLeft;
    }
    private bool gameOver = false;

    int a = 1;
    int b = 1;
    int c = 1;
    int d = 1;
    public int BOMB_PASSCODE = 1;
    public bool stopTimer = false;

    public enum Operation
    {
        ADD, SUB, MULT
    }
    List<Operation> ops;

    void Start()
    {
        explosion.Stop();
        // set random int values
        a = Random.Range(-99, 100);
        b = Random.Range(-9, 10);
        c = Random.Range(-99, 100);
        d = Random.Range(-9, 10);        

        ops = new List<Operation>();
        List<Operation> available_ops = new List<Operation>();
        available_ops.Add(Operation.ADD);
        available_ops.Add(Operation.SUB);
        available_ops.Add(Operation.MULT);
        // shuffle operations
        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, available_ops.Count);
            Operation new_op = available_ops[index];
            available_ops.Remove(new_op);
            ops.Add(new_op);
        }

        // print ("val1: " + a);
        // print ("val2: " + b);
        // print ("val3: " + c);
        // print ("val4: " + d);
        // print ("op1: " + ops[0]);
        // print ("op2: " + ops[1]);
        // print ("op3: " + ops[2]);

        BOMB_PASSCODE = CalculatePasscode();
        print ("BOMB_PASSCODE: " + BOMB_PASSCODE);
        mathPanel.SetTextValues(a, b, c, d, ops[0], ops[1], ops[2]);
    }

    void Update()
    {
        if (stopTimer)
            return;

        // set current time between chars
        float currentTimeScale = TimePerceptionController.instance.GetGameTimeScale();
        timeLeft -= Time.deltaTime * currentTimeScale;

        // check if game is lost
        if (timeLeft <= 0f)
        {
            timeLeft = 0f;

            if (!gameOver)
            {
                gameOver = true;
                // explosion sound effect and particles
                explosion.Play();
                AudioManager.instance.PlaySound(
                        AudioManager.instance.database.explosion, 
                        0.5f, 
                        false, 
                        Mathf.Lerp(0.5f, 2f, currentTimeScale / TimePerceptionController.instance.maxTimeScale),
                        "explode"
                    );
                AudioManager.instance.PlaySound(
                        AudioManager.instance.database.engine, 
                        0.5f, 
                        true, 
                        Mathf.Lerp(0.5f, 2f, currentTimeScale / TimePerceptionController.instance.maxTimeScale),
                        "ssss"
                    );

                // no more input allowed
                PlayerPositionController.instance.canInput = false;
                PlayerScaleController.instance.canInput = false;
                clock.isOn = false;

                // start coroutine to restart level
                StartCoroutine(RestartLevelDelay(5f));
            }   
        }
    }

    private IEnumerator RestartLevelDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.instance.GoToScene("TimeBombScene");
    }

    private int CalculatePasscode()
    {
        int answer = 0;
        if (ops[0] == Operation.ADD &&
            ops[1] == Operation.SUB &&
            ops[2] == Operation.MULT)
        {
            int mult = c * d;
            answer = a + b - mult;
        }
        else if (ops[0] == Operation.SUB &&
                 ops[1] == Operation.ADD &&
                 ops[2] == Operation.MULT)
        {
            int mult = c * d;
            answer = a - b + mult;
        }
        else if (ops[0] == Operation.ADD &&
                 ops[1] == Operation.MULT &&
                 ops[2] == Operation.SUB)
        {
            int mult = b * c;
            answer = a + mult - d;
        }
        else if (ops[0] == Operation.SUB &&
                 ops[1] == Operation.MULT &&
                 ops[2] == Operation.ADD)
        {
            int mult = b * c;
            answer = a - mult + d;
        }
        else if (ops[0] == Operation.MULT &&
                 ops[1] == Operation.ADD &&
                 ops[2] == Operation.SUB)
        {
            int mult = a * b;
            answer = mult + c - d;
        }
        else if (ops[0] == Operation.MULT &&
                 ops[1] == Operation.SUB &&
                 ops[2] == Operation.ADD)
        {
            int mult = a * b;
            answer = mult - c + d;
        }

        return answer;
    }
}
