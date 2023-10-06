using System.Collections.Generic;

namespace Ponta.Aplicacao.Utilidades
{
    public static class TratamentoDeLogs
    {
        public static List<string> CamposLogs(string campos)
        {
            List<string> listadecampos = new();
            var camposlogs = campos.Split("|");

            foreach (var campolog in camposlogs)
                listadecampos.Add(campolog);

            return listadecampos;
        }
        public static string FormatarLabel(string labellogLabel, List<string> campos)
        {
            string label = labellogLabel;
            if (campos != null && !string.IsNullOrEmpty(labellogLabel))
            {
                for (int i = 0; i < campos.Count; i++)
                    label = label.Replace(string.Concat("{", i.ToString(), "}"), campos[i]);
            }


            return label;
        }
    }
}
