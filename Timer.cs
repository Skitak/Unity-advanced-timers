﻿using UnityEngine;
using System.Collections;
using System;
public class Timer {
	private float time = 0f;
	public float endTime;
	public delegate void TimerFunction () ;
	public event TimerFunction OnTimerEnd;
	public event TimerFunction OnTimerUpdate;
	private TimerState state = TimerState.PAUSED;
	private bool isReversed = false;


	/// <summary>
	/// Crée un nouveau Timer
	/// </summary>
	/// <param name="endTime">Temps de fin du chrono</param>
	public Timer (float endTime){
		TimerManager.SetupTimer (this);
		this.endTime = endTime;
	}

	/// <summary>
	/// Crée un nouveau Timer
	/// </summary>
	/// <param name="endTime">Temps de fin du chrono</param>
	/// <param name="function">Fonction déclanché à la fin du timer.</param>
	public Timer (float endTime, TimerFunction function) : this (endTime){
		OnTimerEnd += function;
	}

	/// <summary>
	/// Crée un nouveau Timer
	/// </summary>
	/// <param name="endTime">Temps de fin du chrono</param>
	/// <param name="isAlreadyFinished">Le timer doit-il être fini tout de suite?</param>
	public Timer(float endTime, bool isAlreadyFinished) : this (endTime){
		if (isAlreadyFinished) {
			time = endTime;
			state = TimerState.FINISHED;
		}
	}

	/// <summary>
	/// Crée un nouveau Timer
	/// </summary>
	/// <param name="endTime">Temps de fin du chrono</param>
	/// <param name="function">Fonction déclanché à la fin du timer.</param>
	/// <param name="isAlreadyFinished">Le timer doit-il être fini tout de suite?</param>
	public Timer ( float endTime, TimerFunction function, bool isAlreadyFinished) : this (endTime, isAlreadyFinished){
		OnTimerEnd += function;
	}

	
	/// <summary>
	/// Remet le timer à 0.
	/// /!\ Reset() ne relance pas le timer!
	/// Pour cela, il y a ResetPlay()
	/// </summary>
	public void Reset () {
		state = TimerState.PAUSED;
		time = 0f;
	}
	/// <summary>
	/// Pause le timer.
	/// </summary>
	public void Pause (){
		state = TimerState.PAUSED;
	}
	/// <summary>
	/// Détermine si ce timer est fini.
	/// </summary>
	public bool IsFinished () {
		return state == TimerState.FINISHED;
	}
	/// <summary>
	/// Détermine si ce timer est lancé ou non.
	/// </summary>
	/// <returns><c>true</c> if this instance is started; otherwise, <c>false</c>.</returns>
	public bool IsStarted() {
		return state == TimerState.UPDATING;
	}
	/// <summary>
	/// Lance le timer.
	/// /!\ Cette méthode ne remet pas le timer à 0!
	/// Pour cela, il y a la méthode Reset().
	/// </summary>
	public void Play () {
		state = TimerState.UPDATING;
	}
	/// <summary>
	/// Reset et lance le timer
	/// </summary>
	public void ResetPlay(){
		Reset ();
		Play ();
	}

	public void Update(float delta){
		if (state == TimerState.UPDATING ){
			if (OnTimerUpdate != null)
				OnTimerUpdate();
			Time = isReversed ? Time - delta : Time + delta;
		}
	}

	public float EndTime{
		get { return endTime;}
		set {
			// Reset ();
			endTime = value;
		}
	}

	public bool IsReversed {
		get{ return isReversed; }
		set{ isReversed = value; }
	}

	public float Time {
		get {
			return time;
		}
		set {
			if (state == TimerState.UPDATING) {
				if (value >= endTime) {
					time = endTime;
					state = TimerState.FINISHED;
					if (OnTimerEnd != null)
						OnTimerEnd ();
				}
				else if (value <= 0){
					time = 0;
					state = TimerState.PAUSED;
				}
				else
					time = value;
			}
		}
	}

	public float GetPercentage(){
		return Time / EndTime;
	}

	public float GetPercentageLeft(){
		return 1 - (Time / EndTime);
	}

	public float GetTimeLeft(){
		return EndTime - Time;
	}

  // C# time format : https://www.c-sharpcorner.com/blogs/date-and-time-format-in-c-sharp-programming1
  public string GetFormattedTime(string format) {
		return FormatTime(Time, format);
	}

	public string GetFormattedTimeLeft (string format) {
		return FormatTime(GetTimeLeft(), format);
	}

	private string FormatTime(float timeToFormat, string format) {
    TimeSpan time = TimeSpan.FromSeconds(timeToFormat);
    return time.ToString(format);
	}

	// Starts a timer with "time" as endTime that's fired immediatly
	// Will play the onTimerEnd function as expected
	public static void OneShotTimer(float time, TimerFunction onTimerEnd){
		Timer oneShotTimer = new Timer(time, onTimerEnd);
		oneShotTimer.Play();
	}

	public static void RecurrentTimer(float time, TimerFunction onTimerEnd) {
    Timer oneShotTimer = new Timer(time, onTimerEnd);
		onTimerEnd += () => { oneShotTimer.ResetPlay(); };
		oneShotTimer.Play();
		
	}
}

public enum TimerState {
	PAUSED,
	FINISHED,
	UPDATING
};
