using Grpc.Core;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine.UIElements;
using UnityEngine;
using System.Collections.Generic;
using System;

public class Norak_MLAgent : Agent
{
    TrackCheckPoint trackCheckPoint;
    Vector3 NorakPosition;

    private List<CheckpointSingle> checkpointList;

    private void Awake()
    {
        //checkpointSingle = GetComponent<CheckpointSingle>();
        trackCheckPoint = FindObjectOfType<TrackCheckPoint>();
        NorakPosition = transform.position;
        //checkpointTransform = GameObject.Find("Checkpoints");

        //foreach (Transform checkpointSingleTransform in checkpointTransform.transform)
        //{
        //    CheckpointSingle checkpointSingle = checkpointSingleTransform.GetComponent<CheckpointSingle>();
        //    checkpointList.Add(checkpointSingle);
        //    Debug.Log(checkpointList.Count);
        //}
        
    }

    private void Start()
    {
        trackCheckPoint.OnPlayerCorrectCheckpoint += TrackCheckpoint_OnCarCorrectCheckpoint;
        trackCheckPoint.OnPlayerWrongCheckpoint += TrackCheckpoint_OnCarWrongCheckpoint;
    }

    void TrackCheckpoint_OnCarCorrectCheckpoint()// object sender, EventArgs e)
    {
        //Debug.Log("������� ���������!");
        AddReward(+1);
    }

    void TrackCheckpoint_OnCarWrongCheckpoint()
    {
        //Debug.Log("������� ��������� ������� !");
        AddReward(-1);
        //EndEpisode();
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
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CheckpointSingle>(out CheckpointSingle checkpointSingle))
        {
            //backGround.material.color = Color.green;
            //AddReward(+5f);
            //TrackCheckpoint_OnCarCorrectCheckpoint();
            //Debug.Log("Correct");
           // trackCheckPoint.NorakThroughtCheckpoint(checkpointSingle);
            //trackCheckPoint.NorakThroughtCheckpoint(checkpointSingle);
            //EndEpisode();

        }
        else if (other.TryGetComponent<Wall>(out Wall wall))
        {
            //backGround.material.color = Color.red;
            AddReward(-1f);

            Debug.Log("��������� �����");
            EndEpisode();
            //trackCheckPoint.NorakThroughtCheckpoint(checkpoint);

        }

    }
}
