using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineEx;

namespace nj
{
    public class ObjsPool<T, U> : Objs<T, U> where T : MonoBehaviour where U : cObj
    {
        Transform _rootClones;
        protected virtual short CapacityOfType(byte type) { return 0; }
        protected U[] _cObj = null;

        public U this[int cdx] { get { return _cObj[cdx]; } }

        protected override void _awake()
        {
            base._awake();

            _rootClones = transform.GetChild(1);

            short totalCapacity = 0;
            for (byte type = 0; type < NumType; ++type)
                totalCapacity += CapacityOfType(type);
            _cObj = new U[totalCapacity];

            short cntCdx = 0;
            for (byte type = 0; type < NumType; ++type)
            {
                int capacity = CapacityOfType(type);
                for (int i = 0; i < capacity; ++i)
                {
                    U obj = CloneObj(type, _rootClones);
                    obj.cdx = cntCdx++;
                    obj.gameObject.SetActive(false);
                    _cObj[obj.cdx] = obj;
                }
            }
        }



    }
}