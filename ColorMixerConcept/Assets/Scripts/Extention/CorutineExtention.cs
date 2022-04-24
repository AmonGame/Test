using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CorutineExtention
{
    public static Coroutine WaitSecond(this MonoBehaviour obj, float seconds, Action action)
    {
        if (obj.gameObject.activeSelf)
            return obj.StartCoroutine(Timer(seconds, action));
        else
            return null;
    }

    public static Coroutine UpdateCoroutine(this MonoBehaviour obj, Action action)
    {
        if (obj.gameObject.activeSelf)
            return obj.StartCoroutine(UpdateCoroutines(action));
        else
            return null;
    }

    public static void StopAll(this MonoBehaviour obj)
	{
        obj.StopAllCoroutines();
	}

    public static Coroutine WaitFrame(this MonoBehaviour obj, Action action)
    {
        if (obj.gameObject.activeSelf)
            return obj.StartCoroutine(Timer(action));
        else
            return null;
    }

    static IEnumerator Timer(Action action)
    {
        yield return new WaitForEndOfFrame();
        action?.Invoke();
    }
    static IEnumerator Timer(float timer, Action action)
    {
        yield return new WaitForSeconds(timer);
        action?.Invoke();
    }

    static IEnumerator UpdateCoroutines(Action action)
    {
        while (true)
        {
            action?.Invoke();
            yield return null;
        }
    }
}
