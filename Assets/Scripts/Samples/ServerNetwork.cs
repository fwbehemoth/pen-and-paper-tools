using System.Collections;
using UnityEngine;

public class ServerNetwork : MonoBehaviour {

    private int listenPort = 25000;
    private string _messageLog = "";
    private bool useNAT = false;
    private string someInfo = "Server: hello client";

    public void Awake() 
    {
        if (Network.peerType == NetworkPeerType.Disconnected)
            // Creating server 
         Network.InitializeServer(2, listenPort, useNAT); 
    }

    public void OnGUI() 
    {
       //If this is the server
        if (Network.peerType == NetworkPeerType.Server) 
       {
         //Show clients number
            GUI.Label(new Rect(100, 100, 150, 25), "Server");
            GUI.Label(new Rect(100, 125, 150, 25), "Clients attached: " + Network.connections.Length);

         //Quit server
            if (GUI.Button(new Rect(100, 150, 150, 25), "Quit server")) 
         {
                Network.Disconnect();
                Application.Quit();
            }
            if (GUI.Button(new Rect(100, 175, 150, 25), "Send Hello to client"))
                SendInfoToClient();

         // Getting your ip address and port 
         string ipaddress = Network.player.ipAddress; 
         string port = Network.player.port.ToString();
         GUI.Label(new Rect(100,50,250,40),"IP Adress: "+ipaddress+":"+port); 
        }
        GUI.TextArea(new Rect(275, 100, 300, 300), _messageLog);
    }

    void OnPlayerConnected(NetworkPlayer player) 
    {
        AskClientForInfo(player);
    }

    void AskClientForInfo(NetworkPlayer player) 
    {
        GetComponent<NetworkView>().RPC("SetPlayerInfo", player, player);
    }

    [RPC]
    void ReceiveInfoFromClient(string someInfo) 
    {
        _messageLog += someInfo + "\n";
    }

    [RPC]
    void SendInfoToClient() 
    {
        GetComponent<NetworkView>().RPC("ReceiveInfoFromServer", RPCMode.Others, someInfo);
    }

    [RPC]
    void SendInfoToServer() { }
    [RPC]
    void SetPlayerInfo(NetworkPlayer player) { }
    [RPC]
    void ReceiveInfoFromServer(string someInfo) { }
}