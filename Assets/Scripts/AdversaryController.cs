using UnityEngine;
using System.Collections;
public class AdversaryController : MonoBehaviour
{
    public float xSize = 1.0f;
    public float randomnessFactor = 0.1f;
    public float wiggleFactor = 0.1f;
    public GameObject adversaryPen;
    public GameObject playerPen;
    public Vector3[] restPositions = new Vector3[3];

    public float scribbleDuration = 3.0f;
    public float scribbleStepSpeed = 0.025f;
    public float scribbleRadius = 15.0f;

    public int attackNumCharges = 3;
    public float attackRadius = 2.0f;
    public float attackWithdrawSpeed = 0.05f;
    public float attackChargeSpeed = 0.2f;

    [HideInInspector]
    public int chargeCount = 0;
    private Draw penDraw;

    void Start()
    {
        penDraw = adversaryPen.GetComponent<Draw>();
        penDraw.isActive = false;
        penDraw.forceDraw = false;
    }

    public IEnumerator DrawXAtPosition(Vector3 position)
    {
        penDraw.isActive = false;
        penDraw.forceDraw = false;


        Vector3 startPos1 = position + new Vector3(-xSize, xSize, 0) + GetRandomOffset();
        Vector3 endPos1 = position + new Vector3(xSize, -xSize, 0) + GetRandomOffset();
        Vector3 startPos2 = position + new Vector3(-xSize, -xSize, 0)   + GetRandomOffset();
        Vector3 endPos2 = position + new Vector3(xSize, xSize, 0)   + GetRandomOffset();
        yield return StartCoroutine(MovePenToPosition(startPos1));
        // wait 1 second
        yield return new WaitForSeconds(1.0f);
        penDraw.isActive = true;
        adversaryPen.transform.position = startPos1;
        penDraw.forceDraw = true;
        yield return StartCoroutine(MovePenWithWiggle(endPos1));
        penDraw.forceDraw = false;
        yield return new WaitForSeconds(0.2f);

        yield return StartCoroutine(MovePenToPosition(startPos2));
        yield return new WaitForSeconds(1.0f);

        penDraw.forceDraw = true;
        yield return StartCoroutine(MovePenWithWiggle(endPos2));
        penDraw.forceDraw = false;
        penDraw.isActive = false;
        penDraw.isWriting = false;
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(RestPen());
    }

    public IEnumerator RestPen()
    {
        Vector3 target = restPositions[Random.Range(0, restPositions.Length)];
        float startTime = Time.time;
        while (Time.time - startTime < 1f)
        {
            adversaryPen.transform.position = Vector3.MoveTowards(adversaryPen.transform.position, target, 0.1f);
            yield return new WaitForSeconds(0.01f);
        }
    }

    // public IEnumerator ChasePlayer()
    // {
    //     float startTime = Time.time;
    //     while (true)
    //     {
    //         penDraw.isActive = true;
    //         penDraw.forceDraw = true;
    //         adversaryPen.transform.position = Vector3.MoveTowards(adversaryPen.transform.position, playerPen.transform.position, 0.1f);
    //         yield return new WaitForSeconds(0.01f);
    //     }
    // }

    // https://discussions.unity.com/t/normal-distribution-random/66530
    public static float RandomGaussian(float minValue = 0.0f, float maxValue = 1.0f)
    {
        float u, v, S;

        do
        {
            u = 2.0f * UnityEngine.Random.value - 1.0f;
            v = 2.0f * UnityEngine.Random.value - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0f);

        // Standard Normal Distribution
        float std = u * Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);

        // Normal Distribution centered between the min and max value
        // and clamped following the "three-sigma rule"
        float mean = (minValue + maxValue) / 2.0f;
        float sigma = (maxValue - mean) / 3.0f;
        return Mathf.Clamp(std * sigma + mean, minValue, maxValue);
    }

