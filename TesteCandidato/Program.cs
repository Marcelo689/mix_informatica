using TesteCandidato;

namespace TesteCandidato
{
    //class CepDados
    //{
    //    public string CEP { get; set; }
    //    public string Logradouro { get; set; }
    //    public string Complemento { get; set; }
    //    public string Bairro { get; set; }
    //    public string Localidade { get; set; }
    //    public string UF { get; set; }
    //    public string Unidade { get; set; }
    //    public string IBGE { get; set; }
    //    public string GIA { get; set; }

    //    internal void ImprimirDados()
    //    {
    //        Console.WriteLine($@"
    //            CEP = {CEP}
    //            Logradouro = {Logradouro}
    //            Complemento = {Complemento}
    //            Bairro = {Bairro}
    //            Localidade = {Localidade}
    //            UF = {UF}
    //            Unidade = {Unidade}
    //            IBGE = {IBGE}
    //            GIA = {GIA}
    //        ");
    //    }
    //    public void DigitaCEPCasoNaoExistaCadastra(string cep)
    //    {
    //        if(cep == string.Empty)
    //            cep = "93180000";
    //        do
    //        {
    //            cep = Console.ReadLine();
    //            if (cep.Length > 8)
    //            {
    //                Console.WriteLine("CEP Inválido");
    //            }
    //        } while (cep.Length != 8);

    //        //TODO: Implementar forma de fazer o usuário poder errar várias vezes o CEP informado
    //        //TODO: Melhorar validação do CEP.

    //        //Exemplo CEP 13050020
    //        string viaCEPUrl = "http://viacep.com.br/ws/" + cep + "/json/";

    //        //TODO: Resolver dados com caracter especial no retorno do JSON 

    //        WebClient client = new WebClient();
    //        var result = client.DownloadString(viaCEPUrl);

    //        //TODO: Tratar CEP Inválido.

    //        JObject jsonRetorno = JsonConvert.DeserializeObject<JObject>(result);

    //        var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CEP;server=.;trusted_connection=true;");
    //        var existe = false;
    //        var sqlCommand = new SqlCommand(@"Select * from CEP 
    //            where 
    //            cep = [cep] and
    //            logradouro = [logradouro] and
    //            complemento = [complemento] and
    //            bairro = [bairro] and
    //            localidade = [localidade] and
    //            uf = [uf] and
    //            unidade = [unidade] and
    //            ibge = [ibge] and
    //            gia = [gia]
    //        ", connection);
    //        var Cepdados = new CepDados();
    //        try
    //        {
    //            connection.Open();

    //            var retorno = sqlCommand.ExecuteReader();
    //            if (retorno.Read())
    //            {
    //                Cepdados.CEP = retorno["cep"].ToString();
    //                Cepdados.Logradouro = retorno["logradouro"].ToString();
    //                Cepdados.Complemento = retorno["complemento"].ToString();
    //                Cepdados.Bairro = retorno["bairro"].ToString();
    //                Cepdados.Localidade = retorno["localidade"].ToString();
    //                Cepdados.UF = retorno["uf"].ToString();
    //                Cepdados.Unidade = retorno["unidade"].ToString();
    //                Cepdados.IBGE = retorno["ibge"].ToString();
    //                Cepdados.GIA = retorno["gia"].ToString();
    //            }
    //            existe = true;
    //        }
    //        catch (Exception)
    //        {
    //        }
    //        finally
    //        {
    //            connection.Close();
    //        }

    //        if (!existe)
    //        {

    //            //TODO: Validar CEP existente
    //            string query = "INSERT INTO [dbo].[CEP] ([cep], [logradouro], [complemento], [bairro], [localidade], [uf], [unidade], [ibge], [gia]) VALUES (";
    //            query = query + "'" + jsonRetorno["cep"] + "'";
    //            query = query + ",'" + jsonRetorno["logradouro"] + "'";
    //            query = query + ",'" + jsonRetorno["complemento"] + "'";
    //            query = query + ",'" + jsonRetorno["bairro"] + "'";
    //            query = query + ",'" + jsonRetorno["localidade"] + "'";
    //            query = query + ",'" + jsonRetorno["uf"] + "'";
    //            query = query + ",'" + jsonRetorno["unidade"] + "'";
    //            query = query + ",'" + jsonRetorno["ibge"] + "'";
    //            query = query + ",'" + jsonRetorno["gia"] + "'" + ")";

