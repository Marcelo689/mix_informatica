using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace TesteMVCCEP
{
    public class CepDados
    {
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }
        public string Unidade { get; set; }
        public string IBGE { get; set; }
        public string GIA { get; set; }

        internal void ImprimirDados()
        {
            throw new NotImplementedException();
        }
    }
        public class ManipulaDadosCep
    {
        
            public string CEP { get; set; }
            public string Logradouro { get; set; }
            public string Complemento { get; set; }
            public string Bairro { get; set; }
            public string Localidade { get; set; }
            public string UF { get; set; }
            public string Unidade { get; set; }
            public string IBGE { get; set; }
            public string GIA { get; set; }

            public void ImprimirDados()
            {
                Console.WriteLine($@"
                CEP = {CEP}
                Logradouro = {Logradouro}
                Complemento = {Complemento}
                Bairro = {Bairro}
                Localidade = {Localidade}
                UF = {UF}
                Unidade = {Unidade}
                IBGE = {IBGE}
                GIA = {GIA}
            ");
            }

        public List<CepDados> BuscaPorLogradouro(string logradouro)
        {
            var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CEP;server=.;trusted_connection=true;");
            var sqlCommand = new SqlCommand($@"Select * from CEP 
                where 
                logradouro like '%{logradouro}%'
            ", connection);
            var lista = new List<CepDados>();
            try
            {
                connection.Open();

                var retorno = sqlCommand.ExecuteReader();
                while (retorno.Read())
                {
                    var dados = new CepDados();
                    dados.CEP = retorno["cep"].ToString();
                    dados.Logradouro = retorno["logradouro"].ToString();
                    dados.Complemento = retorno["complemento"].ToString();
                    dados.Bairro = retorno["bairro"].ToString();
                    dados.Localidade = retorno["localidade"].ToString();
                    dados.UF = retorno["uf"].ToString();
                    dados.Unidade = retorno["unidade"].ToString();
                    dados.IBGE = retorno["ibge"].ToString();
                    dados.GIA = retorno["gia"].ToString();
                    lista.Add(dados);
                }

            }
            catch (Exception)
            {
            }
            finally
            {
                connection.Close();
            }

            return lista;
        }

        public List<CepDados> DigitaCEPCasoNaoExistaCadastra(string cep)
            {
                var lista = new List<CepDados>();
                if (cep == string.Empty)
                    return lista;
                //do
                //{
                //    cep = Console.ReadLine();
                //    if (cep.Length > 8)
                //    {
                //        Console.WriteLine("CEP Inválido");
                //    }
                //} while (cep.Length != 8);

                //TODO: Implementar forma de fazer o usuário poder errar várias vezes o CEP informado
                //TODO: Melhorar validação do CEP.

                //Exemplo CEP 13050020
                string viaCEPUrl = "http://viacep.com.br/ws/" + cep + "/json/";

                //TODO: Resolver dados com caracter especial no retorno do JSON 

                WebClient client = new WebClient();
                var result = client.DownloadString(viaCEPUrl);

                //TODO: Tratar CEP Inválido.

                JObject jsonRetorno = JsonConvert.DeserializeObject<JObject>(result);

                var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CEP;server=.;trusted_connection=true;");
                var existe = false;
                var sqlCommand = new SqlCommand($@"Select * from CEP 
                where 
                cep = '{jsonRetorno["cep"]}' and
                logradouro = '{jsonRetorno["logradouro"]}' and
                complemento = '{jsonRetorno["complemento"]}' and
                bairro = '{jsonRetorno["bairro"]}' and
                localidade = '{jsonRetorno["localidade"]}' and
                uf = '{jsonRetorno["uf"]}' and
                unidade = '{jsonRetorno["unidade"]}' and
                ibge = '{jsonRetorno["ibge"]}' and
                gia = '{jsonRetorno["gia"]}'
            ", connection);
                var Cepdados = new CepDados();
                try
                {
                    connection.Open();

                    var retorno = sqlCommand.ExecuteReader();
                    if (retorno.Read())
                    {
                        Cepdados.CEP = retorno["cep"].ToString() ?? "";
                        Cepdados.Logradouro = jsonRetorno["logradouro"].ToString() ?? "";
                        Cepdados.Complemento = jsonRetorno["complemento"].ToString() ?? "";
                        Cepdados.Bairro = jsonRetorno["bairro"].ToString() ?? "";
                        Cepdados.Localidade = jsonRetorno["localidade"].ToString() ?? "";
                        Cepdados.UF = jsonRetorno["uf"].ToString() ?? "";
                        Cepdados.Unidade = jsonRetorno["unidade"] == null ? "" : jsonRetorno["unidade"].ToString();
                        Cepdados.IBGE = jsonRetorno["ibge"].ToString() ?? "";
                        Cepdados.GIA = jsonRetorno["gia"].ToString() ?? "";
                        existe = true;
                    }else
                    {
                        existe = false;
                    }
                        lista.Add(Cepdados);
                //return lista;
                }
                catch (Exception)
                {
                }
                finally
                {
                    connection.Close();
                }

                if (!existe)
                {

                    //TODO: Validar CEP existente
                    string query = "INSERT INTO [dbo].[CEP] ([cep], [logradouro], [complemento], [bairro], [localidade], [uf], [unidade], [ibge], [gia]) VALUES (";
                    query = query + "'" + jsonRetorno["cep"] + "'";
                    query = query + ",'" + jsonRetorno["logradouro"] + "'";
                    query = query + ",'" + jsonRetorno["complemento"] + "'";
                    query = query + ",'" + jsonRetorno["bairro"] + "'";
                    query = query + ",'" + jsonRetorno["localidade"] + "'";
                    query = query + ",'" + jsonRetorno["uf"] + "'";
                    query = query + ",'" + jsonRetorno["unidade"] + "'";
                    query = query + ",'" + jsonRetorno["ibge"] + "'";
                    query = query + ",'" + jsonRetorno["gia"] + "'" + ")";

                //SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CEP;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
                    Cepdados = new CepDados();
                    Cepdados.CEP = jsonRetorno["cep"].ToString() ?? "";
                    Cepdados.Logradouro = jsonRetorno["logradouro"].ToString() ?? "";
                    Cepdados.Complemento = jsonRetorno["complemento"].ToString() ?? "";
                    Cepdados.Bairro = jsonRetorno["bairro"].ToString() ?? "";
                    Cepdados.Localidade = jsonRetorno["localidade"].ToString() ?? "";
                    Cepdados.UF = jsonRetorno["uf"].ToString() ?? "";
                    Cepdados.Unidade = jsonRetorno["unidade"] == null ? "" : jsonRetorno["unidade"].ToString();
                    Cepdados.IBGE = jsonRetorno["ibge"].ToString() ?? "";
                    Cepdados.GIA = jsonRetorno["gia"].ToString() ?? "";
                    
                    sqlCommand = new SqlCommand(query, connection);

                    sqlCommand.CommandType = CommandType.Text;

                    try
                    {
                        connection.Open();

                        sqlCommand.ExecuteNonQuery();
                        lista.Add(Cepdados);
                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                        connection.Close();
                    }

                }

                sqlCommand.Dispose();
                connection.Close();
                connection.Dispose();

            return lista;
            }
            public void ImprimeCepsPorUF()
            {
                Console.WriteLine("Deseja visualizar todos os CEPs alguma UF? Se sim, informar UF, se não, informar sair.");
                string resposta = Console.ReadLine();

                if (resposta == "sair")
                {
                    return;
                }

                if (resposta.Length > 2)
                {
                    Console.WriteLine("UF inválida");
                    resposta = Console.ReadLine();
                }

                if (resposta == "sair")
                {
                    return;
                }


                var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CEP;server=.;trusted_connection=true;");
                var sqlCommand = new SqlCommand($"Select * from CEP where uf = '{resposta}'", connection);
                SqlDataAdapter adapter = new SqlDataAdapter();

                DataSet ds = new DataSet();
                DataView dv;
                sqlCommand.CommandType = CommandType.Text;

                try
                {
                    connection.Open();
                    adapter.SelectCommand = sqlCommand;
                    adapter.Fill(ds, "Create DataView");
                    adapter.Dispose();

                    dv = ds.Tables[0].DefaultView;

                    for (int i = 0; i < dv.Count; i++)
                    {
                        if (dv[i]["uf"].ToString() == resposta)
                        {
                            if (i == 0)
                            {
                                Console.Write(dv[i]["cep"]);
                            }
                            else
                            {
                                Console.Write(";" + dv[i]["cep"]);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                    connection.Close();
                }

                sqlCommand.Dispose();
                connection.Close();
                connection.Dispose();

                Console.ReadLine();
            }
        

    }
}
