using FishNet.Connection;
using FishNet.Object;
using FishNet.Transporting;
using UnityEngine;

public class ServerBroadcaster : NetworkBehaviour
{

    public override void OnStartServer()
    {
        Debug.Log("On start server - broadcaster");
        base.OnStartServer();

        ServerManager.RegisterBroadcast<ChatMsg>(OnChatMsg);
    }

    public override void OnStopServer()
    {
        base.OnStopServer();

        ServerManager.UnregisterBroadcast<ChatMsg>(OnChatMsg);
    }

    private void OnChatMsg(NetworkConnection conn, ChatMsg msg, Channel channel = Channel.Reliable)
    {

        Debug.Log($"Client {conn.ClientId} sent msg: {msg.Text}");

        msg.Text = $"{conn.ClientId}: {msg.Text}";

        ServerManager.Broadcast(msg);

    }

}
