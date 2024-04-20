using FishNet.Object;
using FishNet.Transporting;
using TMPro;
using UnityEngine;

public class EchoCanvas : NetworkBehaviour
{
    [SerializeField] private TMP_Text _text = null;
    [SerializeField] private TMP_InputField _input = null;

    private void Awake()
    {
        _text.text = "";

        _input.onEndEdit.AddListener((text) =>
        {
            if (!ClientManager.Started) return;
            if (string.IsNullOrWhiteSpace(_input.text)) return;

            ClientManager.Broadcast(new ChatMsg
            {
                Text = _input.text
            });

            _input.text = "";

        });
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        ClientManager.RegisterBroadcast<ChatMsg>(OnChatMsg);
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
        
        ClientManager.UnregisterBroadcast<ChatMsg>(OnChatMsg);
    }

    private void OnChatMsg(ChatMsg msg, Channel channel = Channel.Reliable)
    {
        _text.text += msg.Text + "\n";
    }
}
