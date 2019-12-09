using UnityEngine;
using System.Collections;

public class TimerManager : MonoBehaviour {
	private static ArrayList timers = new ArrayList (); 
	public static bool isPlaying = true; 
    private static GameObject instance;

    private void Update() {
        foreach (Timer t in timers)
            t.Update(Time.deltaTime);
    }

    private static void CreateInstance() {
        instance = new GameObject("Timer Manager");
        instance.AddComponent<TimerManager>();
        DontDestroyOnLoad(instance);
    }

	public static void SetupTimer(Timer t){
        if (instance == null) CreateInstance();
		timers.Add (t);
	}
}