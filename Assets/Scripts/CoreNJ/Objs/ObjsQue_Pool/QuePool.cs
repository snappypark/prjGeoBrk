using System.Collections.Generic;
using UnityEngine;
using UnityEngineEx;

namespace nj
{
    public class QuePool<U> where U : qObj
    {
        public short Capacity { get { return _capacity; } }
        public short Num { get { return (short)(_capacity - _waitCdxes.Count); } }

        short _capacity = 0;
        short _cdx_begin = 0;

        Queue<short> _waitCdxes = new Queue<short>();

        U[] _objs = null;

        public QuePool(short cdx_begin, short capacity, U[] objs)
        {
            _objs = objs;
            _capacity = capacity;
            _cdx_begin = cdx_begin;

            for (short i = 0; i < _capacity; ++i)
                _waitCdxes.Enqueue((short)(_cdx_begin + i));
        }

        public U Reuse(Vector3 pos)
        {
            if (_waitCdxes.Count == 0)
                return null;

            U obj = _objs[_waitCdxes.Dequeue()];
            obj.transform.localPosition = pos;
            obj.gameObject.SetActive(true);
            return obj;
        }

        public void Unuse(U obj)
        {
            _waitCdxes.Enqueue(obj.cdx);
            obj.transform.localPosition = VectorEx.Huge;
            obj.gameObject.SetActive(false);
        }

        public void UnuseAll()
        {
            _waitCdxes.Clear();
            for (short i = 0; i < _capacity; ++i)
                Unuse(_objs[_cdx_begin + i]);
        }
    }
}