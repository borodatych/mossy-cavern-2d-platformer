using UnityEngine;

namespace Core
{
    public static class Helpers
    {
        public static bool IsNull<T>(this T myObject, string message = "") where T : class
        {
            switch (myObject)
            {
                case UnityEngine.Object obj when !obj:
                    Debug.LogError("The object is null! " + message);
                    return true;
                case null:
                    Debug.LogError("The object is null! " + message);
                    return true;
                default:
                    return false;
            }
        }
    }
}