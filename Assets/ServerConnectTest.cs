using UnityEngine;
using System.Collections;
using System;

public class ServerConnectTest : MonoBehaviour {
	private Farts serv;
    private WWW lvlUpdate;
	public DLOutputText dlOutputField;
	public ULOutputText ulOutputField;
    public UDOutputText udOutputField;
    public DELOutputText delOutputField;

	IEnumerator Start() {
		serv = gameObject.AddComponent<Farts>();


		// Download level example
        WWW dlLvlReq = serv.getLvlWww("6192449487634432");
        yield return dlLvlReq;

        // Use the downloaded level data
        string dlLvlData = dlLvlReq.text;

        if (serv.dataCheck(dlLvlData))
        {
            Debug.Log("Level downloaded: " + dlLvlData);
            dlOutputField.ChangeText(dlLvlData);
        } 
        else
        {
            Debug.Log("ERROR: LEVEL DATA DOWNLOAD FAILED");
            dlOutputField.ChangeText("ERROR: LEVEL DATA DOWNLOAD FAILED");
        }


        // Upload level example
        WWW ulLvlReq = serv.newLvlWww("432", "564", "livedatatest1", "draftdatatest");
        yield return ulLvlReq;

        // Use the returned data
        string ulLvlId = ulLvlReq.text;

        if (serv.dataCheck(ulLvlId))
        {
            Debug.Log("Level uploaded: " + ulLvlId);
            ulOutputField.ChangeText(ulLvlId);
        }
        else
        {
            Debug.Log("ERROR: LEVEL UPLOAD FAILED");
            ulOutputField.ChangeText("ERROR: LEVEL UPLOAD FAILED");
        }


        // Update level example
		lvlUpdate = serv.updateLvl("5135818813341696", "456", "teehee", "123123");


        // Delete level example [WARNING MAY FREEZE WEB PLAYER]
		/*string delLvlId = serv.delLvl("6015428115562496");

        // Use the returned data
        if (serv.dataCheck(delLvlId))
        {
            Debug.Log("Level deleted: " + delLvlId);
            delOutputField.ChangeText(delLvlId);
        }
        else
        {
            Debug.Log("ERROR: LEVEL DELETE FAILED");
            delOutputField.ChangeText("ERROR: LEVEL DELETE FAILED");
        }*/


		// Register example [WARNING MAY FREEZE WEB PLAYER]
		/*string registerResult = serv.register ("spacedandy", "boobies", "asdfsafsadfasdf");

		// Use the returned data
		if (serv.dataCheck (registerResult)) {
			Debug.Log ("Registration succeeded: " + registerResult);
		} else {
			Debug.Log("Registration failed");
		}*/


        // Login example [WARNING MAY FREEZE WEB PLAYER]
		/*string loginResult = serv.login ("spacedandy", "boobies");

		// Use the returned data
		if (serv.dataCheck (loginResult)) {
			Debug.Log ("Login succeeded: " + loginResult);
		} else {
			Debug.Log("Login failed");
		}*/


        // Download character example [WARNING MAY FREEZE WEB PLAYER]
        /*string charGetResult = serv.getChar(loginResult);

		// Use the returned data
		if (serv.dataCheck(loginResult))
        {
            Debug.Log("Character get succeeded: " + charGetResult);
        }
        else
        {
            Debug.Log("Character get failed");
        }*/


        // Update character example [WARNING MAY FREEZE WEB PLAYER]
        /*string charUpdateResult = serv.updateChar("5770237022568448", "huehuhehueheuheuheheu");

        // Use the returned data
        if (serv.dataCheck(charUpdateResult))
        {
            Debug.Log("Character update succeeded: " + charUpdateResult);
        }
        else
        {
            Debug.Log("Character update failed");
        }*/
    }


    void Update() {
        // Use the returned data from update level request's coroutine
        if (lvlUpdate != null)
        {
            if (lvlUpdate.isDone && lvlUpdate.error == null)
            {
				if (serv.dataCheck(lvlUpdate.text)) {
					Debug.Log("Level updated: " + lvlUpdate.text);
					udOutputField.ChangeText(lvlUpdate.text);
				} else {
					udOutputField.ChangeText("ERROR: LEVEL UPDATE FAILED");
				}
                lvlUpdate = null;
            }
            else if (lvlUpdate.error != null)
            {
                udOutputField.ChangeText("ERROR: LEVEL UPDATE FAILED");
            }
        }
    }
}
