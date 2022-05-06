using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineEx;

namespace nj
{
    public abstract class ObjsQuePool<T, U> : ObjsPool<T, U> where T : MonoBehaviour where U : qObj
    {
        protected QuePool<U>[] _pool = null;

        protected override void _awake()
        {
            base._awake();

            short cntCdx_atBegin = 0;
            _pool = new QuePool<U>[NumType];
            for (byte t = 0; t < NumType; ++t)
            {
                short capacity = CapacityOfType(t);
                _pool[t] = new QuePool<U>(cntCdx_atBegin, capacity, _cObj);
                cntCdx_atBegin += capacity;
            }
        }

        public void Unuse(U obj)
        {
            _pool[obj.type].Unuse(obj);
        }

        public void UnuseAllGamObj()
        {
            for (int type = 0; type < NumType; ++type)
                _pool[type].UnuseAll();
        }

    }
}