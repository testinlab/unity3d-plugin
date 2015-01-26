// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Diagnostics;

public static class TestinCrashHelper
{	
	const int TESTIN_CRASH_TYPE = 5;

	static bool _ShowDebug = true;
	private static bool isInitialized = false;


	#if UNITY_EDITOR 
	private static void InitTestinAgent (string appkey, string channel)
	{

	}

	public static void SetUserInfo (string userInfo)
	{

	}

	private static void _OnDebugLogCallbackHandler (string name, string stack, LogType type)
	{

	}

	private static void reportException(int type, string name, string message)
	{

	}

	#elif UNITY_IPHONE 
	[DllImport("__Internal")]
	private static extern void testinInit (string appId, string channel);
	
	[DllImport("__Internal")]
	private static extern void testinSetUserInfo (string userInfo);
	
	[DllImport("__Internal")]
	private static extern void testinReportCustomizedException (int type, string reason, string stackTrace);

	private static void InitTestinAgent (string appkey, string channel)
	{
		System.Console.Write ("InitForIos");
		try {
			testinInit (appkey, channel);
			isInitialized = true;
		} catch {
			System.Console.Write ("TestinAgent failed to initialize.");
		}
	}

	public static void SetUserInfo (string userInfo)
	{
		if (!isInitialized) {
			return;
		}
		testinSetUserInfo (userInfo);
	}

	private static void _OnDebugLogCallbackHandler (string name, string stack, LogType type)
	{
		if (LogType.Assert != type && LogType.Exception != type) {
			return;
		}
		
		if (!isInitialized) {
			return;
		}
		
		try {
			testinReportCustomizedException (TESTIN_CRASH_TYPE, name, stack);
		} catch (System.Exception e) {
			System.Console.Write ("Unable to log a crash exception to TestinAgent to an unexpected error: " + e.ToString ());
		}
	}

	private static void reportException(int type, string name, string message)
	{
		testinReportCustomizedException (type, name, message);
	}

	#elif UNITY_ANDROID 
	private static readonly string TESTINAGENT_CLASS_ANDROID = "com.testin.agent.TestinAgent";
	private static AndroidJavaClass mTestinPlugin_ANDROID = null;

	private static void InitTestinAgent (string appkey, string channel)
	{	
		System.Console.Write ("InitForAndroid");
		
		AndroidJavaClass cls_UnityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject objActivity = cls_UnityPlayer.GetStatic<AndroidJavaObject> ("currentActivity");
		
		mTestinPlugin_ANDROID = new AndroidJavaClass (TESTINAGENT_CLASS_ANDROID);
		if (mTestinPlugin_ANDROID == null) {
			System.Console.Write ("TestinAgent failed to initialize.  Unable to find class " + TESTINAGENT_CLASS_ANDROID);
			return;
		}
		
		if (channel == null) 
		{
			mTestinPlugin_ANDROID.CallStatic ("init", objActivity, appkey);
		} else 
		{
			mTestinPlugin_ANDROID.CallStatic ("init", objActivity, appkey, channel);
		}
		
		isInitialized = true;
		
	}

	public static void SetUserInfo (string userInfo)
	{
		if (!isInitialized) {
			return;
		}

		mTestinPlugin_ANDROID.CallStatic ("setUserInfo", userInfo);
	}

	private static void _OnDebugLogCallbackHandler (string name, string stack, LogType type)
	{
		if (LogType.Assert != type && LogType.Exception != type) {
			return;
		}
		
		if (!isInitialized) {
			return;
		}
		
		try {
			mTestinPlugin_ANDROID.CallStatic ("reportCustomizedException", TESTIN_CRASH_TYPE, name ,stack);;
		} catch (System.Exception e) {
			System.Console.Write ("Unable to log a crash exception to TestinAgent to an unexpected error: " + e.ToString ());
		}
	}

	private static void reportException(int type, string name, string message)
	{
		mTestinPlugin_ANDROID.CallStatic ("reportCustomizedException", type, name, message);
	}
	
	#endif

	/// <summary>
	/// Description:
	/// Start TestinAgent for Unity, will start TestinAgent for android if it is not already active.
	/// Parameters:
	/// appkey: Testin Provided AppKey for this application
	/// </summary>
	public static void Init (string appkey)
	{
		Init (appkey, null);
	}

	public static void Init (string appkey, string channel)
	{
		if (isInitialized) {
			System.Console.Write ("TestinAgent is already initialized.");
			return;
		}
		
		if (_ShowDebug) 
		{
			System.Console.Write ("Initializing TestinAgent with AppKey " + appkey);
		}

		InitTestinAgent (appkey, channel);

		if (isInitialized) {
			System.AppDomain.CurrentDomain.UnhandledException += _OnUnresolvedExceptionHandler;
			Application.RegisterLogCallback (_OnDebugLogCallbackHandler);
		}

	}

	public static void LogHandledException (System.Exception e)
	{
		doLogError (e);
	}
	
	private static void doLogError (System.Exception e)
	{
		if (!isInitialized) {
			return;
		}
		
		StackTrace stackTrace = new StackTrace (e, true);
		string[] classes = new string[stackTrace.FrameCount];
		string[] methods = new string[stackTrace.FrameCount];
		string[] files = new string[stackTrace.FrameCount];
		int[] lineNumbers = new int[stackTrace.FrameCount];

		String message = "";
		
		for (int i = 0; i < stackTrace.FrameCount; i++) {
			StackFrame frame = stackTrace.GetFrame (i);
			classes [i] = frame.GetMethod ().DeclaringType.Name;
			methods [i] = frame.GetMethod ().Name;
			files [i] = frame.GetFileName ();
			lineNumbers [i] = frame.GetFileLineNumber ();

			message += classes[i] + ".";
			message += methods[i] + "() (at ";
			message += files [i] + ":";
			message += lineNumbers[i] + ")";
		}

		reportException (TESTIN_CRASH_TYPE, e.GetType().Name, message);

	}

	private static void _OnUnresolvedExceptionHandler (object sender, System.UnhandledExceptionEventArgs args)
	{
		if (!isInitialized || args == null || args.ExceptionObject == null) {
			return;
		}
		
		if (args.ExceptionObject.GetType () != typeof(System.Exception)) {
			return;
		}
		
		doLogError ((System.Exception)args.ExceptionObject);
	}

}


