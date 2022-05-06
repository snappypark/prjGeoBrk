using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class wave_abs
{
    const short outOfAdx = 9921;
    #region keys
    const string _jsKeyWAVE = "wave";
    const string _jsKeyINFO = "info";
    const string _jsKeyDOTS = "dots";
    const string _jsKeyEDGES = "edges";
    const string _jsKeyHALFS = "halfs";
    const string _jsKeyPORTALS = "portals";
    const string _jsKeyGRAPHS = "graphs";
    JSONObject _jsInfo = new JSONObject();
    JSONObject _jsEdges = new JSONObject();
    JSONObject _jsDots = new JSONObject();
    JSONObject _jsHalfs = new JSONObject();
    JSONObject _jsPortals = new JSONObject();
    JSONObject _jsGraphs = new JSONObject();
    #endregion
}
