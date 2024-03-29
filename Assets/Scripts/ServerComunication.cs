using UnityEngine;
using System.Collections;
using  System.Collections.Generic;
using UnityEngine.UI;

using System.Text;
public class ServerComunication : MonoBehaviour {
	public static string GAME_VERSION= "1.0.4";
	public static string adStoreLink = "";
	public static string serverURL= "";

	public static string serverURL2= "";

	public delegate void OnMessageRecieved();
	public static event  OnMessageRecieved onMessage;

	public delegate void OnSentSuccessful();
	public static event  OnSentSuccessful onSentSuccessful;

	public delegate void OnUpdateName();
	public static event  OnUpdateName onUpdateName;

	public delegate void OnNewVersion();
	public static event  OnNewVersion onNewVersion;

	public delegate void OnAdRecieved(Texture2D texture);
	public static event  OnAdRecieved onAdRecieved;




	public static string DecodeFromUtf8(string utf8String)
	{
		// copy the string as UTF-8 bytes.
		byte[] utf8Bytes = new byte[utf8String.Length];
		for (int i=0;i<utf8String.Length;++i) {
			Debug.Assert( 0 <= utf8String[i] && utf8String[i] <= 255, "the char must be in byte's range");
			utf8Bytes[i] = (byte)utf8String[i];
		}
		print ("DecodeFromUtf8  " + Encoding.UTF8.GetString (utf8Bytes, 0, utf8Bytes.Length));
		return Encoding.UTF8.GetString(utf8Bytes,0,utf8Bytes.Length);
	}
	public static string UTF8toASCII(string text)
	{
		System.Text.Encoding utf8 = System.Text.Encoding.UTF8;
		byte[] encodedBytes = utf8.GetBytes(text);
		byte[] convertedBytes =
			Encoding.Convert(Encoding.UTF8, Encoding.ASCII, encodedBytes);
		System.Text.Encoding ascii = System.Text.Encoding.ASCII;
		
		return ascii.GetString(convertedBytes);
	}


}
