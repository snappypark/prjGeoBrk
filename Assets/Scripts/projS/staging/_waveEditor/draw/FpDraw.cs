using UnityEngine;
using UnityEngineEx;
using nj;

public class FpDraw : MonoSingleton<FpDraw>
{
    [SerializeField] CreateFpLineDraw _lineCreate;
    [SerializeField] Material MatDefaultLine;
    [SerializeField] Material MatDashedLine;
    [SerializeField] Transform TranGridRoot;

    [SerializeField] Transform TransDotsRoot;
    [SerializeField] Transform TransEdgesRoot;
    [SerializeField] Transform TransWindowsRoot;
    [SerializeField] Transform TransDoorsRoot;

    [SerializeField] Transform TransMarkConstRoot;
    [SerializeField] Transform TransMarkDotsRoot;
    [SerializeField] Transform TransMarkEdgesRoot;
    [SerializeField] Transform TransDashEdgesRoot;

    const string _NAME_DOT = "drawDot";
    const string _NAME_EDGE = "drawEdge";
    const string _NAME_WINDOW = "drawWindow";
    const string _NAME_DOOR = "drawDoor";

    private void OnEnable()
    {
        SetActive(true);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
        if (TranGridRoot.childCount == 0)
        { 
            CreateGrid(new Color(0.7f, 0.6f, 0.5f, 0.3f), 24);
            CreateGrid(new Color(0.7f, 0.5f, 0.5f, 0.3f));
        }
    }

    public void Clear()
    {
        GameObjectEx.DestroyChildren(TransDotsRoot);
        GameObjectEx.DestroyChildren(TransEdgesRoot);
        GameObjectEx.DestroyChildren(TransWindowsRoot);
        GameObjectEx.DestroyChildren(TransDoorsRoot);
        GameObjectEx.DestroyChildren(TransMarkDotsRoot);
        GameObjectEx.DestroyChildren(TransMarkEdgesRoot);
        GameObjectEx.DestroyChildren(TransDashEdgesRoot);
    }

    #region Grid
    float _gridGap = 1; // 1 == 1mm, 1000 = 1m
    float _gridWidth = 0.1f; // 1 == 1mm, 1000 = 1m

    public void CreateGrid(Color color, int numLine = 16)
    {
        const float zGap = 10.1f;
      //  int halfNumLine = (numLine >> 1);
        float width = _gridWidth;
        float gapSum = 0;

        float sizeGrid = numLine * _gridGap;
        for (int i = 0; i <= numLine; ++i)
        {
            gapSum = _gridGap * i;
            
            float beginOriginX = 0; //  -halfNumLine * _gridGap
            float beginOriginY = 0; //  -halfNumLine * _gridGap

            Vector3 beginPos = new Vector3(beginOriginX + gapSum, beginOriginY, zGap);
            Vector3 endPos = new Vector3(beginOriginX + gapSum, beginOriginY + sizeGrid, zGap);
            _lineCreate.With( width, 0.0f, color, TranGridRoot).Active(beginPos, endPos);

            beginPos = new Vector3(beginOriginX, beginOriginY + gapSum, zGap);
            endPos = new Vector3(beginOriginX + sizeGrid, beginOriginY + gapSum, zGap);

            _lineCreate.With( width, 0.0f, color, TranGridRoot).Active(beginPos, endPos);
        }

        gapSum = _gridGap * 25;
        //    m_LineMgr.CreateLineRenderer(new Vector3(-halfLine, 0, -halfLine + gapSum), new Vector3(halfLine, 0, -halfLine + gapSum), width, color, transform);
        //    m_LineMgr.CreateLineRenderer(new Vector3(-halfLine + gapSum, 0, -halfLine), new Vector3(-halfLine + gapSum, 0, halfLine), width, color, transform);
    }
    #endregion

    #region Graph Dot, Edge
    public const int yDepth1 = 1;
    public const int yDepth2 = 2;
    public const int yDepth3 = 3;
    public const int yDepth4 = 4;
    public const int yDepth5 = 5;
    float DEFAULT_EDGE_WIDTH = 1.0f;

    public FpDrawDot CreateDot(Vector3 dotPt)
    {
        FpDrawDot drawDot = CreateGameObject.With<FpDrawDot>(_NAME_DOT, TransDotsRoot);
        drawDot.Dot = _lineCreate.With(dotPt, dotPt, DEFAULT_EDGE_WIDTH, yDepth1, Color.gray, drawDot.transform);
        drawDot.Bone = _lineCreate.With(dotPt, dotPt, DEFAULT_EDGE_WIDTH, yDepth2, Color.cyan, drawDot.transform);
        drawDot.Refresh(dotPt);
        return drawDot;
    }
    
    public FpDrawEdge CreateEdge(Vector3 linePt1, Vector3 linePt2)
    {
        FpDrawEdge drawEdge = CreateGameObject.With<FpDrawEdge>(_NAME_EDGE, TransEdgesRoot);
        drawEdge.Wall = _lineCreate.With(linePt1, linePt2, DEFAULT_EDGE_WIDTH, yDepth1, Color.gray, drawEdge.transform);
        drawEdge.Init(linePt1, linePt2, this);
        drawEdge.Refresh(linePt1, linePt2);
        return drawEdge;
    }
    
    #endregion


}
