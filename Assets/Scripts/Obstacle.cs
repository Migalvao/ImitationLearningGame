using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float upperLimit = 6;
    public float lowerLimit = -6;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = this.transform.localPosition;
        position.z = Random.Range(lowerLimit, upperLimit);
        this.transform.localPosition = position;
    }

    public void Reset()
    {
        Vector3 position = this.transform.localPosition;
        position.z = Random.Range(lowerLimit, upperLimit);
        this.transform.localPosition = position;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
