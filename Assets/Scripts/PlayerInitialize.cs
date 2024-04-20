using UnityEngine;
using FishNet.Object;

public class PlayerInitialize : NetworkBehaviour
{
    [SerializeField] CameraMovement cameraMovement;
    [SerializeField] MainCanvas mainCanvas;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!base.IsOwner)
        {
            GetComponent<PlayerInitialize>().enabled = false;
            return;
        }
        cameraMovement.Init();
        mainCanvas.Init();
    }
}
