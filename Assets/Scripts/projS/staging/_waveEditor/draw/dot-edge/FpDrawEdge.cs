using UnityEngine;
using UnityEngineEx;

public class FpDrawEdge : MonoBehaviour
{
    public FpLineDraw Wall = null;
    public FpLineDraw Bone = null;
    public TextMesh Text = null;

    const string _NAME_TEXT_MESH = "textMesh";
    public void Init(Vector3 linePt1, Vector3 linePt2, FpDraw fpDraw)
    {
        //Bone = CreateGameObject.WithLineRenderer(
        //    linePt1.WithGapY(HudFpGraphs.yDepth2), linePt2.WithGapY(HudFpGraphs.yDepth2), FpGlobalVar.CM * 3, Color.cyan, transform);
        Text = CreateGameObject.With<TextMesh>(_NAME_TEXT_MESH, transform);

        //Text
        Text.GetComponent<MeshRenderer>().DisableRendererOption();
        Text.transform.localEulerAngles = new Vector3(90,0);
        Text.fontSize = 80;
        Text.characterSize = 40;
        Text.anchor = TextAnchor.MiddleCenter;
        Text.fontStyle = FontStyle.Bold;
        Text.color = Color.black;
    }

    public void Refresh(Vector3 linePt1, Vector3 linePt2)
    {
        Wall.Active(linePt1, linePt2);
        //Bone.SetPositions(new Vector3[] { linePt1.WithGapY(HudFpGraphs.yDepth2), linePt2.WithGapY(HudFpGraphs.yDepth2) });

      //  float length = Vector3.Magnitude(linePt2 - linePt1);
        Vector3 center = Vector3.Lerp(linePt1, linePt2, 0.5f);
        Text.transform.localPosition = center.WithGapY(FpDraw.yDepth3);
       // if (length < FpGlobalVar.M1)
       //     Text.text = FpGlobalFunc.GetStrMeter_WithoutLabel(length);
      //  else
        //    Text.text = FpGlobalFunc.GetStrMeter(length);
    }
}
