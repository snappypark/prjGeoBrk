using nj;

public static class lvDesign
{
    public const int LastLv = 120;

    public static int GetMaxExp()
    {
        switch (User.Inst.CurLv)
        {
            case 1:
                return 10;
        }

        scInt add = (int)(User.Inst.CurLv / 5);
        scInt add2 = (int)(User.Inst.CurLv / 9);
        scInt add3 = (int)(User.Inst.CurLv / 15);
        scInt add4 = (int)(User.Inst.CurLv / 21);
        scInt add5 = (int)(User.Inst.CurLv / 27);
        scInt add6 = (int)(User.Inst.CurLv / 36);
        int lv = User.Inst.CurLv % 30;
        switch (lv)
        {
            case 1: return 90;
            case 2: return 120;
            case 3: return 180;
            case 4: return 200;
            default:
                return 210 + add+ add2 + add3 + add4 + add5 + add6;
        }
    }

    public static int GetMaxLife()
    {
        int add = (int)(User.Inst.CurLv * 0.03f); //  0.02 = 1/50
        return 3 + add;
        /*switch (User.Inst.CurLv)
        {
            case 1:
                return 5;
            case 2:
                return 6;
            default:
                return 4 + User.Inst.CurLv.GetValue();
        }*/
    }
}


public static class edgeDesign
{
    public static int HpOfLv(int lv)
    {
        switch (lv)
        {
            case 1: return 15;
            case 2: return 27;
            case 3: return 46;
            case 4: return 69;
            case 5: return 76;
            case 6: return 87;
            case 7: return 74;

            case 8: return 436;
            case 9: return 584;
            case 10: return 981;
            default:
                return 0;
        }
    }
}

public static class ballDesign
{
    public static int DmgOfLv(int lv)
    {
        switch (lv)
        {
            case 1: return 7;
            case 2: return 16;
            case 3: return 35;

            case 4: return 62;
            case 5: return 89;
            case 6: return 140;
            case 7: return 210;

            case 8: return 80;
            case 9: return 90;
            case 10: return 100;
            default:
                return 7;
        }
    }
}
