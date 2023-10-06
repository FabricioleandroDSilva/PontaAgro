using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ponta.Dominio.Enumeradores
{
    public static class ExtensaoEnumerador
    {
        public static string RetornarDescricao(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            if (field.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Length > 0)
            {
                return attributes[0].Description;
            }

            return value.ToString();
        }

        public static T RetornarValorDescricao<T>(string description) where T : Enum
        {
            foreach (FieldInfo field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
            }

            throw new ArgumentException($"Valor de enumeração com a descrição '{description}' não encontrado em { typeof(T).Name }");
        }
    }
}
