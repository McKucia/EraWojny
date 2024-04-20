using FishNet.Component.Prediction;
using FishNet.Object.Prediction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MoveData : IReplicateData
{
    public bool Inverted;

    public MoveData(bool inverted)
    {
        Inverted = inverted;
        _tick = 0;
    }

    private uint _tick;
    public void Dispose() { }
    public uint GetTick() => _tick;
    public void SetTick(uint value) => _tick = value;
}

public struct ReconcileData : IReconcileData
{
    public RigidbodyState RigidbodyState;
    public PredictionRigidbody PredictionRigidbody;

    public ReconcileData(PredictionRigidbody pr)
    {
        RigidbodyState = new RigidbodyState(pr.Rigidbody);
        PredictionRigidbody = pr;
        _tick = 0;
    }

    private uint _tick;
    public void Dispose() { }
    public uint GetTick() => _tick;
    public void SetTick(uint value) => _tick = value;
}