﻿using UnityEngine;
using System.Collections;

public class TimerManager : MonoBehaviour {
	private static ArrayList timers = new ArrayList (); 
	public static bool isPlaying = true; 


    private void Update() {
        foreach (Timer t in timers)
            t.Update(Time.deltaTime);
    }

    public static void Init() {
        GameObject timerManager = new GameObject("Timer Manager");
        timerManager.AddComponent<TimerManager>();
        DontDestroyOnLoad(timerManager);
    }

	public static void SetupTimer(Timer t){
		timers.Add (t);
	}

	public static void Pause() {

	}
}