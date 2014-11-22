using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public const string typeName = "BoxLock";
	public const string gameName = "TheOnlyRoom";
	public GameObject playerPrefab;
	private HostData[] hostList;
	
	void StartServer() {
		Network.InitializeServer (2, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost (typeName, gameName);
	}

	void OnServerInitialized() {
		SpawnPlayer ();
	}

	private void SpawnPlayer() {
		var startingPos = new Vector3 (0f, 1f, 0f);
		Network.Instantiate (playerPrefab, startingPos, Quaternion.identity, 0);
	}

	void OnGUI()
	{
		if (!Network.isClient && !Network.isServer)
		{
			if (GUI.Button(new Rect(100, 100, 150, 20), "Start Server"))
				StartServer();
			
			if (GUI.Button(new Rect(100, 150, 150, 20), "Refresh Hosts"))
				RefreshHostList();
			
			if (hostList != null)
			{
				for (int i = 0; i < hostList.Length; i++)
				{
					if (GUI.Button(new Rect(400, 100 + (30 * i), 160, 20), hostList[i].gameName))
						JoinServer(hostList[i]);
				}
			}
		}
	}
		
	private void RefreshHostList()
	{
		MasterServer.RequestHostList(typeName);
	}
	
	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
	}
	
	private void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}
	
	void OnConnectedToServer()
	{
		Debug.Log("Server Joined");
	}
}
