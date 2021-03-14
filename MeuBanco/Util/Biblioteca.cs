using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;


using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;

namespace JGourmet.UTIL
{
    public static class Biblioteca
    {
        public static bool Invalido { get; set; }
        public static StringBuilder DadosInvalido { get; set; }
        private static TextBox _txtAuxiliar;

      
        private static TAttribute GetEnumAttribute<TAttribute>(this Enum enumVal) where TAttribute : Attribute
        {
            var memberInfo = enumVal.GetType().GetMember(enumVal.ToString());
            return memberInfo[0].GetCustomAttributes(typeof(TAttribute), false).OfType<TAttribute>().FirstOrDefault();
        }
        public static string GetEnumDescription(this Enum enumValue)
            => enumValue.GetEnumAttribute<DescriptionAttribute>()?.Description ?? enumValue.ToString();

        public static decimal Add(this decimal valor1, decimal valor2)
        {
            return decimal.Add(valor1, valor2);
        }

        public static decimal Subtract(this decimal valor1, decimal valor2)
        {
            return decimal.Subtract(valor1, valor2);
        }

        public static decimal Multiply(this decimal valor1, decimal valor2)
        {
            return decimal.Multiply(valor1, valor2);
        }

        public static decimal Divide(this decimal valor1, decimal valor2)
        {
            return decimal.Divide(valor1, valor2);
        }

        //public static decimal arredondar(this decimal valor, int casasDecimais)
        //{
        //    var valorNovo = decimal.Round(valor, casasDecimais);
        //    var valorNovoStr = valorNovo.ToString("F" + casasDecimais, CultureInfo.CurrentCulture);
        //    return decimal.Parse(valorNovoStr);
        //}


        //public static bool StringToDecimal(this object retorno)
        //{
        //    if (retorno == null)
        //    {
        //        return decimal.Zero;
        //    }

        //    var resultado = decimal.Zero;
        //    return decimal.TryParse(retorno.ToString(), out resultado);
        //}

        public static decimal ToDecimal(this string retorno)
        {
            try
            {
                return (string.IsNullOrEmpty(retorno)) ? 0M : decimal.Parse(retorno);
            }
            catch
            {
                return 0M;
            }

        }
        public static int ToInt(this string retorno)
        {
            try
            {
                return (string.IsNullOrEmpty(retorno)) ? 0 : int.Parse(retorno);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int StringToInt(this object retorno)
        {
            int.TryParse(retorno.ToString(), out var resultado);
            return resultado;
        }

        public static DateTime StringToDateTime(this string retorno)
        {
            return DataValida(retorno) ? DateTime.Parse(retorno) : DateTime.Now.Date;
        }

        public static byte[] ImageToByte(this Image image)
        {
            var converter = new ImageConverter();
            return (byte[])converter.ConvertTo(image, typeof(byte[]));
        }

        public static string DecimalToStringComPonto(this decimal valor)
        {
            return valor.ToString().Replace(',', '.');
        }

        public static Image ByteToImage(this byte[] imagem)
        {
            if (imagem == null)
            {
                return null;
            }

            var converter = new ImageConverter();
            var image = (Image)converter.ConvertFrom(imagem);
            return image;
        }

        //  Valida o CNPJ digitado 
        public static bool IsCNPJ(this string cnpj, bool NaoContribuinte = false)
        {
            var multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "").Replace(",", "");

            if (cnpj.Length != 14)
            {
                return false;
            }

            if (cnpj.Equals("9".PadLeft(14, '9')) && NaoContribuinte)
            {
                return true;
            }

            if (cnpj.Equals("0".PadLeft(14, '0')) ||
               cnpj.Equals("1".PadLeft(14, '1')) ||
               cnpj.Equals("2".PadLeft(14, '2')) ||
               cnpj.Equals("3".PadLeft(14, '3')) ||
               cnpj.Equals("4".PadLeft(14, '4')) ||
               cnpj.Equals("5".PadLeft(14, '5')) ||
               cnpj.Equals("6".PadLeft(14, '6')) ||
               cnpj.Equals("7".PadLeft(14, '7')) ||
               cnpj.Equals("8".PadLeft(14, '8')) ||
               cnpj.Equals("9".PadLeft(14, '9'))
               )
            {
                return false;
            }

            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;

            for (var i = 0; i < 12; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            }

            resto = (soma % 11);

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;

            for (var i = 0; i < 13; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            }

            resto = (soma % 11);

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }

        public static bool IsCpf(this string cpf)
        {
            var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11
                || cpf.Equals("00000000000")
                || cpf.Equals("11111111111")
                || cpf.Equals("22222222222")
                || cpf.Equals("33333333333")
                || cpf.Equals("44444444444")
                || cpf.Equals("55555555555")
                || cpf.Equals("66666666666")
                || cpf.Equals("77777777777")
                || cpf.Equals("88888888888")
                || cpf.Equals("99999999999"))
            {
                return false;
            }

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (var i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            }

            resto = soma % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (var i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }

            resto = soma % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        public static string GetCodigoBarras(this string codigoBarras)
        {
            if (string.IsNullOrWhiteSpace(codigoBarras) ||
                !(codigoBarras.Length == 8 ||
                  codigoBarras.Length == 12 ||
                  codigoBarras.Length == 13 ||
                  codigoBarras.Length == 14)
               )
            {
                return "SEM GTIN";
            }
            return codigoBarras;
        }

        public static string FormatarCpfCnpj(this string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 11)
            {
                return value;
            }
            var valueLong = long.Parse(value);
            if (value.Trim().Length == 11)
            {
                value = string.Format(@"{0:000\.000\.000\-00}", valueLong);
            }
            else if (value.Trim().Length == 14)
            {
                value = string.Format(@"{0:00\.000\.000\/0000\-00}", valueLong);
            }
            return value;
        }

