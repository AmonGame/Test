using UnityEngine;

public static class Deb
{ 
    public static void Debag(this string message, ColorText color = ColorText.blue)
    {
		UnityEngine.Debug.Log("<color=" + color.ToString() + ">Message: </color>" + message);
    }

    public static void Logs(this string message, ColorText color = ColorText.blue)
    {
		UnityEngine.Debug.Log("<color=" + color.ToString() + ">Message: " + message + " </color>");
    }

    public static void Log(this string message, ColorText color = ColorText.blue, ColorText text = ColorText.red)
    {
		UnityEngine.Debug.Log("<color=" + color.ToString() + ">Message: </color>" + "<color=" + text.ToString() + "> " + message + " </color>");
    }

    public enum ColorText
    {
        green,
        blue,
        yellow,
        red,
        white,
        magenta,
    }
}
