using UnityEngine;
using System.Collections;

public static class TimerManager {
	private static ArrayList timers = new ArrayList (); 
	public static bool isPlaying = true; 


    public static IEnumerator TimerUpdate()
    {
        while (Application.isPlaying)
        {
            foreach (Timer t in timers)
            {
                t.Update(Time.deltaTime);
            }

            yield return null;
        }
    }

    public static void Init()
    {
        new GameObject("Timer Dummy").AddComponent<TimerDummy>().StartCoroutine(TimerUpdate());
    }

	public static void SetupTimer(Timer t){
		timers.Add (t);
	}

	public static void Pause() {

	}
}