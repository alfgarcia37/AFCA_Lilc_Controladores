using Infragistics.Win;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Afca.Common
{
     
    public static class StringUtils
    {
      public static string GetInitials(string names, string separator)
      {
        // Extract the first character out of each block of non-whitespace
        Regex extractInitials = new Regex(@"\s*([^\s])[^\s]*\s*");
        string replace = extractInitials.Replace(names, "$1").ToUpper();
        return replace.Length >= 2 ? replace.Substring(0, 2) : replace;

      }
      public static string GetFormalCase(string text)
      {
        string[] parts = text.TrimEnd().ToLower().Split(' ');
        string result = "";

        foreach (string part in parts)
        {
          result += part[0].ToString().ToUpper();
          if (part.Length > 1)
            result += part.Substring(1, part.Length - 1);
          result += " ";
        }
        return result;
      }
      public static string ReplaceAccents(string text)
      {
        string toReplace = "áéíóúÁÉÍÓÚ";
        string replace = "aeiouAEIOU";

        for (int i = 0; i < toReplace.Length; i++)
          text = text.Replace(toReplace[i], replace[i]);

        return text;
      }
      public static string Btoa(string text)
      {
        byte[] utfdata = UTF8Encoding.UTF8.GetBytes(text);
        return Convert.ToBase64String(utfdata);
      }


      public static string ComputeSHA256Hash(string input)
      {
        using (SHA256 sha256Hash = SHA256.Create())
        {
          byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
          StringBuilder builder = new StringBuilder();
          for (int i = 0; i < bytes.Length; i++)
          {
            builder.Append(bytes[i].ToString("x2"));
          }
          return builder.ToString();
        }
      }

    }
  }
