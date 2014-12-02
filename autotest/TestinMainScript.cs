using System.Collections;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Diagnostics;
using UnityEngine;

public class TestinMainScript : MonoBehaviour {
	
	private TestinUIAutomator automator;

	// Use this for initialization
	void Start () {
		automator = TestinUIAutomator.GetInstance ();
		automator.Start ();
	}

	// Update is called once per frame
	void Update () {
		if (TestinUIAutomator.isWait) 
		{
			dumpObjectInfo ();
		}
	}

	private void dumpObjectInfo(){
		
		string result;
		
		XAttribute[] rootAtts = {
			new XAttribute("screen", UnityEngine.Screen.width + "x" + UnityEngine.Screen.height),
			new XAttribute("support", Application.platform),
			new XAttribute("EngineName", "Unity3D"),
			new XAttribute("version", TestinUIAutomator.TESTIN_AUTOTEST_PLUGIN_VERSION),
			new XAttribute("EngineVersionCode", ""),
			new XAttribute("EngineVersionName", UnityEngine.Application.unityVersion),
			new XAttribute("pid", Process.GetCurrentProcess ().Id)
		};
		XElement rootElement = new XElement("hierarchy", rootAtts);
		XAttribute[] sceneAtts = {
			new XAttribute("level", Application.loadedLevel),
			new XAttribute("name", Application.loadedLevelName)
		};
		XElement sceneElement = new XElement ("CureentScene", sceneAtts);
		
		//Get all game objects in the current scene
		GameObject[] allObjects = (GameObject[])UnityEngine.Object.FindObjectsOfType(typeof(GameObject));

		foreach (GameObject go in allObjects)
		{
			if(go.transform.parent == null)
			{
				sceneElement = object2XML(go, sceneElement);
			}
		}

		rootElement.Add (sceneElement);
	
		result = rootElement.ToString();
		
		automator.setStrObjectInfo (result);
		TestinUIAutomator.initObjectInfo.Set ();
	
	}

	private XElement object2XML(GameObject go, XElement parentNode)
	{
		string guiText = "";
		if(go.guiText != null)
		{
			guiText = go.guiText.text;
		}

		//Is the GameObject active in the scene
		int visible = 0;
		if(go.activeInHierarchy){
			visible = 1;
		}

		///////////////////////////////////////////////////////////

		bool enabled = false;
		Vector3 pos;

		Camera worldcamera = FindCameraForLayer(go.layer);
		if(worldcamera != null)
		{
			pos = worldcamera.WorldToScreenPoint(go.transform.position);
		}
		else
		{
			pos = Camera.main.WorldToScreenPoint(go.transform.position);
		}
		BoxCollider collider = go.GetComponent<BoxCollider>();

		string bounds = "";

		if(collider != null)
		{
			enabled = true;
			bounds = "[" + pos.x + ", "
				+ (UnityEngine.Screen.height - collider.size.y - pos.y) + "]["
				+ (pos.x + collider.size.x) + ","
				+ (UnityEngine.Screen.height - pos.y) + "]";
		}

		XAttribute[] nodeAtts = {
			new XAttribute("tagId", go.tag),
			new XAttribute("zIndex", pos.z),
			new XAttribute("visible", visible),
			new XAttribute("bounds", bounds),
			new XAttribute("name", go.name),
			new XAttribute("text", guiText),
			new XAttribute("enabled", enabled)
		};

		///////////////////////////////////////////////////////////
		XElement nodeElement = new XElement("node", nodeAtts);
		parentNode.Add (nodeElement);
		if(go.transform.childCount != 0)
		{
			for(int i = 0; i < go.transform.childCount; i++)
			{
				object2XML(go.transform.GetChild (i).gameObject, nodeElement);
			}
		}

		return parentNode;
	} 

	static public Camera FindCameraForLayer (int layer)
	{
		int layerMask = 1 << layer;
		
		Camera cam = Camera.main;
		if (cam != null && (cam.cullingMask & layerMask) != 0) return cam;
		
		Camera[] cameras = FindActive<Camera>();
		
		for (int i = 0, imax = cameras.Length; i < imax; ++i)
		{
			cam = cameras[i];
			if ((cam.cullingMask & layerMask) != 0)
				return cam;
		}
		return null;
	}

	static public T[] FindActive<T> () where T : Component
	{
		#if UNITY_3_5 || UNITY_4_0
		return GameObject.FindSceneObjectsOfType(typeof(T)) as T[];
		#else
		return GameObject.FindObjectsOfType(typeof(T)) as T[];
		#endif
	}

}
