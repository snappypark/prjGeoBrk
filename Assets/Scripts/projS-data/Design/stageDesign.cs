public static class stageDesign
{
    public const int OverStageIdx = 1000000000;
    public const int MaxStageIdx = 299;
    public const int LoopCnt = 50;

    public static int GetNumWave()
    {
        int stageIdx = User.Inst.CurStageIdx % stageDesign.LoopCnt;
        switch (stageIdx)
        {
            #region 0 - 50
            case 0: return 2;   //ct    lv 1, 0
            case 1: return 4;   //xy 
            case 2: return 8;   //xy 
            case 3: return 3;   //xy
            case 4: return 1;   //ou

            case 5: return 2;   //xy
            case 6: return 2;   //xy
            case 7: return 10;  //xy
            case 8: return 2;   //ou
/*min 10*/  case 9: return 2;   //xy

            case 10: return 1;  //ct 
            case 11: return 2;  //ou    lv 9
            case 12: return 1;  //xy
            case 13: return 2;  //xy
            case 14: return 2;  //ou

            case 15: return 2;  //ct
            case 16: return 1;  //xy
            case 17: return 6;  //
            case 18: return 4;  //ct
            case 19: return 4;  //ct

            case 20: return 2;  //ou    lv 16 
            case 21: return 1;  //xy 
            case 22: return 2;  //ou 
            case 23: return 1;  //ct
            case 24: return 1;  //xy 

            case 25: return 3;  //xy
            case 26: return 2;  //ct
            case 27: return 2;  //ou
            case 28: return 2;  //ct
            case 29: return 2;  //ct

            case 30: return 1;  //xy    lv 22, 0
            case 31: return 3;  //ct
            case 32: return 2;  //xy
            case 33: return 4;  //ou
            case 34: return 2;  //ct

            case 35: return 1;  //xy 
            case 36: return 8;  //ct
            case 37: return 2;  //ot
            case 38: return 2;  //xy
            case 39: return 8;  //ct

            case 40: return 1;  //xy    lv 28, 
            case 41: return 1;  //xy
            case 42: return 1;  //xy
            case 43: return 1;  //ou
            case 44: return 6;  //ct

            case 45: return 2;  //xy    lv 34, 0
            case 46: return 2;  //ct
            case 47: return 2;  //xy
            case 48: return 1;  //xy
            case 49: return 2;  //ct
            #endregion

            case 50: return 0;  //ct    lv 32, 0
            case 100: return 0;  //ct    lv 62, 0
            case 150: return 0;  //ct    lv 92, 0

            default: return 0;
        }
    }
}
