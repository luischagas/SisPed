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
    ServicoDePessoasJuridicas servicoDePessoas = new ServicoDePessoasJuridicas();
    PessoaJuridica pessoa = new PessoaJuridica();

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
        rptPessoasJuridicas.DataSource = servicoDePessoas.ListaPessoas();
        rptPessoasJuridicas.DataBind();
    }

    protected void btnCadastrarPessoaJuridica_Click(object sender, EventArgs e)
    {

        if (txtNomeCadastro.Value.Length > 0 && txtEnderecoCadastro.Value.Length > 0 && txtCNPJCadastro.Value.Length > 0)
        {
            pessoa.Nome = txtNomeCadastro.Value;
            pessoa.Endereco = txtEnderecoCadastro.Value;
            pessoa.CNPJ = txtCNPJCadastro.Value;
            pessoa.Ativa = optAtivoCadastro.Checked ? true : false;
            servicoDePessoas.CadastraPessoa(pessoa);

        } else {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AvisoPessoaJuridica", "openModalWarning();", true);
        }

        ListarPessoas();
    }


    protected void btnAlterarPessoaJuridica_Click(object sender, EventArgs e)
    {
        if (txtNomeAlteracao.Value.Length > 0 && txtEnderecoAlteracao.Value.Length > 0 && txtCNPJAlteracao.Value.Length > 0)
        {
            pessoa.Codigo = Convert.ToInt32(hfID.Value);
            pessoa.Nome = txtNomeAlteracao.Value;
            pessoa.Endereco = txtEnderecoAlteracao.Value;
            pessoa.CNPJ = txtCNPJAlteracao.Value;
            if (optAtivoAlteracao.Checked == true)
            {
                pessoa.Ativa = true;
            }
            else
            {
                pessoa.Ativa = false;
            }
            servicoDePessoas.AlteraPessoa(pessoa);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AvisoPessoaJuridica", "openModalWarning();", true);
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
            txtCNPJAlteracao.Value = pessoa.CNPJ;
            if (pessoa.Ativa == true)
            {
                optAtivoAlteracao.Checked = true;
                optInativoAlteracao.Checked = false;
            }
            else
            {
                optAtivoAlteracao.Checked = false;
                optInativoAlteracao.Checked = true;
            }
        }

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditarPessoaJuridica", "openModalUpdate();", true);
    }

    protected void btnDeletarPessoaJuridica_Click(object sender, EventArgs e)
    {
        lblErro.Text = "";

        if (!ObtemProdutoPessoa(Convert.ToInt32(hfID.Value)))
        {
            pessoa.Codigo = Convert.ToInt32(hfID.Value);

            servicoDePessoas.RemovePessoa(pessoa);

            ListarPessoas();
        } else
        {
            lblErro.Visible = true;
            lblErro.Text = "A EMPRESA POSSUI UM PRODUTO VINCULADO A ELA!";
        }
          
    }

    protected void btnDeletar_Click(object sender, CommandEventArgs e)
    {
        hfID.Value = e.CommandArgument.ToString();

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "DeletarPessoaJuridica", "openModalDelete();", true);
    }

    protected void btnCadastrar_Click(object sender, CommandEventArgs e)
    {
        txtNomeCadastro.Value = "";
        txtEnderecoCadastro.Value = "";
        txtCNPJCadastro.Value = "";
        optAtivoCadastro.Checked = true;


        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CadastrarPessoaJuridica", "openModalAdd();", true);
    }

    public bool ObtemProdutoPessoa(int id)
    {
        pessoa = servicoDePessoas.ObtemProdutoPessoa(id);

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