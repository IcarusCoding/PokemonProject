using UnityEngine;
using System.Collections;
using LitJson;

public struct NPC
{
	public string name;
	public int id;
	public string image;
	public ArrayList tasks;
}

public class TestJSON : MonoBehaviour 
{
	public GameObject BaseNPC = null;

	IEnumerator Start()
	{
		//Load JSON data from a URL
		string url = "http://www.millsaj.com/example.json";
		WWW www = new WWW(url);
		
		//Load the data and yield (wait) till it's ready before we continue executing the rest of this method.
		yield return www;
		if (www.error == null)
		{			
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

		//Create X number of sprites based on our JSON info
		for(int x = 0; x < 150; x++)
		{
			for(int i = 0; i<jsonNPC["NPC"].Count; i++)
			{
				npc = new NPC();
				npc.id = System.Convert.ToInt16(jsonNPC["NPC"][i]["id"].ToString());
				npc.image = jsonNPC["NPC"][i]["image"].ToString();
				npc.tasks = new ArrayList();
			
				LoadNPC(npc);
			}
		}
	}
	
	//Creates an NPC based on the information gained from the JSON Script
	private void LoadNPC(NPC npc)
	{
		GameObject npcGameObject = Instantiate (BaseNPC, new Vector3 (0, 3, 0), Quaternion.identity) as GameObject;
		NPCController npcScript = npcGameObject.GetComponent<NPCController> ();

		npcScript.Name = npc.name;
		npcScript.ID = npc.id;

		//The texture url is a test dummy atm, once accual design is implemented, please refer to npc.image
		StartCoroutine(npcScript.BaseSprite.UpdateTexture ("http://millsaj.com/images/NPC/NPC " + System.String.Format("{0:00}", (int)Random.Range(1,19)) + ".png"));
	}
}
