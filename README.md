# Unity-advanced-timers
Out of the box functionning timers for Unity


### How to use them ?

Instantiate one and set an end time for the timer :

```sh
Timer myTimer = new Timer(5f);
```

You have access to the events OnTimerEnd and OnTimerUpdate which works like this : 
```sh
private void myFunction(){
    debug.log("Timer ended");    
}
myTimer.OnTimerEnd += myFunction;
```
But don't forget that you have to start your timer for the countdown to really start. 
```sh
myTimer.Play();
```
Finally, you can do most of that within the constructor of the timer class.
the method Play returns the timer.
```sh
Timer myTimer = new Timer (5f, () => Debug.log("Hello world !")).Play();
```

I invite you to consult the timer class in order to see all the other functionnalities.