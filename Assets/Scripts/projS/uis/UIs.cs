using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nj;

public class UIs : MonoSingleton<UIs>
{
    [SerializeField] RectTransform CanvasRect;

    [SerializeField] public ui_editwave EditWave;

    [SerializeField] public ui_frame Frame;

    [SerializeField] public ui_playing Playing;

    [SerializeField] public ui_menu Menu;

    [SerializeField] public ui_entry Entry;
    [SerializeField] public ui_hud Hud;
    [SerializeField] public ui_cover Cover;

    //[SerializeField] public popups Popups;
    [SerializeField] public ui_ads Ads;

    [SerializeField] public ui_popup Popup;
    [SerializeField] public ui_backBtn BackBtn;

    public Vector2 WorldToCanvas(Vector3 pos)
    {
        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(pos);
        return new Vector2( (ViewportPosition.x - 0.5f) * CanvasRect.sizeDelta.x,
                            (ViewportPosition.y - 0.5f) * CanvasRect.sizeDelta.y );
    }
    /*
    //now you can set the position of the ui element
    UI_Element.anchoredPosition = WorldObject_ScreenPosition;
    */
    static delay _delayBtn = new delay(0.7f);
    public static bool IsWaitingBtn(float waitTime = 0.7f)
    {
        bool wait = _delayBtn.InTime();
        if (!wait)
            _delayBtn.Reset(waitTime);
        return wait;
    }
}
