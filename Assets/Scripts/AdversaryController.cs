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
        penDraw.isActive = true;
        adversaryPen.transform.position = startPos1;
        penDraw.forceDraw = true;
        yield return StartCoroutine(MovePenWithWiggle(endPos1));
        penDraw.forceDraw = false;
        yield return new WaitForSeconds(0.2f);

        yield return StartCoroutine(MovePenToPosition(startPos2));
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

    public IEnumerator ChasePlayer()
    {
        float startTime = Time.time;
        while (true)
        {
            penDraw.isActive = true;
            penDraw.forceDraw = true;
            adversaryPen.transform.position = Vector3.MoveTowards(adversaryPen.transform.position, playerPen.transform.position, 0.1f);
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