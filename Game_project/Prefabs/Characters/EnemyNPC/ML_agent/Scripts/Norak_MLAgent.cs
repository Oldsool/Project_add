using Grpc.Core;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine.UIElements;
using UnityEngine;
using System.Collections.Generic;

public class Norak_MLAgent : Agent
{
    private TrackCheckPoint trackCheckPoint;
    public CheckpointSingle checkpointSingle;
    private Vector3 NorakPosition;
    private GameObject checkpointTransform;

    private List<CheckpointSingle> checkpointList;

    private void Awake()
    {
        checkpointSingle = GetComponent<CheckpointSingle>();
        trackCheckPoint = GetComponent<TrackCheckPoint>();
         NorakPosition = transform.position;
         checkpointTransform = GameObject.Find("Checkpoints");

        foreach (Transform checkpointSingleTransform in checkpointTransform.transform)
        {
            CheckpointSingle checkpointSingle = checkpointSingleTransform.GetComponent<CheckpointSingle>();
            checkpointList.Add(checkpointSingle);
            Debug.Log(checkpointList.Count);
        }
    }

    public override void OnEpisodeBegin() // ������ ������ �������
    {
        transform.localPosition = NorakPosition;
        //foreach (CheckpointSingle checkpointSingleTransform in checkpointList)
        //{
        //    checkpointSingleTransform.transform.localPosition = ;
        //}

            //target.localPosition = new Vector3(1, -1, -1);
        }

    /// <summary>
    /// ��������� � �� �������. ��� ������ �� ����� �� ���������. ��� ����� ������ ������ 
    /// ����� �� ���������� ��� ��������, � ���� ��� �� ������, �� ����, �� ����. 
    /// ��, ��� �� ����� "������" � ��� ��, ��� �� ������� ����� CollectObservations().
    /// </summary>

    public override void CollectObservations(VectorSensor sensor)  //������� ����� ����������, ����� ������
    {
        //foreach (CheckpointSingle checkpointSingleTransform in checkpointList)
        //{
        //    //checkpointSingleTransform.transform.localPosition = ;
        //    sensor.AddObservation(checkpointSingleTransform.transform.localPosition);
        //}
        //sensor.AddObservation(target.localPosition);   // ����� ����������
        sensor.AddObservation(transform.localPosition); //3 
    }



    /// <summary>
    /// ��� ������: ����� �������������� ��������, ������� ��������� "������" ������.�� ���������� ��� �������� � �������(�������� ���, �������� � �.�.)
    ///  ����� ����� �������� ������� � ������� ���������� �������.
    /// </summary>
    // ���������� ����� ML agent ����� ��������� ��������
    public override void OnActionReceived(ActionBuffers actions)   // �������� �������� �� ������ � ��������� �� � ������. , ���� � ���� ������
    {
        float moveX = actions.ContinuousActions[0]; // actions.ContinuousActions - ��� �������� ������������ ���� �� ������� (0)
        float moveY = actions.ContinuousActions[1];

        float SpeedMovement = 1.0f;

        transform.localPosition += new Vector3(moveX, 0, moveY) * (SpeedMovement * Time.deltaTime);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Vertical");
        continuousActions[1] = Input.GetAxisRaw("Horizontal");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CheckpointSingle>(out CheckpointSingle checkpointSingle))
        {
            //backGround.material.color = Color.green;
            AddReward(+5f);
            
            Debug.Log("Correct");
            trackCheckPoint.NorakThroughtCheckpoint(checkpointSingle);
            EndEpisode();

        }
        else if (other.TryGetComponent<Wall>(out Wall wall))
        {
            //backGround.material.color = Color.red;
            AddReward(-1f);
            
            Debug.Log("Uncorrect");
            EndEpisode();
            //trackCheckPoint.NorakThroughtCheckpoint(checkpoint);

        }

    }
}
