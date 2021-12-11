using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    private bool canPressButton;
    public Ball ball;

    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(-8, 1, Random.Range(-6, 6));
        canPressButton = true;
    }

    public bool CanPressButton()
    {
        return canPressButton;
    }

    public void resetButton()
    {
        canPressButton = true;
        this.transform.localPosition = new Vector3(8, -1, Random.Range(-6, 6));
        gameObject.SetActive(true);
    }

    public void PressButton()
    {
        if (canPressButton)
        {
            canPressButton = false;
            gameObject.SetActive(false);

            ball.spawnBall();
        }
    }
}
