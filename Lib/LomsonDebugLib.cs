/*
 LomsonLib - Library to help management of various object types in program 
  developed in c#.
  
  Copyright (C) 2014 Jareth Lomson <incognito1234@users.sourceforge.net>
  
 This program is free software; you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation; either version 2 of the License, or
  (at your option) any later version.

  This program is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with this program; if not, write to the Free Software
  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

using System.Reflection;


/*
 * LomsonLib.UI
 * 
 *     Version 1.1
 *      See ListViewLayoutManager.cs for release notes
 */

namespace LomsonLib.LomsonDebug
{
	/// <summary>
	/// Description of LomsonDebug.
	/// </summary>
	public class LomsonDebug
	{
		/// <summary>
		/// Write a timestamp message on debug output
		/// </summary>
		/// <param name="sw">StopWatch</param>
		/// <param name="message">Message to display</param>
		public static void TimeStampedWrite(Stopwatch sw, String message) {
			Debug.WriteLine(sw.ElapsedMilliseconds.ToString() + "ms - " + message);
		}
		
		/// <summary>
		/// Print all fields - public and not public on debug output
		/// </summary>
		public static void WriteAllFieldOnDebugOutput( object classInstance ) {
			Debug.WriteLine("Entering WriteAllFieldOnDebugOutput");
			Debug.Indent();
			
			Type classType = classInstance.GetType();
			FieldInfo [] test = classType.GetFields(
            	BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.NonPublic |
                BindingFlags.Public );
            
            Debug.Indent();
            foreach (FieldInfo fi in test) {
            	Debug.WriteLine("Field = " + fi.Name);
            }
            Debug.Unindent();
            
            Debug.WriteLine("Ending WriteAllFieldOnDebugOutput");            
		}
		
		
		
		public static void WriteAllMethodFromAnEvent( object classInstance, string eventName) {
			Debug.WriteLine("Entering WriteAllMethodFromAnEvent - eventName = " + eventName);
			Debug.Indent();
			
			Delegate ehl = (LomsonDebug.GetEventHandler(classInstance, eventName)) as Delegate;
			if (ehl != null) {
				
			    MethodInfo method = ehl.Method;
	            string name = ehl.Target == null ? "" : ehl.Target.ToString();
	            if (ehl.Target is Control) name = ((Control)ehl.Target).Name;
	            Debug.WriteLine(name + "; " + method.DeclaringType.Name + "." + method.Name);
	            
				Debug.Unindent();
			}
			
			
			Debug.Unindent();			
			Debug.WriteLine("Ending WriteAllMethodFromAnEvent");
		}
			
		
		/// <summary>
		/// Get handler from event name
		/// </summary>
		/// <param name="classInstance"></param>
		/// <param name="eventName"></param>
		/// <returns></returns>
		public static object GetEventHandler(object classInstance, string eventName)
        {
            Type classType = classInstance.GetType();
            
            try{
	            FieldInfo eventField =
	            	classType.GetField(eventName,
	            	           BindingFlags.GetField            	           
	                         | BindingFlags.NonPublic
	                         | BindingFlags.Instance);
	
	            object eventDelegate = eventField.GetValue(classInstance);
	
	            // eventDelegate will be null if no listeners are attached to the event
	            if (eventDelegate == null)
	            {
	                return null;
	            }
	            return eventDelegate;
            } catch (Exception e) {
            	Debug.WriteLine("GetEventHandler - Exception " + e.Message);
            	return null;
            }
           
        }
		
		// Sample: http://stackoverflow.com/questions/660480/determine-list-of-event-handlers-bound-to-event
//		 Form form = new MyForm();
//        EventHandlerList events = (EventHandlerList)typeof(Component)
//            .GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance)
//            .GetValue(form, null);
//        object key = typeof(Form)
//            .GetField("EVENT_FORMCLOSING", BindingFlags.NonPublic | BindingFlags.Static)
//            .GetValue(null);
//
//        Delegate handlers = events[key];
//        foreach (Delegate handler in handlers.GetInvocationList())
//        {
//            MethodInfo method = handler.Method;
//            string name = handler.Target == null ? "" : handler.Target.ToString();
//            if (handler.Target is Control) name = ((Control)handler.Target).Name;
//            Console.WriteLine(name + "; " + method.DeclaringType.Name + "." + method.Name);
//        }
	}
}
