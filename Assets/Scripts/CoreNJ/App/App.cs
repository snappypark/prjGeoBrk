using System.Collections;
using UnityEngine;

namespace nj
{ 
public class App : MonoSingleton<App>
{
    [SerializeField] public InApp InApp;
    public FlowMgr FlowMgr = new FlowMgr();
    float _dt = 0.0f;

    void Awake()
    {
        scr.Awake();
        Test.Active = false;
        AppScr.Awake();
        AppLayer.Awake();
        coord.Awake();

#if UNITY_EDITOR
        Test.Active = true;
            //   Application.targetFrameRate = 64;
            Application.targetFrameRate = 54;
#elif UNITY_ANDROID
        if(!IsVendingApk())
            Application.Quit();
#else
        Application.targetFrameRate = 54;
        _guiType = eGUIType.NotUse;
#endif
        }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIs.Inst.BackBtn.Show();
        }
    }

    public static bool IsVendingApk()
    {
        return Application.installerName.Equals("com.android.vending");
    }
    
    #region Flow
    public enum eStartFlow
    {
        Entry,
        Menu,
        PlayStage,
        EditWave,
    }
    [SerializeField] public eStartFlow StartFlow = eStartFlow.Entry;

    IEnumerator Start()
    {
        yield return ProjS.Flow_Start.OnStart();
        switch (StartFlow)
        {
            case eStartFlow.Menu:
                FlowMgr.Change<ProjS.Flow_Menu>();
                break;
            case eStartFlow.PlayStage:
                FlowMgr.Change<ProjS.Flow_Play>();
                break;
            case eStartFlow.EditWave:
                FlowMgr.Change<ProjS.Flow_EditWave>();
                break;
            default:
                FlowMgr.Change<ProjS.Flow_Entry>();
                break;
        }
        StartCoroutine(FlowMgr.Update_());
    }
    
    void OnDisable()
    {
        StopCoroutine(FlowMgr.Update_());
    }

    #endregion

    #region GUI
    enum eGUIType
    {
        NotUse = -1,
        JSonTool = 0,
        TestMode2,
        TestMode3,
    }

    [SerializeField] eGUIType _guiType = eGUIType.NotUse;
    string[] _toolbarStrs = new string[] { "JSonTool", "TestMode2", "TestMode3" };
    string[] _fpEditStrs = new string[] { "Move", "Draw", "Delete" };
    int _fpEditInt = 0;

    void OnGUI()
    {
        if (_guiType == eGUIType.NotUse)
            return;

        _dt += (Time.deltaTime - _dt) * 0.1f;
        GUILayout.BeginArea(new Rect(4, 4, Screen.width - 8, Screen.height));

        GUIStyle guiStyle = new GUIStyle(GUI.skin.button) { fontSize = Screen.height >> 5 };
        GUILayoutOption[] guiOptions0 = new[] { GUILayout.Width(Screen.width), GUILayout.Height(Screen.height >> 5) };
        GUILayoutOption[] guiOptions = new[] { GUILayout.Width(Screen.width * 0.3f), GUILayout.Height(Screen.height >> 4) };

        GUILayout.Label(string.Format("fps:{0:0.00} ", 1.0f / _dt), guiStyle, guiOptions0);
        _guiType = (eGUIType)GUILayout.Toolbar((int)_guiType, _toolbarStrs, GUILayout.Width(Screen.width));

        switch (_guiType)
        {
            case eGUIType.JSonTool:
                {
                    if (GUILayout.Button("New ", guiStyle, guiOptions))
                    {
                    }
                    if (GUILayout.Button("Save ", guiStyle, guiOptions))
                    {
                   //     string jsonStr = JsonUtility.ToJson(GraphMgr.Inst.graphForEdit);
                   //     FileIO.Local.Write("g_Tmp", jsonStr, "txt");
                    }
                    if (GUILayout.Button("Load ", guiStyle, guiOptions))
                    {
                        //string jsonStr  = FileIO.Local.Read("g_Tmp", "txt");
                      //  JsonUtility.FromJsonOverwrite(jsonStr, GraphMgr.Inst.graphForEdit);
                        //App.Inst.JSoner.PhaseVuArtInfo = JsonUtility.FromJson<JSoner_VuArtInfo>(jsonStr);
                        //                        ARs.Inst.LoadByJsonStr_ArtMetaInfo(jsonStr_Fp);
                    }
                }
                break;
            case eGUIType.TestMode2:
                {
                    _fpEditInt = GUILayout.Toolbar(_fpEditInt, _fpEditStrs, guiOptions);
                }
                break;
            case eGUIType.TestMode3:
                {
                }
                break;
        }

        //GUILayout.BeginHorizontal();
        //GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
    #endregion
}
}


    static class scr
    {
        public static float AspectMin = 1.5f;  //  for height
        public static float Aspect3_2 = 0.6666666667f;  //  for height
        public static float AspectW_H = 0.0f;  // for width
        public static float AspectH_W = 0.0f;  //  for height

        public static int Width = 0;
        public static int Height = 0;
        public static float OverWidth = 0;
        public static float OverHeight = 0;
        public static float HalfWidth = 0;
        public static float HalfHeight = 0;
        public static float HalfOverWidth = 0;
        public static float HalfOverHeight = 0;

        
            const int scrCommonHeight = 960;//920;//860// 960;//980;//1080;// 1280
#if UNITY_ANDROID || UNITY_IOS
            static int scrHeight = scrCommonHeight;//890// 960;//980;//1080;// 1280//1600
#else
            static int scrHeight = scrCommonHeight;
#endif
        public static void Awake()
        {
            Width = Screen.width;
            Height = Screen.height;
            OverWidth = 1 / Width;
            OverHeight = 1 / Height;
            HalfWidth = Screen.width * 0.5f;
            HalfHeight = Screen.height * 0.5f;
            HalfOverWidth = 1 / HalfWidth;
            HalfOverHeight = 1 / HalfHeight;

            AspectW_H = (float)Screen.width / (float)Screen.height;
            AspectH_W = (float)Screen.height / (float)Screen.width;

            
#if UNITY_EDITOR
                Height = Screen.height;
#elif UNITY_ANDROID || UNITY_IOS
                Height = Mathf.Min(Screen.currentResolution.height, scrHeight);
#else
                Height = Screen.height ;
#endif
                //  Width = Screen.width;
                //  Height = Screen.height;
                Width = (int)(AspectW_H * Height);
                //Debug.Log("1Screen.Height " + Height);
                //   Debug.Log("Screen.width " + Width);
                Screen.SetResolution(Width, Height, true);

        }
    }