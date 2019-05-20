using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CadastrarProdutos : System.Web.UI.Page
{
    ServicoDeProdutos servicoDeProdutos = new ServicoDeProdutos();
    ServicoDePessoasJuridicas servicoDePessoasJuridicas = new ServicoDePessoasJuridicas();
    Produto produto = new Produto();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            ListarProdutos();
        }

    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    public void ListarProdutos()  
    {

        rptProdutos.DataSource = servicoDeProdutos.ListarProdutos();
        rptProdutos.DataBind();
    }

    private void rptProdutos_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {
          
        }
    }

    protected void btnCadastrarProduto_Click(object sender, EventArgs e)
    {
        if(txtNomeCadastro.Value.Length > 0 && txtPrecoCadastro.Value.Length > 0 && ddlFornecedorCadastro.SelectedValue != "0")
        {
            produto.Nome = txtNomeCadastro.Value;
            produto.Preco = Convert.ToDecimal(txtPrecoCadastro.Value);
            produto.Fornecedor = servicoDePessoasJuridicas.ObtemPessoa(Convert.ToInt32(ddlFornecedorCadastro.SelectedItem.Value));

            servicoDeProdutos.CadastraProduto(produto);
        } else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AvisoProduto", "openModalWarning();", true);
        }

        ListarProdutos();
    }


    protected void btnAlterarProduto_Click(object sender, EventArgs e)
    {
        if (txtNomeAlteracao.Value.Length > 0 && txtPrecoAlteracao.Value.Length > 0 && ddlFornecedorAlteracao.SelectedValue != "0")
        {
            produto.Codigo = Convert.ToInt32(hfID.Value);
            produto.Nome = txtNomeAlteracao.Value;
            produto.Preco = Convert.ToDecimal(txtPrecoAlteracao.Value);
            produto.Fornecedor = servicoDePessoasJuridicas.ObtemPessoa(Convert.ToInt32(ddlFornecedorAlteracao.SelectedItem.Value));

            servicoDeProdutos.AlteraProduto(produto);
        } else {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AvisoProduto", "openModalWarning();", true);
        }
      
        ListarProdutos();
    }

    protected void btnAlterar_Click(object sender, CommandEventArgs e)
    {

        ddlFornecedorAlteracao.Items.Clear();
        ddlFornecedorAlteracao.DataBind();

        var listaPessoa = servicoDePessoasJuridicas.ListaPessoas();

        if (listaPessoa == null || !listaPessoa.Any())
        {
            throw new Exception();
        }

        var empresasAtivas = from l in listaPessoa where (l.Ativa) select l;

        foreach (var item in empresasAtivas)
        {
            ddlFornecedorAlteracao.Items.Add(new ListItem() { Text = item.Nome, Value = item.Codigo.ToString() });
        }

        ddlFornecedorAlteracao.Items.Insert(0, new ListItem()
        {
            Text = "Selecionar",
            Value = "0"
        });

        var id = Convert.ToInt32(e.CommandArgument);

        produto = servicoDeProdutos.ObtemProduto(id);

        if (produto != null)
        {
            hfID.Value = produto.Codigo.ToString();
            txtNomeAlteracao.Value = produto.Nome;
            txtPrecoAlteracao.Value = produto.Preco.ToString();
            ddlFornecedorAlteracao.SelectedValue = produto.Fornecedor.Codigo.ToString();
        }


        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditarProduto", "openModalUpdate();", true);
    }

    protected void btnDeletarProduto_Click(object sender, EventArgs e)
    {
        lblErro.Text = "";

        if (!ObtemPedidoProduto(Convert.ToInt32(hfID.Value)))
        {
            produto.Codigo = Convert.ToInt32(hfID.Value);

            servicoDeProdutos.RemoveProduto(produto);
        } else {
            lblErro.Visible = true;
            lblErro.Text = "O PRODUTO POSSUI UM PEDIDO VINCULADO A ELE!";
        }



        ListarProdutos();
    }

    protected void btnDeletar_Click(object sender, CommandEventArgs e)
    {
        hfID.Value = e.CommandArgument.ToString();

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "DeletarProduto", "openModalDelete();", true);
    }


    protected void btnCadastrar_Click(object sender, CommandEventArgs e)
    {
        txtNomeCadastro.Value = "";
        txtPrecoCadastro.Value = "";
        ddlFornecedorCadastro.Items.Clear();
        ddlFornecedorCadastro.DataBind();

        var listaPessoa = servicoDePessoasJuridicas.ListaPessoas();

        if (listaPessoa == null || !listaPessoa.Any())
        {
            throw new Exception();
        }

        var empresasAtivas = from l in listaPessoa where (l.Ativa) select l;

        foreach (var item in empresasAtivas)
        {
            ddlFornecedorCadastro.Items.Add(new ListItem() { Text = item.Nome, Value = item.Codigo.ToString() });
        }

        ddlFornecedorCadastro.Items.Insert(0, new ListItem()
        {
            Text = "Selecionar",
            Value = "0"
        });

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CadastrarProduto", "openModalAdd();", true);
    }

    public bool ObtemPedidoProduto(int id)
    {
        produto = servicoDeProdutos.ObtemPedidoProduto(id);

        if (produto.Codigo > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}