        public static string FormatarTelefone(this string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 10)
            {
                return value;
            }
            var valueLong = long.Parse(value.RemoverPontos());
            if (value.Trim().Length == 10)
            {
                value = string.Format(@"{0:(00) 0000-0000}", valueLong);
            }
            else if (value.Trim().Length == 11)
            {
                value = string.Format(@"{0:(00) 00000-0000}", valueLong);
            }
            return value;
        }

        public static string FormatarCep(this string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !IsNumeric(value) || value.Length < 8)
            {
                return value;
            }

            var valueLong = long.Parse(value);
            value = string.Format("{0:00000-000}", valueLong);
            return value;
        }

        //  Valida o CPF digitado 
        public static bool ValidaCPF(string cpf, bool naoContribuinte = false)
        {
            // Caso coloque todos os numeros iguais
            var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
            {
                return false;
            }

            if (cpf.Equals("9".PadLeft(11, '9')) && naoContribuinte)
            {
                return true;
            }

            if (cpf.Equals("0".PadLeft(11, '0')) ||
                cpf.Equals("1".PadLeft(11, '1')) ||
                cpf.Equals("2".PadLeft(11, '2')) ||
                cpf.Equals("3".PadLeft(11, '3')) ||
                cpf.Equals("4".PadLeft(11, '4')) ||
                cpf.Equals("5".PadLeft(11, '5')) ||
                cpf.Equals("6".PadLeft(11, '6')) ||
                cpf.Equals("7".PadLeft(11, '7')) ||
                cpf.Equals("8".PadLeft(11, '8')) ||
                cpf.Equals("9".PadLeft(11, '9'))
                )
            {
                return false;
            }

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (var i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            }

            resto = soma % 11;

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (var i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }

            resto = soma % 11;

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        public static decimal TruncaValor(decimal? Value, int Casas)
        {
            string valor;
            int pos;


            valor = Value.ToString();


            pos = valor.IndexOf(",");
            if (pos > 0)
            {
                valor = valor.Substring(0, pos + Casas);
            }

            return Convert.ToDecimal(valor);
        }



        public static decimal Multiplicar(decimal valor1, decimal valor2)
        {
            var result = decimal.Zero;
            return result = decimal.Multiply(valor1, valor2);
        }

        public static bool IsNumeric(string valor)
        {
            try
            {
                var vlr = decimal.Parse(valor);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string RemoverAcentos(this string text)
        {
            if (text == null)
                return null;

            text = text.Trim().
                        Replace("\r", "").
                        Replace("\t", "").
                        Replace("\n", "").
                        Replace("[^\\p{ASCII}]", "").
                        Replace("[^\\p{ASCII}]", "").
                        Replace("\"", "").
                        Replace("'", "");

            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            var sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (var letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                {
                    sbReturn.Append(letter);
                }
            }
            return sbReturn.ToString();
        }

        public static bool DataValida(string data)
        {
            var resultado = DateTime.MinValue;
            if (DateTime.TryParse(data, out resultado))
            {
                return true;
            }

            return false;
        }

        public static string ObterHexDeString(string s)
        {
            var hex = "";
            foreach (var c in s)
            {
                int tmp = c;
                hex += string.Format("{0:x2}", Convert.ToUInt32(tmp.ToString()));
            }
            return hex;
        }

        public static string ObterHexSha1DeString(string s)
        {
            var bytes = Encoding.UTF8.GetBytes(s);

            var sha1 = SHA1.Create();
            var hashBytes = sha1.ComputeHash(bytes);

            return ObterHexDeByteArray(hashBytes);
        }

        private static string ObterHexDeByteArray(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (var b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }

      
        public static string Encripta(this string pChave)
        {
            string chaveCriptografada;
            var b = ASCIIEncoding.ASCII.GetBytes(pChave);
            chaveCriptografada = Convert.ToBase64String(b);
            return chaveCriptografada;
        }

        public static string Desencripta(this string pChave)
        {
            string chaveDecriptografada;
            var b = Convert.FromBase64String(pChave);
            chaveDecriptografada = ASCIIEncoding.ASCII.GetString(b);
            return chaveDecriptografada;
        }

        public static string MD5String(this string texto)
        {
            var md5 = new MD5CryptoServiceProvider();
            var byteArray = Encoding.ASCII.GetBytes(texto);

            byteArray = md5.ComputeHash(byteArray);
            var hashedValue = new StringBuilder();

            foreach (var b in byteArray)
            {
                hashedValue.Append(b.ToString("x2"));
            }

            return hashedValue.ToString();
        }

      
        public static bool IsEmail(this string email)
        {
            var regExpEmail = new Regex("^[A-Za-z0-9](([_.-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([.-]?[a-zA-Z0-9]+)*)([.][A-Za-z]{2,4})$");
            var match = regExpEmail.Match(email);

            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string FormatarChaveNFce(string chave, bool quebra = true)
        {
            if (chave == "")
            {
                return "";
            }

            var palavraFormatada = new StringBuilder();
            var posicao = 0;
            for (var i = 0; i < 11; i++)
            {
                if (quebra)
                {
                    if (i == 7)
                    {
                        palavraFormatada.Append("\n" + new string(' ', 10));
                    }
                }

                palavraFormatada.Append(chave.Substring(posicao, 4) + " ");
                posicao += 4;
            }

            return palavraFormatada.ToString();
        }



        public static string InputDialogValor(string titulo, string nomeCampo, string valor = "", bool campoNumerico = false)
        {
            var form = new Form();
            var label = new Label();
            var textBox = new TextBox();
            var buttonUp = new Button();
            var buttonDow = new Button();
            var buttonOk = new Button();
            var buttonCancel = new Button();
            form.Text = titulo;
            label.Text = nomeCampo;
            textBox.Text = valor;

            buttonUp.Click += AumentarQuantidade;
            buttonDow.Click += DiminuirQuantidade;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            _txtAuxiliar = textBox;
            if (campoNumerico)
            {
                textBox.Font = new Font("Microsoft Sans Serif", 14.25F);
                textBox.ReadOnly = true;
                textBox.TextAlign = HorizontalAlignment.Center;
            }


            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            buttonUp.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
            //  buttonDow.Anchor = buttonDow.Anchor | AnchorStyles.Right;
            var tamanhoForm = (!campoNumerico) ? 396 : 490;
            form.ClientSize = new Size(tamanhoForm, 107);
            if (!campoNumerico)
            {
                form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            }
            else
            {
                form.Controls.AddRange(new Control[] { label, textBox, buttonUp, buttonDow, buttonOk, buttonCancel });
            }

            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;

            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            if (form.ShowDialog() == DialogResult.Cancel)
            {
                return "";
            }

            return textBox.Text;

        }

        private static void DiminuirQuantidade(object sender, EventArgs e)
        {
            if (_txtAuxiliar != null)
            {
                var valor = _txtAuxiliar.Text.ToInt();
                if (valor > 1)
                {
                    valor--;
                }

                _txtAuxiliar.Text = valor.ToString();
            }
        }

        private static void AumentarQuantidade(object sender, EventArgs e)
        {
            if (_txtAuxiliar != null)
            {
                var valor = _txtAuxiliar.Text.ToInt();
                valor++;
                _txtAuxiliar.Text = valor.ToString();
            }
        }

        public static bool ValidarSchema(string xmlFilename, string schemaFilename)
        {
            if (!File.Exists(xmlFilename))
            {
                DadosInvalido = new StringBuilder();
                DadosInvalido.Append("XML para validação não Localizado\n" + xmlFilename);

                return false;
            }

            if (!File.Exists(schemaFilename))
            {
                DadosInvalido = new StringBuilder();
                DadosInvalido.Append("Schema para validar o  evento  não localizado");
                return false;
            }

            DadosInvalido = new StringBuilder();
            // Define o tipo de validação
            var settings = new XmlReaderSettings();
            var resolver = new XmlUrlResolver
            {
                Credentials = CredentialCache.DefaultCredentials
            };
            settings.XmlResolver = resolver;
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas.Add("http://www.portalfiscal.inf.br/nfe", schemaFilename);
            //  Carrega o arquivo de esquema
            var schemas = new XmlSchemaSet();
            settings.Schemas = schemas;
            // Quando carregar o eschema, especificar o namespace que ele valida
            // e a localização do arquivo 

            schemas.Add("http://www.portalfiscal.inf.br/nfe", schemaFilename);
            // Especifica o tratamento de evento para os erros de validacao
            settings.ValidationEventHandler += ValidationEventHandler;
            // cria um leitor para validação
            using (var fs = new FileStream(xmlFilename, FileMode.Open, FileAccess.Read))
            {
                var validator = XmlReader.Create(xmlFilename, settings);


                Invalido = false;
                try
                {
                    // Faz a leitura de todos os dados XML
                    while (validator.Read())
                    { }
                }
                catch (XmlException err)
                {
                    // Um erro ocorre se o documento XML inclui caracteres ilegais
                    // ou tags que não estão aninhadas corretamente
                    Console.WriteLine("Ocorreu um erro critico durante a validacao XML.");
                    Console.WriteLine(err.Message);
                    Invalido = true;
                }
                finally
                {
                    validator.Close();

                }
            }
            return !Invalido;


        }

        private static void ValidationEventHandler(object sender, ValidationEventArgs args)
        {
            Invalido = true;
            var erro = args.Message.Replace("O elemento 'http://www.portalfiscal.inf.br/nfe:", "");
            erro = erro.Replace("' é inválido dependendo do tipo de dados 'String' - Falha na restrição Pattern.", "");
            erro = erro.Replace(" O valor '", "");
            erro = erro.Replace("' é inválido ", "");
            DadosInvalido.Append(erro + "\n");
            Console.WriteLine("Erros da validação : " + args.Message);
            Console.WriteLine();
        }

        public static int GetLineFromCursorPosition(this RichTextBox self)
        {
            return self.GetLineFromCharIndex(self.SelectionStart) + 1;
        }

        public static int GetColumnFromCursorPosition(this RichTextBox self)
        {
            var rowStartIndex = Win32.SendMessage(self.Handle,
                Win32.EM_LINEINDEX, -1, 0);

            return self.SelectionStart - rowStartIndex + 1;
        }

        private class Win32
        {
            public const int EM_LINEINDEX = 0xBB;

            [DllImport("User32.Dll")]
            public static extern int SendMessage(IntPtr hWnd, int Msg,
                int wParam, int lParam);
        }

        public static string TextoEmailNfce(string razaoSocial, int numNfce, string chaveAcesso, string protocolo, string qrCode)
        {
            var str = @"<style type='text/css'>table{font-family:'Trebuchet MS', Arial, Helvetica, sans-serif;width:100%;border-collapse:collapse;}table td,th {border: 1px solid #CC0000;padding: 3px 7px 2px 7px;}table th {font-size: 1.2em;text-align: left;padding-top: 5px;padding-bottom: 4px;background-color: #990000;color: #fff;}table tr.alt td {color: #000;background-color: #D6EDFF;}</style> </head> <body> <table> <tr> <td colspan=3>Enviado por: " + razaoSocial + @"</td> </tr> <tr> <th>Nota</th> <th>Chave</th> <th>Protocolo</th> </tr> <tr> <td>" + numNfce + @"</td> <td>" + chaveAcesso + @"</td> <td>" + protocolo + @"</td> </tr> <tr> <td colspan=3><a href='" + qrCode + @"'>QRCODE</a></td> </tr> <tr> <td colspan=3>Aplicativo JGourmet v1.00<br>JAMSOFT Informática - (79) 3431-1310 <br>www.jamsoft.com.br</td> </tr> </table> </body>";

            return str;
        }

      
        public static string GetProcessPath(string name)
        {
            try
            {
                foreach (var pPath in Process.GetProcesses())
                {
                    if (pPath.ProcessName.ToString().ToUpper().Contains(name.ToUpper()))
                    {
                        var fullpath = pPath.MainModule.FileName;
                        return fullpath;
                    }
                }
            }
            catch { }
            return string.Empty;
        }

        public static List<string> GetVersaoNetFramWork()
        {
            var versoes = new List<string>();
            using (var regKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, "").OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\"))
            {

                foreach (var versionKeyName in regKey.GetSubKeyNames())
                {
                    if (versionKeyName.StartsWith("v"))
                    {

                        var versionKey = regKey.OpenSubKey(versionKeyName);
                        var versao = (string)versionKey.GetValue("Version", "");

                        if (versao != "")
                        {
                            versoes.Add(versao);
                            continue;
                        }
                        foreach (var subKeyName in versionKey.GetSubKeyNames())
                        {
                            var subKey = versionKey.OpenSubKey(subKeyName);
                            versao = (string)subKey.GetValue("Version", "");
                            if (versao != "")
                            {
                                versoes.Add(versao);
                            }
                        }

                    }
                }
            }
            return versoes;
        }

        public static bool IsChave(this string _chave, string _modelo)
        {
            try
            {
                if (string.IsNullOrEmpty(_chave))
                {
                    return false;
                }

                if (_chave.Length != 44)
                {
                    return false;
                }

                var uf = int.Parse(_chave.Substring(0, 2));
                if (!((uf >= 11 && uf <= 17) || (uf >= 21 && uf <= 35) || (uf >= 41 && uf <= 43) || (uf >= 50 && uf <= 53)))
                {
                    return false;
                }
                var mes = int.Parse(_chave.Substring(4, 2));
                if (mes < 1 || mes > 12)
                {
                    return false;
                }
                var ano = int.Parse(_chave.Substring(2, 2));
                var cnpj = _chave.Substring(6, 14);
                if (!IsCNPJ(cnpj))
                {
                    return false;
                }
                var modelo = _chave.Substring(20, 2);
                if (!modelo.Equals(_modelo))
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static string SubString(this string text, int size)
        {
            if (string.IsNullOrEmpty(text) || text.Length <= size)
            {
                return text;
            }
            return text.Substring(0, size);
        }

        public static string SubStringInvertida(this string text, int size)
        {
            if (string.IsNullOrEmpty(text) || text.Length <= size)
            {
                return text;
            }
            var inicio = text.Length - size;
            return text.Substring(inicio);
        }




        




        public static void ToolTipButton(this Control button, string messagem, bool showTithe = true)
        {
            var buttonToolTip = new ToolTip();
            if (showTithe)
            {
                buttonToolTip.ToolTipTitle = "JAMSOFT Sistemas";
            }
            else
            {
                buttonToolTip.ToolTipTitle = "";
            }
            buttonToolTip.UseFading = true;
            buttonToolTip.UseAnimation = true;
            buttonToolTip.IsBalloon = true;

            buttonToolTip.ShowAlways = true;

            buttonToolTip.AutoPopDelay = 5000;
            buttonToolTip.InitialDelay = 0;
            buttonToolTip.ReshowDelay = 500;

            buttonToolTip.SetToolTip(button, messagem);
        }

        public static string RemoverPontos(this string messagem)
        {
            var rgx = new Regex("\\D");
            return rgx.Replace(messagem, "");
        }


        public static string EscreverExtenso(this decimal valor)
        {
            if (valor <= 0 | valor >= 1000000000000000)
                return "Valor não suportado pelo sistema.";
            else
            {
                string strValor = valor.ToString("000000000000000.00");
                string valor_por_extenso = string.Empty;
                for (int i = 0; i <= 15; i += 3)
                {
                    valor_por_extenso += Escrever_Valor_Extenso(Convert.ToDecimal(strValor.Substring(i, 3)));
                    if (i == 0 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(0, 3)) == 1)
                            valor_por_extenso += " TRILHÃO" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " E " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(0, 3)) > 1)
                            valor_por_extenso += " TRILHÕES" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " E " : string.Empty);
                    }
                    else if (i == 3 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(3, 3)) == 1)
                            valor_por_extenso += " BILHÃO" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " E " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(3, 3)) > 1)
                            valor_por_extenso += " BILHÕES" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " E " : string.Empty);
                    }
                    else if (i == 6 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(6, 3)) == 1)
                            valor_por_extenso += " MILHÃO" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " E " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(6, 3)) > 1)
                            valor_por_extenso += " MILHÕES" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " E " : string.Empty);
                    }
                    else if (i == 9 & valor_por_extenso != string.Empty)
                        if (Convert.ToInt32(strValor.Substring(9, 3)) > 0)
                            valor_por_extenso += " MIL" + ((Convert.ToDecimal(strValor.Substring(12, 3)) > 0) ? " E " : string.Empty);
                    if (i == 12)
                    {
                        if (valor_por_extenso.Length > 8)
                            if (valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "BILHÃO" | valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "MILHÃO")
                                valor_por_extenso += " DE";
                            else
                                if (valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "BILHÕES" | valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "MILHÕES"
| valor_por_extenso.Substring(valor_por_extenso.Length - 8, 7) == "TRILHÕES")
                                valor_por_extenso += " DE";
                            else
                                    if (valor_por_extenso.Substring(valor_por_extenso.Length - 8, 8) == "TRILHÕES")
                                valor_por_extenso += " DE";
                        if (Convert.ToInt64(strValor.Substring(0, 15)) == 1)
                            valor_por_extenso += " REAL";
                        else if (Convert.ToInt64(strValor.Substring(0, 15)) > 1)
                            valor_por_extenso += " REAIS";
                        if (Convert.ToInt32(strValor.Substring(16, 2)) > 0 && valor_por_extenso != string.Empty)
                            valor_por_extenso += " E ";
                    }
                    if (i == 15)
                        if (Convert.ToInt32(strValor.Substring(16, 2)) == 1)
                            valor_por_extenso += " CENTAVO";
                        else if (Convert.ToInt32(strValor.Substring(16, 2)) > 1)
                            valor_por_extenso += " CENTAVOS";
                }
                return valor_por_extenso;
            }
        }

        static string Escrever_Valor_Extenso(decimal valor)
        {
            if (valor <= 0)
                return string.Empty;
            else
            {
                string montagem = string.Empty;
                if (valor > 0 & valor < 1)
                {
                    valor *= 100;
                }
                string strValor = valor.ToString("000");
                int a = Convert.ToInt32(strValor.Substring(0, 1));
                int b = Convert.ToInt32(strValor.Substring(1, 1));
                int c = Convert.ToInt32(strValor.Substring(2, 1));
                if (a == 1)
                    montagem += (b + c == 0) ? "CEM" : "CENTO";
                else if (a == 2)
                    montagem += "DUZENTOS";
                else if (a == 3)
                    montagem += "TREZENTOS";
                else if (a == 4)
                    montagem += "QUATROCENTOS";
                else if (a == 5)
                    montagem += "QUINHENTOS";
                else if (a == 6)
                    montagem += "SEISCENTOS";
                else if (a == 7)
                    montagem += "SETECENTOS";
                else if (a == 8)
                    montagem += "OITOCENTOS";
                else if (a == 9)
                    montagem += "NOVECENTOS";
                if (b == 1)
                {
                    if (c == 0)
                        montagem += ((a > 0) ? " E " : string.Empty) + "DEZ";
                    else if (c == 1)
                        montagem += ((a > 0) ? " E " : string.Empty) + "ONZE";
                    else if (c == 2)
                        montagem += ((a > 0) ? " E " : string.Empty) + "DOZE";
                    else if (c == 3)
                        montagem += ((a > 0) ? " E " : string.Empty) + "TREZE";
                    else if (c == 4)
                        montagem += ((a > 0) ? " E " : string.Empty) + "QUATORZE";
                    else if (c == 5)
                        montagem += ((a > 0) ? " E " : string.Empty) + "QUINZE";
                    else if (c == 6)
                        montagem += ((a > 0) ? " E " : string.Empty) + "DEZESSEIS";
                    else if (c == 7)
                        montagem += ((a > 0) ? " E " : string.Empty) + "DEZESSETE";
                    else if (c == 8)
                        montagem += ((a > 0) ? " E " : string.Empty) + "DEZOITO";
                    else if (c == 9)
                        montagem += ((a > 0) ? " E " : string.Empty) + "DEZENOVE";
                }
                else if (b == 2)
                    montagem += ((a > 0) ? " E " : string.Empty) + "VINTE";
                else if (b == 3)
                    montagem += ((a > 0) ? " E " : string.Empty) + "TRINTA";
                else if (b == 4)
                    montagem += ((a > 0) ? " E " : string.Empty) + "QUARENTA";
                else if (b == 5)
                    montagem += ((a > 0) ? " E " : string.Empty) + "CINQUENTA";
                else if (b == 6)
                    montagem += ((a > 0) ? " E " : string.Empty) + "SESSENTA";
                else if (b == 7)
                    montagem += ((a > 0) ? " E " : string.Empty) + "SETENTA";
                else if (b == 8)
                    montagem += ((a > 0) ? " E " : string.Empty) + "OITENTA";
                else if (b == 9)
                    montagem += ((a > 0) ? " E " : string.Empty) + "NOVENTA";
                if (strValor.Substring(1, 1) != "1" & c != 0 & montagem != string.Empty)
                    montagem += " E ";
                if (strValor.Substring(1, 1) != "1")
                    if (c == 1)
                        montagem += "UM";
                    else if (c == 2)
                        montagem += "DOIS";
                    else if (c == 3)
                        montagem += "TRÊS";
                    else if (c == 4)
                        montagem += "QUATRO";
                    else if (c == 5)
                        montagem += "CINCO";
                    else if (c == 6)
                        montagem += "SEIS";
                    else if (c == 7)
                        montagem += "SETE";
                    else if (c == 8)
                        montagem += "OITO";
                    else if (c == 9)
                        montagem += "NOVE";
                return montagem;
            }
        }


        public static void ToolTipDataGridViewRowCollection(this DataGridView button, string messagem)
        {
            var buttonToolTip = new ToolTip
            {
                ToolTipTitle = "JAMSOFT Sistemas",
                UseFading = true,
                UseAnimation = true,
                IsBalloon = true,

                ShowAlways = true,

                AutoPopDelay = 1,
                InitialDelay = 0,
                ReshowDelay = 500
            };

            buttonToolTip.SetToolTip(button, messagem);
            button.Show();
        }

        public static void ToQuantidade(this TextBox _textBox)
        {
            if (string.IsNullOrEmpty(_textBox.Text))
            {
                _textBox.Text = "0,000";
            }
            _textBox.TextAlign = HorizontalAlignment.Right;
            _textBox.TextChanged += new EventHandler(TextQuant_TextChanged);
            _textBox.KeyPress += new KeyPressEventHandler(TextMoeda_TextChanged_KeyPress);
            _textBox.Enter += new EventHandler(TextMoeda_Enter);
        }

        private static void TextQuant_TextChanged(object sender, EventArgs e)
        {
            var txt = (TextBox)sender;
            Quantidade(ref txt);
        }



        public static void ToMonetario(this TextBox _textBox)
        {
            if (string.IsNullOrEmpty(_textBox.Text))
            {
                _textBox.Text = "0,00";
            }
            _textBox.TextAlign = HorizontalAlignment.Right;
            _textBox.TextChanged += new EventHandler(TextMoeda_TextChanged);
            _textBox.KeyPress += new KeyPressEventHandler(TextMoeda_TextChanged_KeyPress);
            _textBox.Enter += new EventHandler(TextMoeda_Enter);
            _textBox.SelectionStart = _textBox.Text.Length;
        }

        public static void ToInteger(this TextBox _textBox)
        {
            _textBox.KeyPress += new KeyPressEventHandler(TextMoeda_TextChanged_KeyPress);
        }

        private static void Moeda(ref TextBox txt)
        {
            var n = string.Empty;
            double v = 0;
            try
            {
                n = txt.Text.Replace(",", "").Replace(".", "");
                if (n.Equals(""))
                {
                    n = "";
                }

                n = n.PadLeft(3, '0');
                if (n.Length > 3 && n.Substring(0, 1) == "0")
                {
                    n = n.Substring(1, n.Length - 1);
                }

                v = Convert.ToDouble(n) / 100;
                txt.Text = string.Format("{0:N}", v);
                txt.SelectionStart = txt.Text.Length;
            }
            catch (Exception)
            {

            }
        }

        private static void Quantidade(ref TextBox txt)
        {
            var n = string.Empty;
            double v = 0;
            try
            {
                n = txt.Text.Replace(",", "").Replace(".", "");
                if (n.Equals(""))
                {
                    n = "";
                }

                n = n.PadLeft(4, '0');
                if (n.Length > 4 && n.Substring(0, 1) == "0")
                {
                    n = n.Substring(1, n.Length - 1);
                }

                v = Convert.ToDouble(n) / 1000;
                txt.Text = v.ToString("N3");//string.Format("{0:N}", v);
                txt.SelectionStart = txt.Text.Length;
            }
            catch (Exception)
            {

            }
        }



        private static void TextMoeda_TextChanged(object sender, EventArgs e)
        {
            var txt = (TextBox)sender;
            Moeda(ref txt);
        }


        private static void TextMoeda_TextChanged_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)8) && (e.KeyChar != (char)44) && (e.KeyChar != (char)48))
            {
                e.Handled = true;
            }
        }



        private static void TextMoeda_Enter(object sender, EventArgs e)
        {
            var txt = (TextBox)sender;
            txt.SelectionStart = txt.Text.Length;
        }


        public static Bitmap ResizeImage(Image image)
        {
            var largura = (double)400 / image.Width;
            var altura = (double)300 / image.Height;
            var result = Math.Min(largura, altura);
            var newLargura = (int)(image.Width * result);
            var newAltura = (int)(image.Height * result);
            var newImage = new Bitmap(newLargura, newAltura);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newLargura, newAltura);
            var bmp = new Bitmap(newImage);
            return bmp;
        }

        public static T ToEnum<T>(this string enumString)
        {
            return (T)Enum.Parse(typeof(T), enumString);
        }

        public static string RemoverTextString(this string _campo)
        {
            var re = new Regex("[0-9]");
            var s = new StringBuilder();
            try
            {
                foreach (Match m in re.Matches(_campo))
                {
                    s.Append(m.Value);
                }
                return s.ToString();
            }
            catch
            {
                return "";
            }
        }

        public static string CampoInformado(this string _campo)
        {
            return string.IsNullOrWhiteSpace(_campo) ? "Não informado" : _campo;
        }



        public partial class Metodos
        {

            [DllImportAttribute("user32.dll", EntryPoint = "BlockInput")]
            [return: MarshalAsAttribute(UnmanagedType.Bool)]
            public static extern bool BlockInput([MarshalAsAttribute(UnmanagedType.Bool)] bool fBlockIt);


            public static void BloquearTecladoMouse(bool bloquear)
            {
                try
                {
                    BlockInput(bloquear);
                }
                catch (Exception)
                { }
            }
        }

       
        public static string GerarCodigoNumerico()
        {
            var codigoNumerico = string.Empty;

            var listAux = new List<string>();
            listAux.AddRange(
                new string[]
                {
                        "00000000","11111111","22222222","33333333","44444444",
                        "55555555","66666666","77777777","88888888","99999999",
                        "12345678","23456789","34567890","45678901","56789012",
                        "67890123","78901234","89012345","90123456","01234567"
                });

            do
            {
                codigoNumerico = new Random().Next(10000000, 99999999).ToString("00000000");
            } while (listAux.Contains(codigoNumerico));

            return codigoNumerico;
        }

        public static string GetNumerico(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }
            return string.Join("", Regex.Split(value.Trim(), @"[^\d]"));
        }
        //NFe
       
        public static string GerarVerificador(string chave)
        {
            var soma = 0;
            var mod = -1;
            var dv = -1;
            var pesso = 2;


            for (var i = chave.Length - 1; i != -1; i--)
            {
                var ch = Convert.ToInt32(chave[i].ToString());
                soma += ch * pesso;

                if (pesso < 9)
                {
                    pesso += 1;
                }
                else
                {
                    pesso = 2;
                }
            }


            mod = soma % 11;

            if (mod == 0 || mod == 1)
            {
                dv = 0;
            }
            else
            {
                dv = 11 - mod;
            }

            return dv.ToString();
        }



        public static List<T> ConvertToList<T>(this DataTable dt)
        {

            var columnNames = dt.Columns.Cast<DataColumn>()
                    .Select(c => c.ColumnName.ToUpper())
                    .ToList();

            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    var result = columnNames.FirstOrDefault(a => a.Replace("_", "").Equals(pro.Name.ToUpper()));
                    if (result != null)
                    {
                        PropertyInfo pI = objT.GetType().GetProperty(pro.Name);
                        pro.SetValue(objT, row[result] == DBNull.Value ?
                            null :
                            Convert.ChangeType(row[result], pI.PropertyType));
                    }
                    //if (columnNames.Contains(pro.Name.ToUpper()))
                    //{

                    //}
                }

                return objT;
            }).ToList();
        }

        public static List<T> ConvertToList<T>(this SqlDataReader dr)
        {
            var dt = new DataTable();
            dt.Load(dr);
            return ConvertToList<T>(dt);
        }

    }
}
