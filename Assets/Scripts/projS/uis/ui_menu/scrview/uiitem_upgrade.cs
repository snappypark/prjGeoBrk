using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Mosframe;



public class uiitem_upgrade : UIBehaviour, IDynamicScrollViewItem
{
    [SerializeField] Text _name;
    [SerializeField] Text _lv;
    [SerializeField] Text _price;
    public void onUpdateItem(int index, int totlaIdx)
    {
       // _name.text = upgTables.tables[index].name;
     //   int lv = 0;//= user.upg_lv(index);
      //  _lv.text = string.Format("Lv. {0}", lv);
     //   _price.text = upgTables.tables[index].prices[lv].GetValue().ToString();
        //   _idx = index;
        //   _text.text = string.Format("{0}", (index + 1 - gapIdx));
    }


}
