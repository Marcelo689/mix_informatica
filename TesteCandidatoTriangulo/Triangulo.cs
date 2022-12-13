using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteCandidatoTriangulo
{
    public class RetornoArray
    {
        public string entrada { get; set; }
        public int[] arrays { get; set; }
    }
    public class Triangulo
    {
        /// <summary>
        ///    6
        ///   3 5
        ///  9 7 1
        /// 4 6 8 4
        /// Um elemento somente pode ser somado com um dos dois elementos da próxima linha. Como o elemento 5 na Linha 2 pode ser somado com 7 e 1, mas não com o 9.
        /// Neste triangulo o total máximo é 6 + 5 + 7 + 8 = 26
        /// 
        /// Seu código deverá receber uma matriz (multidimensional) como entrada. O triângulo acima seria: [[6],[3,5],[9,7,1],[4,6,8,4]]
        /// </summary>
        /// <param name="dadosTriangulo"></param>
        /// <returns>Retorna o resultado do calculo conforme regra acima</returns>
        public int ResultadoTriangulo(string dadosTriangulo)
        {

            List<List<int>> arrays = new List<List<int>>();
            dadosTriangulo = RetornaArraysDeDentro(dadosTriangulo);
            var retorno = RetornarProximoArray(dadosTriangulo);
            while (retorno.entrada != "")
            {
                arrays.Add(retorno.arrays.ToList());
                retorno = RetornarProximoArray(retorno.entrada);
                if(retorno.entrada == "")
                    arrays.Add(retorno.arrays.ToList());
            }
            var indice = 0;
            List<int> maiores = new List<int>();
            foreach (var item in arrays)
            {
                var maior = item.Min();
                var subIndice = 0;
                foreach ( var subArray in item)
                {
                    if(maior < subArray && (subIndice == indice || subIndice == indice + 1))
                    {
                         maior = subArray;
                         indice = subIndice;
                    }
                    subIndice++;
                }

                maiores.Add(maior);
            }

            var total = maiores.Sum(e => e);
            return total;
        }

        public string RetornaArraysDeDentro(string entrada)
        {
            entrada = entrada.Substring(1, entrada.Length-2);

            return entrada;
        }

        public string RemoveArrayJaAdicionado(string completa)
        {
            int ladoEsquerdoColchetes = completa.IndexOf("[");
            int ladoDireitoColchetes = completa.IndexOf("]");
            string arrayEmString = "";
            for (var i = 0; i < completa.Length; i++)
            {
                if (i == ladoDireitoColchetes)
                    break;
                if (i < ladoDireitoColchetes && i > ladoEsquerdoColchetes)
                {
                    arrayEmString += completa[i].ToString();
                }

            }

            completa = completa.Replace("[" + arrayEmString + "],", "");
            return completa.Replace("[" + arrayEmString + "]", "");
        }
        public RetornoArray RetornarProximoArray(string entrada)
        {
            int ladoEsquerdoColchetes = entrada.IndexOf("[");
            int ladoDireitoColchetes = entrada.IndexOf("]");
            int contador = 0;
            string arrayEmString = "";
            if (ladoEsquerdoColchetes == -1 || -1 == ladoDireitoColchetes)
                return null;
            for(var i =0; i< entrada.Length; i++)
            {
                if(contador == ladoDireitoColchetes)
                    break;
                if (contador < ladoDireitoColchetes && contador > ladoEsquerdoColchetes)
                {
                    arrayEmString += entrada[i].ToString();
                }

                contador++;
            }

            var numeros = arrayEmString.Split(',');

            List<int> result = new List<int>();
            contador = 0;
            foreach (var item in numeros)
            {
                result.Add( int.Parse(item));
            }

            return new RetornoArray() {
                entrada = RemoveArrayJaAdicionado(entrada),
                arrays =result.ToArray()
            };
        }
    }
}
