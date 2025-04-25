using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSingle : MonoBehaviour
{
    private TrackCheckPoint trackCheckpoints;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Norak>(out Norak norak))
        {
            trackCheckpoints.NorakThroughtCheckpoint(this);
        }
    }

    public void SetTrackCheckpoints(TrackCheckPoint trackCheckpoints)
    {
        this.trackCheckpoints = trackCheckpoints;
    }
}
