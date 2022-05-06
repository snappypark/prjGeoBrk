using UnityEngine;

namespace nj
{ 
[System.Serializable]
public struct scInt
{
    [System.NonSerialized]
    static readonly int MAX_TSSDATA = 2;

    [System.NonSerialized]
    TssSdtInt[] m_tssData;

    bool IsSafe()
    {
        for (int i = 1; i < MAX_TSSDATA; i++)
        {
            int data1 = (int)m_tssData[i];
            int data2 = (int)m_tssData[i - 1];
            int dif = data1 - data2;
            if (dif != 0)
            {
                sc_static.ProblemDetected("[SecureInt] dif=" + dif + ", data1=" + data1 + ", data2=" + data2);
                return false;
            }
        }

        return true;
    }

    public int GetValue()
    {
        if (m_tssData != null)
        {
            if (IsSafe())
                return (int)m_tssData[0];
        }
        return 0;
    }
    public void SetValue(int v)
    {
        if (m_tssData == null)
            m_tssData = new TssSdtInt[MAX_TSSDATA];

        for (int i = 0; i < MAX_TSSDATA; i++)
            m_tssData[i] = v;

    }

    public int CompareTo(scInt value)
    {
        int v0 = GetValue();
        int v1 = (int)value;
        return (v0 - v1);
    }

    public static implicit operator int(scInt v)
    {
        return v.GetValue();
    }

    public static implicit operator scInt(int v)
    {
        scInt obj = new scInt();
        obj.SetValue(v);
        return obj;
    }

    public static scInt operator ++(scInt v)
    {
        int value = v.GetValue();
        value++;
        v.SetValue(value);
        return v;
    }

    public static scInt operator --(scInt v)
    {
        int value = v.GetValue();
        value--;
        v.SetValue(value);
        return v;
    }

    public override string ToString()
    {
        return string.Format("{0}", GetValue());
    }
}
}