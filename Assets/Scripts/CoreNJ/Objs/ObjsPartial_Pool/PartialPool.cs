using System.Collections.Generic;
using System.Collections.GenericEx;
using UnityEngine;
using UnityEngineEx;

namespace nj
{
    public class PartialPool<U> where U : pqObj
    {
        public short Capacity { get { return _capacity; } }
        public short Num { get { return (short)(_capacity - _waitCdxes.Count); } }
        public int Remain { get { return _waitCdxes.Count; } }

        short _capacity = 0;
        short _cdx_begin = 0;

        Queue<short> _waitCdxes = new Queue<short>();

        U[] _objs = null;

        Queue<short>[,] _q = null;
        public Queue<short> this[Pt pt] { get { return _q[pt.x, pt.y]; } }
        public Queue<short> this[int idxX, int idxY] { get { return _q[idxX, idxY]; } }

        public PartialPool(short cdx_begin, short capacity, U[] objs)
        {
            _cdx_begin = cdx_begin;
            _objs = objs;
            _capacity = capacity;

            for (short i = 0; i < _capacity; ++i)
                _waitCdxes.Enqueue((short)(_cdx_begin + i));

            _q = new Queue<short>[coord.size.x, coord.size.y];
            for (int x = 0; x < coord.size.x; ++x)
                for (int y = 0; y < coord.size.y; ++y)
                    _q[x, y] = new Queue<short>();
        }

        public void Move(U obj)
        {
            if (coord.IsOutOfPos_OnPartial(obj.transform.localPosition))
            {
                if (obj.IsInPartial &&
                    this[obj.pdx].DequeueOne(obj.cdx))
                    obj.pdx = Pt.Huge;
                obj.IsInPartial = false;
            }
            else
            {
                Pt idxPt = obj.transform.Pt();
                if (obj.pdx != idxPt)
                {
                    if (obj.IsInPartial)
                        this[obj.pdx].DequeueOne(obj.cdx);
                    else
                        obj.IsInPartial = true;
                    obj.pdx = idxPt;
                    this[obj.pdx].Enqueue(obj.cdx);
                }
            }
        }

        public U Reuse(Vector3 pos)
        {
            if (_waitCdxes.Count == 0)
                return null;

            U obj = _objs[_waitCdxes.Dequeue()];
            obj.transform.localPosition = pos;
            obj.pdx = Pt.Huge;
            obj.gameObject.SetActive(true);

            Move(obj);

            return obj;
        }

        public void Unuse(U obj)
        {
            if (obj.IsInPartial)
                this[obj.pdx].DequeueOne(obj.cdx);
            _waitCdxes.Enqueue(obj.cdx);
            obj.OnUnuse();
            obj.transform.localPosition = VectorEx.Huge;
            obj.pdx = Pt.Huge;
            obj.IsInPartial = false;
            obj.gameObject.SetActive(false);
        }

        public void UnuseAll()
        {
            for (int i = 0; i < coord.size.x; ++i)
                for (int j = 0; j < coord.size.y; ++j)
                    this[i, j].Clear();
            _waitCdxes.Clear();
            for (short i = 0; i < _capacity; ++i)
            {
                short cdx = (short)(_cdx_begin + i);
                _waitCdxes.Enqueue(cdx);

                U obj = _objs[cdx];
                obj.OnUnuse(cObj.eUnuse.AllClear);
                obj.transform.localPosition = VectorEx.Huge;
                obj.pdx = Pt.Huge;
                obj.IsInPartial = false;
                obj.gameObject.SetActive(false);
            }
        }

    }
}