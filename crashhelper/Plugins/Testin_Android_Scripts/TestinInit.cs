using UnityEngine;
using System.Collections;

public class TestinInit : MonoBehaviour {

	/// <summary>
	/// Your Testin Appkey.  Every app has a special identifier that allows Testin Crash
	/// to associate error monitoring data with your app.  Your Appkey can be found on the
	/// "App Settings" page of the app you are trying to monitor.
	/// See the Testin Crash portal http://crash.testin.cn
	/// </summary>
	/// <example>A real Appkey looks like this:  ec0cd05fb39700ba42a43eca799e7528</example>
	private const string TestinAppKey = "ec0cd05fb39700ba42a43eca799e7528";
	
	void Awake ()
	{
		TestinAndroid.Init (TestinAppKey);
		Destroy (this);
	}
}
