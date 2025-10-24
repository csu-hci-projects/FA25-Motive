using System.Collections;
using UnityEngine;

public class EnvelopeDrop : MonoBehaviour
{
    public float speed = 1.5f;
    public Vector3 targetPos = new Vector3(92, -82, 0);
    private Vector3 startPos;
    public RectTransform envelope;

    // Update is called once per frame

    void Start()
    {
        startPos = new Vector3(92, 687, 0);
        envelope.anchoredPosition = startPos;
    }
    void Update()
    {
        envelope.anchoredPosition = Vector3.MoveTowards(envelope.anchoredPosition, targetPos, speed * Time.deltaTime);
    }
}
