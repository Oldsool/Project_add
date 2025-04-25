using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckPoint : MonoBehaviour
{
    List <CheckpointSingle> checkpointSinglesList;

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
    }

    public void NorakThroughtCheckpoint(CheckpointSingle checkpointSingle)
    {
        Debug.Log(checkpointSinglesList.IndexOf(checkpointSingle));
    }
    
}
