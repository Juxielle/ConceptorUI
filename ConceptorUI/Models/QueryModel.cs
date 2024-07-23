using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ConceptorUI.Models
{
    internal class QueryModel
    {
        public List<string>? Tables { get; set; }
        public List<string>? Projections { get; set; }
        public List<string>? DefaultValues { get; set; }
        public List<string>? Selections { get; set; }
        public List<List<string>>? Datas { get; set; }
        public QueryModel? Child { get; set; }

        public string GetData(string fieldCode, int dataIndex, int level)
        {
            if(level == 1)
            {
                foreach(var pj in Projections!)
                    if (pj == fieldCode) return Datas![dataIndex][Projections.IndexOf(pj)];
            }
            else if(level > 1)
            {
                return Child!.GetData(fieldCode, dataIndex, level - 1);
            }
            return "NAN";
        }

        public int GetDataSize(int level)
        {
            if (level == 1)
                return Datas!.Count;
            else if (level > 1)
                return Child!.GetDataSize(level - 1);
            return 0;
        }
    }
}
