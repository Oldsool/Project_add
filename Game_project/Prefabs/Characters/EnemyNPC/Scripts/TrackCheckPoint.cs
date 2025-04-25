using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckPoint : MonoBehaviour
{
    List <CheckpointSingle> checkpointSingles = new List <CheckpointSingle> ();

    void Awake()
    {
        Transform checkpointTransform = transform.Find("Checkpoints");

        foreach ( Transform  checkpointSingleTransform in checkpointTransform)
        {
            CheckpointSingle checkpointSingle = checkpointSingleTransform.GetComponent<CheckpointSingle>();
            checkpointSingle.SetTrackCheckpoints(this);
        }
    }

    public void NorakThroughtCheckpoint(CheckpointSingle checkpointSingle)
    {
        Debug.Log(checkpointSingle.transform.name);
    }
    
}
