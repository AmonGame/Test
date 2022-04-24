public static class StringExtension
{
 
    public static int BoolToInt(this bool val)
    {
        if (val)
            return 1;
        else
            return 0;
    }

    public static bool IntToBool(this int val)
    {
        if (val != 0)
            return true;
        else
            return false;
    }

    public static int GetDeterministicHashCode(this string str)
    {
        unchecked
        {
            int hash1 = (5381 << 16) + 5381;
            int hash2 = hash1;

            for (int i = 0; i < str.Length; i += 2)
            {
                hash1 = ((hash1 << 5) + hash1) ^ str[i];
                if (i == str.Length - 1)
                    break;
                hash2 = ((hash2 << 5) + hash2) ^ str[i + 1];
            }

            return hash1 + (hash2 * 1566083941);
        }
    }
}
