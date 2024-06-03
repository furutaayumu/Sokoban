using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Move : MonoBehaviour
{
    private float timeTaken = 0.2f;
    private float timeErapsed;
    private Vector3 destination;
    private Vector3 origin;

    private void Start()
    {
        destination = transform.position;
        origin = destination;
    }

    public void MoveTo(Vector3 newDestination)
    {
        timeErapsed = 0;
        origin = destination;
        transform.position = origin;
        destination = newDestination;
    }
    // Start is called before the first frame update


    // Update is called once per frame
    private void Update()
    {
        if(origin == destination) { return;}
        timeErapsed += Time.deltaTime;
        float timeRate = timeErapsed / timeTaken;
        if (timeRate > 1) { timeRate = 1; }
        float easing = timeRate;
        Vector3 currentPosition = Vector3.Lerp(origin, destination, easing);
        transform.position = currentPosition;
    }
}
