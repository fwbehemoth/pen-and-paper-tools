using System.Collections;
using UnityEngine;

public class ClientNetwork : MonoBehaviour {

    private string remoteIP = "127.0.0.1";
    private int remotePort = 25000;
    private string _messageLog = "";
    private string someInfo = "";
    private NetworkPlayer _myNetworkPlayer;

    void OnGUI() 
    {
        if (Network.peerType == NetworkPeerType.Disconnected) 
       {
         remoteIP = GUI.TextField(new Rect(100,50,100,20),remoteIP); 
         remotePort = int.Parse(GUI.TextField(new Rect(100,75,40,20),remotePort.ToString())); 
         //Conect to server
            if (GUI.Button(new Rect(100, 100, 150, 25), "Connect")) 
         {
                Network.Connect(remoteIP, remotePort);
            }
        } 
       else 
       {
         //If this is the client
            if (Network.peerType == NetworkPeerType.Client) 
         {
                GUI.Label(new Rect(100, 100, 150, 25), "Client");

                if (GUI.Button(new Rect(100, 125, 150, 25), "Logout"))
                    Network.Disconnect();

                if (GUI.Button(new Rect(100, 150, 150, 25), "Send Hello to server"))
                    SendInfoToServer();
            }
        }

        GUI.TextArea(new Rect(275, 100, 300, 300), _messageLog);
    }

    [RPC]
    void SendInfoToServer()
    {
        someInfo = "Client " + _myNetworkPlayer.guid + ": Hello Server";
        GetComponent<NetworkView>().RPC("ReceiveInfoFromClient", RPCMode.Server, someInfo);
    }
    [RPC]
    void SetPlayerInfo(NetworkPlayer player) 
    {
        _myNetworkPlayer = player;
        someInfo = "Player setted";
        GetComponent<NetworkView>().RPC("ReceiveInfoFromClient", RPCMode.Server, someInfo);
    }

    [RPC]
    void ReceiveInfoFromServer(string someInfo) 
    {
        _messageLog += someInfo + "\n";
    }

    void OnConnectedToServer() {
        _messageLog += "Connected to server" + "\n";
    }
    void OnDisconnectedToServer() {
        _messageLog += "Disco from server" + "\n";
    }


    [RPC]
    void ReceiveInfoFromClient(string someInfo) { }
    [RPC]
    void SendInfoToClient(string someInfo) { }
}
