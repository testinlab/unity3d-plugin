using UnityEngine;
using System.Collections;

public class TestinInit : MonoBehaviour {

	/// <summary>
	/// Your Testin Appkey.  Every app has a special identifier that allows Testin Crash
	/// to associate error monitoring data with your app.  Your Appkey can be found on the
	/// "App Settings" page of the app you are trying to monitor.
	/// </summary>
	/// <example>A real Appkey looks like this:  a0d49052a0853862a7c18a77d2a39838</example>
	private const string TestinAppKey = "a0d49052a0853862a7c18a77d2a39838";
	private const string TestinChannel = "Test";
	
	void Awake ()
	{
		TestinCrashHelper.Init (TestinAppKey, TestinChannel);
		TestinCrashHelper.SetUserInfo ("james");
		Destroy (this);
	}
}
