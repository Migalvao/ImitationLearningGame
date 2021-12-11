using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class AgentSetup : Agent
{
    //target postion
    [SerializeField] private Button button;
    [SerializeField] private Ball ball;
    [SerializeField] private Obstacle[] obstacles;
    public Result result;

    public float speed = 0.1f;

    //public Ball ball;
    //public Button button;

    public override void OnEpisodeBegin()
    {
        transform.localPosition = Vector3.zero;
        button.resetButton();
        ball.resetBall();

        foreach (Obstacle o in obstacles)
        {
            o.Reset();
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(button.CanPressButton() ? 1 : 0);

        if (button.CanPressButton())
        {
            Vector3 dirToButton = (button.transform.localPosition - transform.localPosition).normalized;
            sensor.AddObservation(dirToButton.x);
            sensor.AddObservation(dirToButton.z);
        }
        else
        {
            sensor.AddObservation(0);
            sensor.AddObservation(0);
        }

        sensor.AddObservation(ball.ballExists() ? 1 : 0);

        if (ball.ballExists())
        {
            //Ball has spawned
            Vector3 dirToBall = (ball.GetLastBallTransform().transform.localPosition - transform.localPosition).normalized;
            sensor.AddObservation(dirToBall.x);
            sensor.AddObservation(dirToBall.z);
        }
        else
        {
            //Ball hasnt spawned
            sensor.AddObservation(0f);
            sensor.AddObservation(0f);
        }

        foreach (Obstacle o in obstacles)
        {
            Vector3 dirToObstacle = (o.transform.localPosition - transform.localPosition).normalized;
            sensor.AddObservation(dirToObstacle.x);
            sensor.AddObservation(dirToObstacle.z);
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        // Debug.Log("HORIZONTAL " + Input.GetAxisRaw("Horizontal").ToString());
        // Debug.Log("VERTICAL " + Input.GetAxisRaw("Vertical").ToString());
        switch (Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")))
        {
            case -1: discreteActions[0] = 1; break;
            case 0: discreteActions[0] = 0; break;
            case 1: discreteActions[0] = 2; break;
        }
        switch (Mathf.RoundToInt(Input.GetAxisRaw("Vertical")))
        {
            case -1: discreteActions[1] = 1; break;
            case 0: discreteActions[1] = 0; break;
            case 1: discreteActions[1] = 2; break;
        }

        discreteActions[2] = Input.GetKey(KeyCode.Return) ? 1 : 0;
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        /*
        Debug.Log("movimento x " + actions.DiscreteActions[0].ToString());
        Debug.Log("movimento z " + actions.DiscreteActions[1].ToString());
        Debug.Log("movimento botao " + actions.DiscreteActions[2].ToString());  
        */
        int moveX = actions.DiscreteActions[0];
        int moveZ = actions.DiscreteActions[1];

        Vector3 addForce = new Vector3(0, 0, 0);

        switch (moveX)
        {
            case 0: addForce.x = 0f; ; break;
            case 1: addForce.x = -1f; break;
            case 2: addForce.x = +1f; break;
        }

        switch (moveZ)
        {
            case 0: addForce.z = 0f; break;
            case 1: addForce.z = -1f; break;
            case 2: addForce.z = +1f; break;
        }


        this.transform.position += addForce * speed;

        bool isButtonPressed = actions.DiscreteActions[2] == 1;
        if (isButtonPressed)
        {
            Collider[] colliderArray = Physics.OverlapBox(this.transform.position, Vector3.one);
            foreach (Collider collider in colliderArray)
            {

                if (collider.TryGetComponent<Button>(out Button but))
                {
                    if (but.CanPressButton())
                    {
                        but.PressButton();
                        AddReward(1f);
                    }
                }
            }
        }
        // AddReward(-1f / MaxStep);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Ball>(out Ball ball))
        {
            ball.TouchBall();
            AddReward(2f);
            result.setSuccessMaterial();
            EndEpisode();
        }
        else if (collision.gameObject.tag == "wall")
        {
            AddReward(-1f);
            result.setFailedMaterial();
            EndEpisode();
        }
        else if (collision.gameObject.tag == "obstacle")
        {
            AddReward(-0.8f);
            result.setFailedMaterial();
            EndEpisode();
        }
    }
}
