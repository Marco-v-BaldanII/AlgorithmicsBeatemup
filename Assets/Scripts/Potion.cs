using System.Collections;
using UnityEngine;

public class Potion : MonoBehaviour
{

    public Vector2 destination;
    public Vector2 startPoint;
    public float timeToMove = 1f;

    private float timePassed = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(MoveUp());
    }

    private IEnumerator MoveUp()
    {
        startPoint = transform.position;
        destination = transform.position + new Vector3(0, 2);

        

        while (timePassed <= timeToMove)
        {
            timePassed += Time.deltaTime;
            float t = timePassed / timeToMove;

            transform.position = Vector2.Lerp(startPoint, destination, t);
            yield return null;
        }
    }

}
