using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the SpawnManager and it allows
/// the player to spawn into the multiplayer game.
/// </summary>

public class SpawnScript : MonoBehaviour 
{
	//Used to determine if the palyer needs to spawn into
	//the game.
	
	private bool justConnectedToServer = false;
	
	//Used to define the JoinTeamWindow.
	
	private Rect joinRect;
	
	private string joinWindowTitle = "Team Selection";
	
	private int joinWindowWidth = 330;	
	private int joinWindowHeight = 100;
	
	private int joinLeftIndent;	
	private int joinTopIndent;
	
	private int buttonHeight = 40;
		
	//The Player prefabs are connected to these in the 
	//inspector	
	public Transform Player;
	private int group = 0;
	
	
	//Used to capture spawn points.
	private GameObject[] spawnPoints;

	void OnConnectedToServer ()
	{
		justConnectedToServer = true;	
	}
	
	void JoinTeamWindow (int windowID)
	{
		//If the player clicks on the Join Red Team button then
		//assign them to the red team and spawn them into the game.
		
		if(GUILayout.Button("Join", GUILayout.Height(buttonHeight)))
		{
			justConnectedToServer = false;
			SpawnPlayer();
		}
	}
	
	
	void OnGUI()
	{
		//If the player has just connected to the server then draw the 
		//Join Team window.
		
		if(justConnectedToServer == true)
		{
			joinLeftIndent = Screen.width / 2 - joinWindowWidth / 2;			
			joinTopIndent = Screen.height / 2 - joinWindowHeight / 2;	

			joinRect = new Rect(joinLeftIndent, joinTopIndent,
			                        joinWindowWidth, joinWindowHeight);		

			joinRect = GUILayout.Window(0, joinRect, JoinTeamWindow,
			                                joinWindowTitle);
		}
	}
	
	
	void SpawnPlayer ()
	{
		//Find all spawns points and place a reference to them in the array
		//redSpawnPoints.
		
		spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
		
		
		//Randomly select one of those spawn points.
		
		GameObject randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
		
		
		//Instantiate the player at the randomly selected spawn point.
		Network.Instantiate(Player, randomSpawn.transform.position,
		                    randomSpawn.transform.rotation, group);
	}
}
