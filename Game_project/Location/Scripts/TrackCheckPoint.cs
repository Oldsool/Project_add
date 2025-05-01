using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckPoint : MonoBehaviour
{
    public event Action OnPlayerCorrectCheckpoint;
    public event Action OnPlayerWrongCheckpoint;
    CheckpointSingle check;
    List <CheckpointSingle> checkpointSinglesList;
    Transform checkpointTransform;
    public int nextCheckpointSingleIndex;

    public int countCheck;

    void Awake()
    {
        checkpointTransform = transform.Find("Checkpoints");

        checkpointSinglesList = new List <CheckpointSingle>();
        foreach ( Transform  checkpointSingleTransform in checkpointTransform)
        {
            CheckpointSingle checkpointSingle = checkpointSingleTransform.GetComponent<CheckpointSingle>();
            checkpointSingle.SetTrackCheckpoints(this);
           
            checkpointSinglesList.Add(checkpointSingle);
        }


        
        nextCheckpointSingleIndex = 0;

    }

    public Transform GetNextCheckpoint(int count)
    {
        countCheck = nextCheckpointSingleIndex;
        
            check = checkpointSinglesList[count];

        return check.transform;
    }


    public void NorakThroughtCheckpoint(CheckpointSingle checkpointSingle)
    {
        if (checkpointSinglesList.IndexOf(checkpointSingle) == nextCheckpointSingleIndex)
        {
            //correct
            nextCheckpointSingleIndex = (nextCheckpointSingleIndex) + 1 % checkpointSinglesList.Count;
        
            OnPlayerCorrectCheckpoint?.Invoke();
            if (nextCheckpointSingleIndex == 39) 
            {
                nextCheckpointSingleIndex = 0;
            }
        }
        else
        {
            //uncorrect
            OnPlayerWrongCheckpoint?.Invoke();

        }
    }
    

}
