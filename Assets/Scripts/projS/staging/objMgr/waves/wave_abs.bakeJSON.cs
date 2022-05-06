using System.Collections.Generic;
using UnityEngineExJSON;

public partial class wave_abs /*bakeJSON*/
{
    public static string BakeJSON(int nextTime)
    {
        edge[] arrEdges = edges.Inst.FindObjs_InPartials();
        dot[] arrDots = dots.Inst.FindObjs_InPartials(delegate (dot obj) { obj.rdot.adxes.Add(obj.adx); });
        
        for (int i = 0; i < staging.Inst.edit.rdots.Count; ++i)
            staging.Inst.edit.rdots[i].SetRespwanTime();
        
        JSONObject jsRoot = new JSONObject(); // for return jsRoot.Print()
        JSONObject jsWave = NewJSONObj.With(_jsKeyWAVE, jsRoot, JSONObject.Type.OBJECT);
        
        //info
        JSONObject jsInfo = NewJSONObj.With(_jsKeyINFO, jsWave, JSONObject.Type.OBJECT);
        jsInfo.AddField("nextime", nextTime);

        // edges
        JSONObject jsEdges = NewJSONObj.With(_jsKeyEDGES, jsWave, JSONObject.Type.ARRAY);
        for (int i = 0; i < edges.Inst.NumObj; ++i)
        {
            JSONObject jsEdge = NewJSONObj.With(jsEdges, JSONObject.Type.ARRAY);

            edge edg = arrEdges[i];
            jsEdge.Add((int)edg.type);
            jsEdge.Add((int)edg.stats.Lv);

            jsEdge.Add(edg.Pos1.x);
            jsEdge.Add(edg.Pos1.y);

            jsEdge.Add(edg.Pos2.x);
            jsEdge.Add(edg.Pos2.y);
            jsEdge.Add(edg.waiting.duration);
        }

        JSONObject jsDots = NewJSONObj.With(_jsKeyDOTS, jsWave, JSONObject.Type.ARRAY);
        for (int i = 0; i < dots.Inst.NumObj; ++i)
        {
            JSONObject jsDot = NewJSONObj.With(jsDots, JSONObject.Type.ARRAY);

            dot d = arrDots[i];
            jsDot.Add((int)d.type);
            jsDot.Add((int)d.Lv);

            jsDot.Add(d.transform.localPosition.x);
            jsDot.Add(d.transform.localPosition.y);
            jsDot.Add(d.waiting.duration);
        }

        JSONObject jsHalfs = NewJSONObj.With(_jsKeyHALFS, jsWave, JSONObject.Type.ARRAY);
        halfEdge[] halfs = dots.Inst.FindAllHalfedge_ByArrDots(arrDots);
        if (halfs != null)
        {
            int numHalf = halfs.Length;
            for (int i = 0; i < numHalf; ++i)
            {
                JSONObject jshalf = NewJSONObj.With(jsHalfs, JSONObject.Type.ARRAY);

                halfEdge haf = halfs[i];
                jshalf.Add(haf.origin.adx);
                jshalf.Add(haf.norDir.x);
                jshalf.Add(haf.norDir.y);
                jshalf.Add((int)haf.type);
                jshalf.Add(null != haf.pair ? haf.pair.adx : outOfAdx);

                Stack<short> tmp = new Stack<short>();
                while (haf.cdxs.Count > 0)
                    tmp.Push(haf.cdxs.Pop());
                while (tmp.Count > 0)
                {
                    edge edg = edges.Inst[tmp.Pop()];
                    jshalf.Add(edg.adx);
                    haf.cdxs.Push(edg.cdx);
                }
            }
        }

        JSONObject jsPortals = NewJSONObj.With(_jsKeyPORTALS, jsWave, JSONObject.Type.ARRAY);
        portal[] arrPortals = portals.Inst.FindObjs_InPartials();
        for (int i = 0; i < portals.Inst.NumObj; ++i)
        {
            JSONObject jsPortal = NewJSONObj.With(jsPortals, JSONObject.Type.ARRAY);

            portal p = arrPortals[i];
            jsPortal.Add(p.type);
            jsPortal.Add(p.transform.localPosition.x);
            jsPortal.Add(p.transform.localPosition.y);
            jsPortal.Add(null != p.Target ? p.Target.adx : outOfAdx);
        }
        
        JSONObject jsGraphs = NewJSONObj.With(_jsKeyGRAPHS, jsWave, JSONObject.Type.ARRAY);
        for (int i = 0; i < staging.Inst.edit.rdots.Count; ++i)
        {
            JSONObject jsgraph = NewJSONObj.With(jsGraphs, JSONObject.Type.ARRAY);
            rDot rd = staging.Inst.edit.rdots[i];
            jsgraph.Add((int)rd.state);
            jsgraph.Add(rd.spdCenter);
            jsgraph.Add(rd.spdCross);
            jsgraph.Add(rd.waitTime);

            int numAdxes = rd.adxes.Count;
            for (int a = 0; a < numAdxes; ++a)
                jsgraph.Add(rd.adxes[a]);
        }
        
        //Debug.Log(jsRoot.Print());
        return jsRoot.Print();
    }
    
}
