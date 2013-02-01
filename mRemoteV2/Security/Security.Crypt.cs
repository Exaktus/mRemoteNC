using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using AxMSTSCLib;
using AxWFICALib;
using Microsoft.VisualBasic;
using mRemoteNC.App;
using My;

//using mRemoteNC.Runtime;

namespace mRemoteNC
{
    namespace Security
    {
        public class Crypt
        {
            public static string Encrypt(string StrToEncrypt, string StrSecret)
            {
                if (StrToEncrypt == "" || StrSecret == "")
                {
                    return StrToEncrypt;
                }

                try
                {
                    RijndaelManaged rd = new RijndaelManaged();

                    MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                    byte[] key = md5.ComputeHash(Encoding.UTF8.GetBytes(StrSecret));

                    md5.Clear();
                    rd.Key = key;
                    rd.GenerateIV();

                    byte[] iv = rd.IV;
                    MemoryStream ms = new MemoryStream();

                    ms.Write(iv, 0, iv.Length);

                    CryptoStream cs = new CryptoStream(ms, rd.CreateEncryptor(), CryptoStreamMode.Write);
                    byte[] data = System.Text.Encoding.UTF8.GetBytes(StrToEncrypt);

                    cs.Write(data, 0, data.Length);
                    cs.FlushFinalBlock();

                    byte[] encdata = ms.ToArray();
                    cs.Close();
                    rd.Clear();

                    return Convert.ToBase64String(encdata);
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        string.Format(Language.strErrorEncryptionFailed, ex.Message));
                }

                return StrToEncrypt;
            }

            public static string Decrypt(string StrToDecrypt, string StrSecret)
            {
                if (StrToDecrypt == "" || string.IsNullOrEmpty(StrSecret))
                {
                    return StrToDecrypt;
                }

                try
                {
                    RijndaelManaged rd = new RijndaelManaged();
                    int rijndaelIvLength = 16;
                    MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                    byte[] key = md5.ComputeHash(Encoding.UTF8.GetBytes(StrSecret));

                    md5.Clear();

                    byte[] encdata = Convert.FromBase64String(StrToDecrypt);
                    MemoryStream ms = new MemoryStream(encdata);
                    byte[] iv = new byte[16];

                    ms.Read(iv, 0, rijndaelIvLength);
                    rd.IV = iv;
                    rd.Key = key;

                    CryptoStream cs = new CryptoStream(ms, rd.CreateDecryptor(), CryptoStreamMode.Read);

                    byte[] data = new byte[ms.Length - rijndaelIvLength + 1];
                    int i = cs.Read(data, 0, data.Length);

                    cs.Close();
                    rd.Clear();

                    return System.Text.Encoding.UTF8.GetString(data, 0, i);
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                        string.Format(Language.strErrorDecryptionFailed, ex.Message));
                }

                return StrToDecrypt;
            }
        }
    }
}