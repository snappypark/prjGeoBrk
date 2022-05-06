using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nj;

public class table
{
    public string name;
    public scInt[] values = null;
    //public scInt[] prices = null;

    public int this[int i] { get { return values[i]; } }

    public table(string name_, int maxLength_)
    {
        name = name_;
        values = new scInt[maxLength_];
    }
}
