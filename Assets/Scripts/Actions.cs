using UnityEngine;
using System;

public static class Actions
{
    public static Action onIntroFinished;

    public static Action onMouseActivate;
    public static Action onMouseDeactivate;

    public static Action <GameObject> onPenOver;
    public static Action onTimeUp;
    public static Action timerReset;
    public static Action onCutsceneFinished;
}
