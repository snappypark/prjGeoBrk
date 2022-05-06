using UnityEngine;

public static class LangTable
{
    static SystemLanguage _lang { get {
#if UNITY_EDITOR
            return SystemLanguage.English;
            //return SystemLanguage.Japanese;
            //return SystemLanguage.English;
#else
            return Application.systemLanguage;
#endif
        }
    }

    public static string Version(bool isTest)
    {
        return isTest ? string.Format("v.{0}", Application.version) : string.Format("v{0}", Application.version);
    }
    
    public static string MoveJoystic()
    {
        switch (_lang) {
            case SystemLanguage.Korean: return "조이스틱을 움직여주세요.";
            case SystemLanguage.Japanese: return "ジョイスティックを使って遊ぶ.";
            default: return "MOVE THE JOYSTICK TO PLAY";
        }
    }

    public static string Stage(int stage, int maxStage)
    {
        if (stage >= maxStage)
            return string.Format("THE END {0}", stage) ;
        
        switch (_lang) {
            case SystemLanguage.Korean: return string.Format("스테이지 {0}", stage);
            case SystemLanguage.Japanese: return string.Format("ステージ {0}", stage);
            default: return string.Format("STAGE {0}", stage);
        }
    }

    public static string UninstallLoss()
    {
        switch (_lang) {
            case SystemLanguage.Korean: return "앱을 제거시 게임 데이터가 저장되지 않습니다.";
            case SystemLanguage.Japanese: return "Game data cannot be recovered once the app is deleted.";
            default: return "Game data cannot be recovered once the app is deleted.";
        }
    }

    public static string ExitGame()
    {
        switch (_lang) {
            case SystemLanguage.Korean: return "게임 종료 말씀이시군요?";
            case SystemLanguage.Japanese: return "ゲームを終了しますか？";
            default: return "QUIT THE GAME?";
        }
    }

    public static string Yes()
    {
        switch (_lang) {
            case SystemLanguage.Korean: return "네";
            case SystemLanguage.Japanese: return "はい";
            default: return "Yes";
        }
    }

    public static string No()
    {
        switch (_lang) {
            case SystemLanguage.Korean: return "아니오";
            case SystemLanguage.Japanese: return "いいえ";
            default: return "No";
        }
    }

    public static string LvUp(int curLv, int LastLv)
    {
        return curLv >= LastLv ? "Max Level" : "Level Up";
    }
}
