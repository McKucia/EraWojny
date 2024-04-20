using UnityEngine;
using FishNet.Object;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class PlayerUnitSpawner : NetworkBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] NetworkObject unitObject;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!base.IsOwner)
            GetComponent<PlayerUnitSpawner>().enabled = false;
    }

    public void SpawnInfantry()
    {
        if (!base.IsOwner) return;
        SpawnObject(unitObject, transform, this);
    }

    [ServerRpc]
    public void SpawnObject(NetworkObject obj, Transform player, PlayerUnitSpawner script)
    {
        NetworkObject nob = Instantiate<NetworkObject>(obj, player.position + player.forward, player.transform.rotation);
        nob.gameObject.GetComponent<Unit>().PlayerCamera = playerCamera;

        UnitySceneManager.MoveGameObjectToScene(nob.gameObject, gameObject.scene);

        ServerManager.Spawn(nob, base.Owner);

        SetSpawnedObject(nob, script);
    }

    [ObserversRpc]
    public void SetSpawnedObject(NetworkObject spawned, PlayerUnitSpawner script)
    {
        UnitySceneManager.MoveGameObjectToScene(spawned.gameObject, gameObject.scene);
        spawned.gameObject.GetComponent<Unit>().PlayerCamera = playerCamera;
    }

    [ServerRpc(RequireOwnership = false)]
    public void DespawnObject(NetworkObject obj)
    {
        ServerManager.Despawn(obj);
    }
}
