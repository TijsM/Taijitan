using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Taijitan.Helpers
{
    public static class EnumHelpers
    {
        public static SelectList ToSelectList<TEnum>()
        {
            var values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>()
                    .Select(e => new { Id = e, Name = e.GetDisplayName() });
            return new SelectList(values, "Id", "Name");
        }

        public static string GetDisplayName<TEnum>(this TEnum enumValue)
        {
            return typeof(TEnum).GetMember(enumValue.ToString())[0]
                           .GetCustomAttribute<DisplayAttribute>()?
                           .Name ?? enumValue.ToString();
        }



        public static string GetDescription(this Enum GenericEnum)
        {
            Type genericEnumType = GenericEnum.GetType();
            MemberInfo[] memberInfo = genericEnumType.GetMember(GenericEnum.ToString());
            if ((memberInfo != null && memberInfo.Length > 0))
            {
                var _Attribs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if ((_Attribs != null && _Attribs.Count() > 0))
                {
                    return ((System.ComponentModel.DescriptionAttribute)_Attribs.ElementAt(0)).Description;
                }
            }
            return GenericEnum.ToString();
        }
    }
}