public IEnumerator Scribble()
{
    Vector3 scribbleCenter = new Vector3(0, 0, 0); // Start at current position
    yield return MovePenToPosition(scribbleCenter);
    penDraw.isActive = true;
    penDraw.forceDraw = true;

    float startTime = Time.time;
    Vector3 velocity = Vector3.zero;
    Vector3 currentPosition = adversaryPen.transform.position;

    while (Time.time - startTime < scribbleDuration)
    {
        Vector3 targetPosition = scribbleCenter + new Vector3(
            RandomGaussian(-scribbleRadius, scribbleRadius),
            RandomGaussian(-scribbleRadius, scribbleRadius),
            0);

        // Reduce acceleration factor for smoother movement
        velocity += (targetPosition - currentPosition) * 0.4f; // Slower acceleration
        velocity *= 0.825f; // Higher damping to reduce overshooting
        currentPosition += velocity * (scribbleStepSpeed * 0.5f); // Scale down movement speed

        adversaryPen.transform.position = currentPosition;

        yield return new WaitForSeconds(0.02f); // Slightly slower update rate
    }

    penDraw.forceDraw = false;
    yield return new WaitForSeconds(0.2f);
}


    public IEnumerator ChasePlayer()
    {
        float startTime = Time.time;
        penDraw.isActive = false;
        penDraw.forceDraw = false;
        while (chargeCount < attackNumCharges)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-attackRadius, attackRadius), Random.Range(-attackRadius, attackRadius), 0);
            Vector3 withdrawPosition = playerPen.transform.position + randomOffset;
            
            yield return MovePenToPositionWithSpeed(withdrawPosition, attackWithdrawSpeed);
            yield return MovePenToPositionWithSpeed(playerPen.transform.position, attackChargeSpeed);
            chargeCount++;
        }
    }

    public IEnumerator WrestleWithPlayer() {
        while (true) {
            Vector3 randomOffset = new Vector3(Random.Range(-attackRadius, attackRadius), Random.Range(-attackRadius, attackRadius), 0);
            Vector3 withdrawPosition = playerPen.transform.position + randomOffset;
            
            yield return MovePenToPositionWithSpeed(withdrawPosition, attackWithdrawSpeed);
            yield return MovePenToPositionWithSpeed(playerPen.transform.position, attackChargeSpeed);
        }
    }

    private IEnumerator MovePenToPositionWithSpeed(Vector3 target, float speed)
    {
        while (Vector3.Distance(adversaryPen.transform.position, target) > 0.025f)
        {
            adversaryPen.transform.position = Vector3.MoveTowards(adversaryPen.transform.position, target, speed);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private Vector3 GetRandomOffset()
    {
        return new Vector3(Random.Range(-randomnessFactor, randomnessFactor), Random.Range(-randomnessFactor, randomnessFactor), 0);
    }

    private IEnumerator MovePenToPosition(Vector3 target)
    {
        while (Vector3.Distance(adversaryPen.transform.position, target) > 0.01f)
        {
            adversaryPen.transform.position = Vector3.MoveTowards(adversaryPen.transform.position, target, 0.1f);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator MovePenWithWiggle(Vector3 target)
    {
        while (Vector3.Distance(adversaryPen.transform.position, target) > 0.2f)
        {
            float distanceFactor = Vector3.Distance(adversaryPen.transform.position, target) / Vector3.Distance(adversaryPen.transform.position, playerPen.transform.position);
            distanceFactor = Mathf.Clamp(distanceFactor, 0, 1);
            Vector3 wiggleOffset = new Vector3(Random.Range(-wiggleFactor, wiggleFactor), Random.Range(-wiggleFactor, wiggleFactor), 0);
            wiggleOffset *= distanceFactor;
            adversaryPen.transform.position = Vector3.MoveTowards(adversaryPen.transform.position + wiggleOffset, target, 0.1f);
            yield return new WaitForSeconds(0.01f);
        }
    }
}