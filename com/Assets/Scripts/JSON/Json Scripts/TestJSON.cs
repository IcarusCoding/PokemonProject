using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public struct NPC
{
	public string name;
	public int id;
	public string image;

	public List<Event> ListOfEvents;
}

public struct Event
{
	public string dialog;
	public bool postYieldDialog;

	public Vector3 destination;
	public Vector3 rotation;
	
	public bool yieldNextEvent;
}

public class TestJSON : MonoBehaviour 
{
	//An empty placeholder in which all npc's derive from
	public GameObject BaseNPC = null;

	IEnumerator Start()
	{
		//Load JSON data from a URL
		string url = "https://www.millsaj.com/example.json";
		WWW www = new WWW(url);
		
		//Load the data and yield (wait) till it's ready before we continue executing the rest of this method.
		yield return www;
		if (www.error == null)
		{			
			Debug.Log("fdsa");
			//Process npc data found in JSON file
			ProcessNPC(www.text);
		}
		else
		{
			Debug.Log("ERROR: " + www.error);
		}		
	}
	
	//Converts a JSON string into NPC objects and then spawns them accordingly
	private void ProcessNPC(string jsonString)
	{
		JsonData jsonNPC = JsonMapper.ToObject(jsonString);
		NPC npc;
		Event _event;

		for(int i = 0; i<jsonNPC["NPC"].Count; i++)
		{
			npc = new NPC();
			npc.ListOfEvents = new List<Event>();

			npc.name = jsonNPC["NPC"][i]["name"].ToString();
			npc.id = System.Convert.ToInt16(jsonNPC["NPC"][i]["id"].ToString());
			npc.image = jsonNPC["NPC"][i]["image"].ToString();

			for(int o = 0; o<jsonNPC["NPC"][i]["EVENTS"]["event"].Count; o++)
			{
				_event = new Event();
				_event.dialog = jsonNPC["NPC"][i]["EVENTS"]["event"][o]["dialog"].ToString();
				npc.ListOfEvents.Add(_event);
			}
		}	
	}
	
	//Creates an NPC based on the information gained from the JSON Script
	private void LoadNPC(NPC aNPC, Event aEvent)
	{
		GameObject npcGameObject = Instantiate (BaseNPC, new Vector3 (0, 3, 0), Quaternion.identity) as GameObject;
		NPCController npcScript = npcGameObject.GetComponent<NPCController> ();

		npcScript.CreateNPC ();

		npcScript.Name = aNPC.name;
		npcScript.ID = aNPC.id;

		npcScript.taskController.AddDialogTask (aNPC.ListOfEvents[0].dialog, true);
		//The texture url is a test dummy atm, once accual design is implemented, please refer to npc.image
		StartCoroutine(npcScript.BaseSprite.UpdateTexture ("http://millsaj.com/images/NPC/NPC " + System.String.Format("{0:00}", (int)Random.Range(1,19)) + ".png"));
	}
}
