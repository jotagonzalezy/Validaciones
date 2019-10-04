using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace verificacion
{
    public class ValidacionClass
    {
        public class InfoTarjeta
        {
            public string Cuenta { get; set; }
            public string Dv { get; set; }
            public string Tipo { get; set; }
            public string Industria { get; set; }
            public string Banco { get; set; }
        }

        public bool ValidaRut(string rut, string dv)
        {
            var largo = rut.Length;
            if (largo <= 0)
                throw new Exception("Error largo");

            var sum = 0;
            var mul = 2;

            for (var i = largo - 1; i >= 0; i--)
            {
                var digit = short.Parse(rut.Substring(i, 1));
                sum = sum + digit * mul;
                mul = mul == 7 ? 2 : mul + 1; //multiplicador 32765432
            }

            var dvr = 11 - sum % 11;
            var strdv = dvr == 10 ? "K" : dvr.ToString();

            return strdv == dv.ToUpper();
        }

        public bool ValidaSscc(string sscc)
        {
            var isValid = false;
            var largo = sscc.Length;
            if (largo < 18)
                throw new Exception("Error largo");

            var suma = 0;
            for (var i = largo - 2; i >= 0; i--)
            {
                var digit = short.Parse(sscc.Substring(i, 1));
                var mul = i % 2 == 0 ? 3 : 1; //multiplicador.Pares x1 impares x3
                suma = suma + digit * mul;
            }

            var dvr = 10 - suma % 10;
            dvr = dvr == 10 ? 0 : dvr;

            if (sscc.Substring(largo - 1) == dvr.ToString())
                isValid = true;

            return isValid;
        }

        public bool ValidaGtin13(string gtin)
        {
            var isValid = false;
            var largo = gtin.Length;
            if (largo < 13)
                throw new Exception("Error largo");

            var suma = 0;
            for (var i = largo - 2; i >= 0; i--)
            {
                var digit = short.Parse(gtin.Substring(i, 1));
                var mul = i % 2 == 0 ? 1 : 3; //multiplicador.Pares x1 impares x3
                suma = suma + digit * mul;
            }

            var dvr = 10 - suma % 10;
            dvr = dvr == 10 ? 0 : dvr;

            if (gtin.Substring(largo - 1) == dvr.ToString())
                isValid = true;

            return isValid;
        }

        //El algoritmo de Lenin o fórmula de Luhn, también conocida como "algoritmo de módulo 10",
        //es una fórmula de suma de verificación,
        //utilizada para validar una diversidad de números de identificación; 
        //como números de tarjetas de crédito, números IMEI, etc.
        public bool Validar10(string creditCardNumber)
        {
            var largo = creditCardNumber.Length;
            if (largo > 18 || largo < 15)
                throw new Exception("Error largo");

            var sum = 0;
            var timesTwo = false;

            for (var i = largo - 1; i >= 0; i--)
            {
                int addend;
                var digit = short.Parse(creditCardNumber.Substring(i, 1));

                if (timesTwo)
                {
                    addend = digit * 2;
                    if (addend > 9)
                        addend -= 9;
                }
                else
                {
                    addend = digit;
                }
                sum += addend;
                timesTwo = !timesTwo;
            }
            return sum % 10 == 0;//divisible por 10
        }

        //Card length:

        //Visa and Visa Electron: 13 or 16
        //Mastercard: 16
        //Discover: 16
        //American Express: 15
        //Diner's Club: 14 (including enRoute, International, Blanche)
        //Maestro: 12 to 19 (multi-national Debit Card)
        //Laser: 16 to 19 (Ireland Debit Card)
        //Switch: 16, 18 or 19 (United Kingdom Debit Card)
        //Solo: 16, 18 or 19 (United Kingdom Debit Card)
        //JCB: 15 or 16 (Japan Credit Bureau)
        //China UnionPay: 16 (People's Republic of China)

        public InfoTarjeta ExtraeData(string creditCardNumber)
        {
            var largo = creditCardNumber.Length;
            if (largo > 18 || largo < 15)
                throw new Exception("Error largo");

            var digit = creditCardNumber.Substring(largo - 1, 1);
            var cuenta = creditCardNumber.Substring(largo - 10, 9);
            var tipoId = creditCardNumber.Substring(0, 1);

            var binNumber = creditCardNumber.Substring(0, 6); //BANK IDENTIFICATION NUMBER

            var info = new InfoTarjeta
            {
                Dv = digit,
                Cuenta = cuenta,
                Banco = "DESCONOCIDO",
            };

            if (tipoId == "1" || tipoId == "2")
            {
                info.Industria = "AEROLINEA";
            }

            if (tipoId == "3")
            {
                info.Industria = "VIAJE Y ENTRETENIMIENTO";
                if (Convert.ToInt32(binNumber) >= 340000 && Convert.ToInt32(binNumber) <= 349999)
                    info.Tipo = "AMERICAN EXPRESS";

                if (Convert.ToInt32(binNumber) >= 370000 && Convert.ToInt32(binNumber) <= 379999)
                    info.Tipo = "AMERICAN EXPRESS";
            }

            if (tipoId == "4")
            {
                info.Industria = "BANCO Y FINANCIERA";
                info.Tipo = "VISA";

                if (Convert.ToInt32(binNumber) >= 456400 && Convert.ToInt32(binNumber) <= 456499)
                    info.Banco = "BancoEstado - CHILE";

                if (499847 == Convert.ToInt32(binNumber))
                    info.Banco = "Banco Falabella - CHILE";

                if (409767 == Convert.ToInt32(binNumber))
                    info.Banco = "Banco Falabella - CHILE";

                if (425900 == Convert.ToInt32(binNumber))
                    info.Banco = "Banco Falabella - CHILE";

                if (Convert.ToInt32(binNumber) >= 425900 && Convert.ToInt32(binNumber) <= 4259999)
                    info.Banco = "Banco BICE - CHILE";



            }

            if (tipoId == "5")
            {
                info.Industria = "BANCO Y FINANCIERA";
                if (Convert.ToInt32(binNumber) >= 510000 && Convert.ToInt32(binNumber) <= 559999)
                {
                    info.Tipo = "MASTERCARD";
                    if (559202 == Convert.ToInt32(binNumber))
                        info.Banco = "Banco CENCOSUD - CHILE";

                    if (541678 == Convert.ToInt32(binNumber))
                        info.Banco = "BancoEstado - CHILE";

                    if (548742 == Convert.ToInt32(binNumber))
                        info.Banco = "Banco Falabella - CHILE";


                }
            }

            if (tipoId == "6")
            {
                if (Convert.ToInt32(binNumber) >= 601100 && Convert.ToInt32(binNumber) <= 601199)
                    info.Tipo = "DISCOVER";

                if (Convert.ToInt32(binNumber) >= 64400 && Convert.ToInt32(binNumber) <= 644999)
                    info.Tipo = "DISCOVER";

                if (Convert.ToInt32(binNumber) >= 650000 && Convert.ToInt32(binNumber) <= 654999)
                    info.Tipo = "DISCOVER";

                info.Industria = "COMERCIALIZACION Y BANCO";
            }

            if (tipoId == "7")
            {
                info.Industria = "PETROLERA";
            }

            if (tipoId == "8")
            {
                info.Industria = "TELECOMUNICACIONES";
            }


            if (tipoId == "9")
            {
                info.Industria = "NACIONAL";
            }




            return info;
        }

        public bool ValidaGtin14(string gtin)
        {
            var isValid = false;
            var largo = gtin.Length;
            if (largo < 14)
                throw new Exception("Error largo");

            var suma = 0;
            for (var i = largo - 2; i >= 0; i--)
            {
                var digit = short.Parse(gtin.Substring(i, 1));
                var mul = i % 2 == 0 ? 3 : 1; //multiplicador.Pares x1 impares x3
                suma = suma + digit * mul;
            }

            var dvr = 10 - suma % 10;
            dvr = dvr == 10 ? 0 : dvr;

            if (gtin.Substring(largo - 1) == dvr.ToString())
                isValid = true;

            return isValid;
        }

        //La identificación de contenedores se efectúa mediante una combinación alfanumérica de 11 dígitos.4​
        //Las primeras tres letras identifican al propietario y son asignadas a las compañías por el Bureau International des Containers et du Transport Intermodal(BIC).
        //La cuarta letra toma los siguientes valores:
        // U para identificar a los contenedores propiamente dichos.
        // J para el equipo auxiliar adosable.
        // Z para chasis o tráilers de transporte vial.
        // Luego siguen 6 dígitos numéricos y por último un dígito verificador para asegurar la correcta relación con los 10 anteriores.
        //http://www.shippingline.org/container-numbers/e/
        public bool ValidaContenedor(string contenedor)
        {
            var largo = contenedor.Length;
            if (largo < 12)
                throw new Exception("Error largo");

            double suma = 0;
            int i;
            var valnum = 0;

            for (i = largo - 3; i >= 0; i--)
            {
                switch (contenedor.Substring(i, 1))
                {
                    case "0":
                        valnum = 0;
                        break;
                    case "1":
                        valnum = 1;
                        break;
                    case "2":
                        valnum = 2;
                        break;
                    case "3":
                        valnum = 3;
                        break;
                    case "4":
                        valnum = 4;
                        break;
                    case "5":
                        valnum = 5;
                        break;
                    case "6":
                        valnum = 6;
                        break;
                    case "7":
                        valnum = 7;
                        break;
                    case "8":
                        valnum = 8;
                        break;
                    case "9":
                        valnum = 9;
                        break;
                    case "A":
                        valnum = 10;
                        break;
                    case "B":
                        valnum = 12;
                        break;
                    case "C":
                        valnum = 13;
                        break;
                    case "D":
                        valnum = 14;
                        break;
                    case "E":
                        valnum = 15;
                        break;
                    case "F":
                        valnum = 16;
                        break;
                    case "G":
                        valnum = 17;
                        break;
                    case "H":
                        valnum = 18;
                        break;
                    case "I":
                        valnum = 19;
                        break;
                    case "J":
                        valnum = 20;
                        break;
                    case "K":
                        valnum = 21;
                        break;
                    case "L":
                        valnum = 23;
                        break;
                    case "M":
                        valnum = 24;
                        break;
                    case "N":
                        valnum = 25;
                        break;
                    case "O":
                        valnum = 26;
                        break;
                    case "P":
                        valnum = 27;
                        break;
                    case "Q":
                        valnum = 28;
                        break;
                    case "R":
                        valnum = 29;
                        break;
                    case "S":
                        valnum = 30;
                        break;
                    case "T":
                        valnum = 31;
                        break;
                    case "U":
                        valnum = 32;
                        break;
                    case "V":
                        valnum = 34;
                        break;
                    case "W":
                        valnum = 35;
                        break;
                    case "X":
                        valnum = 36;
                        break;
                    case "Y":
                        valnum = 37;
                        break;
                    case "Z":
                        valnum = 38;
                        break;
                }
                suma = suma + valnum * Math.Pow(2, i);
            }
            var dvr = (suma % 11).ToString(CultureInfo.InvariantCulture);
            if (dvr == "10")
                dvr = "0";

            var isValid = contenedor.Substring(largo - 2) == "-" && contenedor.Substring(largo - 1).ToUpper() == dvr;
            return isValid;

        }
    }

}