    //            //SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CEP;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");

    //            sqlCommand = new SqlCommand(query, connection);

    //            sqlCommand.CommandType = CommandType.Text;

    //            try
    //            {
    //                connection.Open();

    //                sqlCommand.ExecuteNonQuery();
    //            }
    //            catch (Exception)
    //            {
    //            }
    //            finally
    //            {
    //                connection.Close();
    //            }

    //        }
    //        else
    //        {
    //            Cepdados.ImprimirDados();
    //        }

    //        sqlCommand.Dispose();
    //        connection.Close();
    //        connection.Dispose();
    //    }
    //    internal void ImprimeCepsPorUF()
    //    {
    //        Console.WriteLine("Deseja visualizar todos os CEPs alguma UF? Se sim, informar UF, se não, informar sair.");
    //        string resposta = Console.ReadLine();

    //        if (resposta == "sair")
    //        {
    //            return;
    //        }

    //        if (resposta.Length > 2)
    //        {
    //            Console.WriteLine("UF inválida");
    //            resposta = Console.ReadLine();
    //        }

    //        if (resposta == "sair")
    //        {
    //            return;
    //        }


    //        var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CEP;server=.;trusted_connection=true;");
    //        var sqlCommand = new SqlCommand($"Select * from CEP where uf = '{resposta}'", connection);
    //        SqlDataAdapter adapter = new SqlDataAdapter();

    //        DataSet ds = new DataSet();
    //        DataView dv;
    //        sqlCommand.CommandType = CommandType.Text;

    //        try
    //        {
    //            connection.Open();
    //            adapter.SelectCommand = sqlCommand;
    //            adapter.Fill(ds, "Create DataView");
    //            adapter.Dispose();

    //            dv = ds.Tables[0].DefaultView;

    //            for (int i = 0; i < dv.Count; i++)
    //            {
    //                if (dv[i]["uf"].ToString() == resposta)
    //                {
    //                    if (i == 0)
    //                    {
    //                        Console.Write(dv[i]["cep"]);
    //                    }
    //                    else
    //                    {
    //                        Console.Write(";" + dv[i]["cep"]);
    //                    }
    //                }
    //            }
    //        }
    //        catch (Exception)
    //        {
    //        }
    //        finally
    //        {
    //            connection.Close();
    //        }

    //        sqlCommand.Dispose();
    //        connection.Close();
    //        connection.Dispose();

    //        Console.ReadLine();
    //    }
    //}

}
class Program
{
    static void Main(string[] args)
    {
        /*
        Bem vindo ao teste de Back-end da e.Mix!

        Abaixo está desenvolvido de uma forma bem simples e com alguns erros uma consulta de CEP.

        O que esperamos de você neste teste é que faça um novo projeto WEB da forma mais correta, segura e performática na sua avaliação com base no código abaixo.

        Entre os códigos você pode notar que existem observações "To Do" que também devem ser realizadas para que o teste esteja correto.
        Exemplo: "TODO: Criar banco de dados - LocalDB com o nome CEP"

        Observação: Você poderá utilizar qualquer tecnologia ou framework da sua preferência.

        */

        //TODO: Fazer um projeto WEB

        //TODO: Perguntar se o usuário quer consultar se logradouro existe na base

        //TODO: Criar banco de dados - LocalDB com o nome CEP
        //TODO: Adicionar tabela conforme script abaixo
        //USE[CEP]
        //GO

        //SET ANSI_NULLS ON
        //GO

        //SET QUOTED_IDENTIFIER ON
        //GO

        //CREATE TABLE[dbo].[CEP](
        //    [Id]          INT IDENTITY(1, 1) NOT NULL,
        //    [cep]         CHAR(9)       NULL,
        //    [logradouro] NVARCHAR(500) NULL,
        //    [complemento] NVARCHAR(500) NULL,
        //    [bairro] NVARCHAR(500) NULL,
        //    [localidade] NVARCHAR(500) NULL,
        //    [uf] CHAR(2)       NULL,
        //    [unidade] BIGINT NULL,
        //    [ibge]        INT NULL,
        //    [gia]         NVARCHAR(500) NULL
        //);
        var buscaCep = new ManipulaDadosCep();
        buscaCep.DigitaCEPCasoNaoExistaCadastra("93180000");
        buscaCep.ImprimeCepsPorUF();
        //TODO: Retornar os dados do CEP infomado no início para o usuário




    }
}
