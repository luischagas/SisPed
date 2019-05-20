using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CadastrarPessoas : System.Web.UI.Page
{
    ServicoDePessoasFisicas servicoDePessoas = new ServicoDePessoasFisicas();
    PessoaFisica pessoa = new PessoaFisica();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ListarPessoas();
        }

    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    public void ListarPessoas()
    {
        rptPessoasFisicas.DataSource = servicoDePessoas.ListaPessoas();
        rptPessoasFisicas.DataBind();
    }




    protected void btnCadastrarPessoaFisica_Click(object sender, EventArgs e)
    {
        if (txtNomeCadastro.Value.Length > 0 && txtEnderecoCadastro.Value.Length > 0 && txtCPFCadastro.Value.Length > 0 && txtDataNascimentoCadastro.Value.Length > 0 && rbtSexoCadastro.Value.Length > 0)
        {
            pessoa.Nome = txtNomeCadastro.Value;
            pessoa.Endereco = txtEnderecoCadastro.Value;
            pessoa.CPF = txtCPFCadastro.Value;
            pessoa.DataNascimento = Convert.ToDateTime(txtDataNascimentoCadastro.Value);
            pessoa.Sexo = rbtSexoCadastro.Value;
            servicoDePessoas.CadastraPessoa(pessoa);

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AvisoPessoaFisica", "openModalWarning();", true);
        }

        ListarPessoas();
    }


    protected void btnAlterarPessoaFisica_Click(object sender, EventArgs e)
    {
        if (txtNomeAlteracao.Value.Length > 0 && txtEnderecoAlteracao.Value.Length > 0 && txtCPFAlteracao.Value.Length > 0 && txtDtNascimentoAlteracao.Value.Length > 0 && rbtSexoAlteracao.Value.Length > 0)
        {
            pessoa.Codigo = Convert.ToInt32(hfID.Value);
            pessoa.Nome = txtNomeAlteracao.Value;
            pessoa.Endereco = txtEnderecoAlteracao.Value;
            pessoa.CPF = txtCPFAlteracao.Value;
            pessoa.DataNascimento = Convert.ToDateTime(txtDtNascimentoAlteracao.Value);
            pessoa.Sexo = rbtSexoAlteracao.Value;
            servicoDePessoas.AlteraPessoa(pessoa);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AvisoPessoaFisica", "openModalWarning();", true);
        }

        ListarPessoas();
    }

    protected void btnAlterar_Click(object sender, CommandEventArgs e)
    {
        var id = Convert.ToInt32(e.CommandArgument);

        pessoa = servicoDePessoas.ObtemPessoa(id);

        if (pessoa != null)
        {
            hfID.Value = pessoa.Codigo.ToString();
            txtNomeAlteracao.Value = pessoa.Nome;
            txtEnderecoAlteracao.Value = pessoa.Endereco;
            txtCPFAlteracao.Value = pessoa.CPF;
            txtDtNascimentoAlteracao.Value = pessoa.DataNascimento.ToString("dd/MM/yyyy");
            rbtSexoAlteracao.Value = pessoa.Sexo;
        }


        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditarPessoaFisica", "openModalUpdate();", true);
    }

    protected void btnDeletarPessoaFisica_Click(object sender, EventArgs e)
    {
        lblErro.Text = "";

        if (!ObtemPedidoPessoa(Convert.ToInt32(hfID.Value)))
        {
            pessoa.Codigo = Convert.ToInt32(hfID.Value);

            servicoDePessoas.RemovePessoa(pessoa);

            ListarPessoas();
        } else
        {
            lblErro.Visible = true;
            lblErro.Text = "A PESSOA POSSUI UM PEDIDO VINCULADO A ELA!";
        }

      
    }

    protected void btnDeletar_Click(object sender, CommandEventArgs e)
    {
        hfID.Value = e.CommandArgument.ToString();

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "DeletarPessoaFisica", "openModalDelete();", true);
    }


    protected void btnCadastrar_Click(object sender, CommandEventArgs e)
    {
        txtNomeCadastro.Value = "";
        txtEnderecoCadastro.Value = "";
        txtCPFCadastro.Value = "";
        txtDataNascimentoCadastro.Value = "";
        rbtSexoCadastro.Value = "";


        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CadastrarPessoaFisica", "openModalAdd();", true);
    }

    public bool ObtemPedidoPessoa(int id)
    {
        pessoa = servicoDePessoas.ObtemPedidoPessoa(id);

        if (pessoa.Codigo > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}