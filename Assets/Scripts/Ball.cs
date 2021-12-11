using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.localPosition = new Vector3(-8, 3, Random.Range(-6, 6));
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void resetBall()
    {
        gameObject.SetActive(false);
        transform.localPosition = new Vector3(-8, 3, Random.Range(-6, 6));
    }

    public bool ballExists()
    {
        return gameObject.activeSelf;
    }

    public void spawnBall()
    {
        gameObject.SetActive(true);
        rb.velocity = new Vector3(3, 0, 0);
    }
    public void TouchBall()
    {
        gameObject.SetActive(false);
    }

    public GameObject GetLastBallTransform()
    {
        return gameObject;
    }
}
