/*
 * TrackMethodCaller Debugging Utility
 * 
 * Description:
 * This utility is designed to assist with tracking and logging the caller of specific methods. 
 * When integrated into a method, it will log the calling method's name and its declaring class. 
 * Additionally, it keeps a count of how many times a specific caller has invoked the method, 
 * providing insights into call patterns and frequencies.
 * 
 * Usage:
 * 1. Use the using DebugTools as namespace, put it at the toop of your code
 * 2. Place the `TrackMethodCaller.LogCaller();` statement at the beginning of any method you wish to monitor.
 * 3. Observe the Unity Console for logs about the calling method and the number of times it has been called.
 * 
 * Note:
 * This tool is intended for debugging and development purposes only. 
 * It is advised to remove or disable its usage in production code to avoid potential performance overhead.
 */

using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace DebugTools {
    public static class TrackMethodCaller
    {
        private static Dictionary<string, int> callCounts = new Dictionary<string, int>();

        public static void LogCaller()
        {
            StackTrace stackTrace = new StackTrace();

            // Prendi il frame precedente a questo metodo
            StackFrame callerFrame = stackTrace.GetFrame(1);

            // Ottieni il metodo che ha chiamato questo metodo
            var method = callerFrame.GetMethod();

            string callerInfo = $"Metodo chiamante: {method.Name} - Classe chiamante: {method.DeclaringType.Name}";

            if (callCounts.ContainsKey(callerInfo))
            {
                callCounts[callerInfo]++;
            }
            else
            {
                callCounts[callerInfo] = 1;
            }

            UnityEngine.Debug.Log($"{callerInfo} ha chiamato il metodo {method.Name} {callCounts[callerInfo]} volte.");
        }
    }

}
