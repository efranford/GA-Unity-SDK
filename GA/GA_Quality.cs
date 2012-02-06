/// <summary>
/// This class handles quality (QA) events, such as crashes, fps, etc.
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GA_Quality : MonoBehaviour
{
	#region public methods
	
	public static void NewEvent(string eventName, string message, float x, float y, float z)
	{
		CreateNewEvent(eventName, message, x, y, z);
	}
	
	public static void NewEvent(string eventName, string message)
	{
		CreateNewEvent(eventName, message, null, null, null);
	}
	
	public static void NewEvent(string eventName)
	{
		CreateNewEvent(eventName, null, null, null, null);
	}
	
	#endregion
	
	#region private methods
	
	/// <summary>
	/// Used for player QA events
	/// </summary>
	/// <param name="businessID">
	/// The event identifier. F.x. "FailedToLoadLevel" <see cref="System.String"/>
	/// </param>
	/// <param name="message">
	/// A string detailing the event, F.x. the stack trace from an exception <see cref="System.String"/>
	/// </param>
	private static void CreateNewEvent(string eventName, string message, float? x, float? y, float? z)
	{
		Dictionary<string, object> parameters = new Dictionary<string, object>()
		{
			//{ GA_ServerFieldTypes.Fields[GA_ServerFieldTypes.FieldType.UserID], GA_GenericInfo.UserID },
			//{ GA_ServerFieldTypes.Fields[GA_ServerFieldTypes.FieldType.SessionID], GA_GenericInfo.SessionID },
			//{ GA_ServerFieldTypes.Fields[GA_ServerFieldTypes.FieldType.Build], GA.BUILD },
			{ GA_ServerFieldTypes.Fields[GA_ServerFieldTypes.FieldType.EventID], eventName },
			{ GA_ServerFieldTypes.Fields[GA_ServerFieldTypes.FieldType.Level], Application.loadedLevelName }
		};
		
		if (message != null && message.Length > 0)
		{
			parameters.Add(GA_ServerFieldTypes.Fields[GA_ServerFieldTypes.FieldType.Message], message);
		}
		
		if (x.HasValue)
		{
			parameters.Add(GA_ServerFieldTypes.Fields[GA_ServerFieldTypes.FieldType.X], x.ToString());
		}
		
		if (y.HasValue)
		{
			parameters.Add(GA_ServerFieldTypes.Fields[GA_ServerFieldTypes.FieldType.Y], y.ToString());
		}
		
		if (z.HasValue)
		{
			parameters.Add(GA_ServerFieldTypes.Fields[GA_ServerFieldTypes.FieldType.Z], z.ToString());
		}
		
		GA_Queue.AddItem(parameters, GA_Submit.CategoryType.GA_Log);
	}
	
	#endregion
}