using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckPoint : MonoBehaviour
{
    public event Action OnPlayerCorrectCheckpoint;
    public event Action OnPlayerWrongCheckpoint;

    List <CheckpointSingle> checkpointSinglesList;
    private int nextCheckpointSingleIndex;

    void Awake()
    {
        Transform checkpointTransform = transform.Find("Checkpoints");

        checkpointSinglesList = new List <CheckpointSingle>();
        foreach ( Transform  checkpointSingleTransform in checkpointTransform)
        {
            CheckpointSingle checkpointSingle = checkpointSingleTransform.GetComponent<CheckpointSingle>();
            checkpointSingle.SetTrackCheckpoints(this);
            checkpointSinglesList.Add(checkpointSingle);
        }

        

        nextCheckpointSingleIndex = 0;
    }

    public void NorakThroughtCheckpoint(CheckpointSingle checkpointSingle)
    {
        if (checkpointSinglesList.IndexOf(checkpointSingle) == nextCheckpointSingleIndex)
        {
            //correct
            nextCheckpointSingleIndex = (nextCheckpointSingleIndex) + 1 % checkpointSinglesList.Count;
        
            Debug.Log(nextCheckpointSingleIndex);
            OnPlayerCorrectCheckpoint?.Invoke();// this, EventArgs.Empty);
        }
        else
        {
            //uncorrect
            Debug.Log(nextCheckpointSingleIndex);
            OnPlayerWrongCheckpoint?.Invoke();// this, EventArgs.Empty);

        }
    }
    

}
