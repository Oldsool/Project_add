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
        //Debug.Log("Событие произошло!");
        AddReward(+1);
    }

    void TrackCheckpoint_OnCarWrongCheckpoint()
    {
        //Debug.Log("Событие произошло неверно !");
        AddReward(-1);
        //EndEpisode();
    }

    public override void OnEpisodeBegin() // начало нового эпизода
    {
        transform.localPosition = NorakPosition;
        //foreach (CheckpointSingle checkpointSingleTransform in checkpointList)
        //{
        //    checkpointSingleTransform.transform.localPosition = ;
        //}

            //target.localPosition = new Vector3(1, -1, -1);
        }

    /// <summary>
    /// Нейросеть — не человек. Она ничего не знает по умолчанию. Вот прямо вообще ничего 
    /// Когда ты запускаешь его обучение, у него нет ни памяти, ни глаз, ни ушей. 
    /// Всё, что он может "видеть" — это то, что ты передал через CollectObservations().
    /// </summary>

    public override void CollectObservations(VectorSensor sensor)  //функция сбора наблюдения, глаза агента
    {
        //foreach (CheckpointSingle checkpointSingleTransform in checkpointList)
        //{
        //    //checkpointSingleTransform.transform.localPosition = ;
        //    sensor.AddObservation(checkpointSingleTransform.transform.localPosition);
        //}
        //sensor.AddObservation(target.localPosition);   // набор наблюдений
        sensor.AddObservation(transform.localPosition); //3 
    }



    /// <summary>
    /// Что делает: здесь обрабатываются действия, которые нейросеть "отдает" агенту.Ты применяешь эти действия к объекту(двигаешь его, прыгаешь и т.д.)
    ///  Также здесь ставятся награды и условия завершения эпизода.
    /// </summary>
    // Вызывается когда ML agent решит применить действие
    public override void OnActionReceived(ActionBuffers actions)   // получает действия от модели и применяет их к агенту. , руки и ноги агента
    {
        float moveX = actions.ContinuousActions[0]; // actions.ContinuousActions - это действие непрерывного типа по индексу (0)
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

            Debug.Log("Коснулись стены");
            EndEpisode();
            //trackCheckPoint.NorakThroughtCheckpoint(checkpoint);

        }

    }
}
