using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class MyNetworkManager : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);

        MyNetworkPlayer player = conn.identity.GetComponent<MyNetworkPlayer>();
        
        player.SetDisplayName($"Player {numPlayers}");

        float randomR = UnityEngine.Random.Range(0, 255);
        float randomG = UnityEngine.Random.Range(0, 255);
        float randomB = UnityEngine.Random.Range(0, 255);
        
        player.SetTeamColour(new Color(randomR/255, randomG/255, randomB/255));
        
        Debug.Log($"There are now: {numPlayers}");
    }
}
