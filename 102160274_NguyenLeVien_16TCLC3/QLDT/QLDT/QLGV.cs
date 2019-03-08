using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDT
{
    class QLGV
    {
        List<GV> l;
        public List<GV> GetListGV()
        {
            return this.l;
        }
        public List<GV> AddGV(string m)
        {
            GV s = new GV();
            for (int i = 0; i < this.l.Count; i++)
            {
                if (this.l[i].MaGV != m)
                {
                    this.l.Add(s);
                }
            }
            return this.l;
        }
    }
}
