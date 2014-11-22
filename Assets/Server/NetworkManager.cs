using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public const string typeName = "BoxLock";
	public const string gameName = "The Room";
	public string username = "Enter your name";
	public GameObject playerPrefab;
	private HostData[] hostList;
	private Stack colorStack;
	
	void StartServer() {
		GUI.skin.textField.fontSize = 20;
		GUI.skin.textField.alignment = TextAnchor.MiddleCenter;
		Network.InitializeServer (6, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost (typeName, gameName);
		colorStack = new Stack ();
		colorStack.Push (Color.red);
		colorStack.Push (Color.blue);
		colorStack.Push (Color.yellow);
		colorStack.Push (Color.green);
		colorStack.Push (Color.magenta);
		colorStack.Push (Color.cyan);
	}

	void OnServerInitialized() {
		SpawnPlayer ();
	}

	void OnPlayerDisconnected(NetworkPlayer player) {
		Network.DestroyPlayerObjects (player);
		colorStack.Push (playerPrefab.renderer.sharedMaterial.color);
	}

	private void SpawnPlayer() {
		var startingPos = new Vector3 (5f, 5f, -5f);
		Network.Instantiate(playerPrefab, startingPos, Quaternion.identity, 0);
		playerPrefab.renderer.sharedMaterial.color = (Color) colorStack.Pop();
	}

	void OnGUI()
	{
		var midWidth = Screen.width / 2;
		var midHeight = Screen.height / 2;
		if (!Network.isClient && !Network.isServer)
		{
			username = GUI.TextField(new Rect(midWidth - 160, midHeight - 100, 320, 40), username, 25, GUI.skin.textField);

			if (GUI.Button(new Rect(midWidth - 160, midHeight - 40, 150, 20), "Start Server"))
				StartServer();
			
			if (GUI.Button(new Rect(midWidth + 10, midHeight - 40, 150, 20), "Refresh Hosts"))
				RefreshHostList();
			
			if (hostList != null)
			{
				for (int i = 0; i < hostList.Length; i++)
				{
					if (GUI.Button(new Rect(midWidth - 160, midHeight + (30 * i), 320, 20), hostList[i].gameName))
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
		SpawnPlayer();
	}
}
