using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CadastrarPedidos : System.Web.UI.Page
{
    ServicoDePedidos servicoDePedidos = new ServicoDePedidos();
    ServicoDePessoasJuridicas servicoDePessoasJuridicas = new ServicoDePessoasJuridicas();
    ServicoDePessoasFisicas servicoDePessoasFisicas = new ServicoDePessoasFisicas();
    ServicoDeProdutos servicoDeProdutos = new ServicoDeProdutos();
    Pedido Pedido = new Pedido();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ListarPedidos();
        }

    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    public void ListarPedidos()
    {

        rptPedidos.DataSource = servicoDePedidos.ListarPedidos();
        rptPedidos.DataBind();
    }

    private void rptPedidos_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {

        }
    }

    protected void btnCadastrarPedido_Click(object sender, EventArgs e)
    {

        if (ddlCompradorCadastro.SelectedValue != "0" && ddlVendedorCadastro.SelectedValue != "0" && ddlProdutoCadastro.SelectedValue != "0" && ddlProdutoCadastro.SelectedValue != "" && txtValorUnitarioCadastro.Value.Length > 0 && txtQuantidadeCadastro.Value.Length > 0)
        {
            ItemDoPedido item = new ItemDoPedido();

            Pedido.Comprador = servicoDePessoasFisicas.ObtemPessoa(Convert.ToInt32(ddlCompradorCadastro.SelectedValue));
            Pedido.Vendedor = servicoDePessoasJuridicas.ObtemPessoa(Convert.ToInt32(ddlVendedorCadastro.SelectedItem.Value));
            Pedido.Item = item;
            Pedido.Item.Produto = servicoDeProdutos.ObtemProduto(Convert.ToInt32(ddlProdutoCadastro.SelectedItem.Value));
            Pedido.Item.ValorUnitario = Convert.ToDecimal(txtValorUnitarioCadastro.Value);
            Pedido.Item.Quantidade = Convert.ToInt32(txtQuantidadeCadastro.Value);

            servicoDePedidos.CadastraPedido(Pedido);
        } else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AvisoPedido", "openModalWarning();", true);
        }

        ListarPedidos();
    }


    protected void btnAlterarPedido_Click(object sender, EventArgs e)
    {
        if (ddlCompradorAlteracao.SelectedValue != "0" && ddlVendedorAlteracao.SelectedValue != "0" && ddlProdutoAlteracao.SelectedValue != "0" && ddlProdutoAlteracao.SelectedValue != "" && txtValorUnitarioAlteracao.Value.Length > 0 && txtQuantidadeAlteracao.Value.Length > 0)
        {
            ItemDoPedido item = new ItemDoPedido();

            Pedido.Codigo = Convert.ToInt32(hfID.Value);
            Pedido.Comprador = servicoDePessoasFisicas.ObtemPessoa(Convert.ToInt32(ddlCompradorAlteracao.SelectedValue));
            Pedido.Vendedor = servicoDePessoasJuridicas.ObtemPessoa(Convert.ToInt32(ddlVendedorAlteracao.SelectedItem.Value));
            Pedido.DataPedido = DateTime.Now;
            Pedido.Item = item;
            Pedido.Item.Produto = servicoDeProdutos.ObtemProduto(Convert.ToInt32(ddlProdutoAlteracao.SelectedItem.Value));
            Pedido.Item.ValorUnitario = Convert.ToDecimal(txtValorUnitarioAlteracao.Value);
            Pedido.Item.Quantidade = Convert.ToInt32(txtQuantidadeAlteracao.Value);

            servicoDePedidos.AlteraPedido(Pedido);
        } else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AvisoPedido", "openModalWarning();", true);
        }

        ListarPedidos();
    }

    protected void btnAlterar_Click(object sender, CommandEventArgs e)
    {

        ddlCompradorAlteracao.Items.Clear();
        ddlVendedorAlteracao.Items.Clear();
        ddlProdutoAlteracao.Items.Clear();
        txtValorUnitarioAlteracao.Value = "";
        txtQuantidadeAlteracao.Value = "";

        var listaPessoaFisica = servicoDePessoasFisicas.ListaPessoas();

        if (listaPessoaFisica == null || !listaPessoaFisica.Any())
        {
            throw new Exception();
        }

        foreach (var item in listaPessoaFisica)
        {
            ddlCompradorAlteracao.Items.Add(new ListItem() { Text = item.Nome, Value = item.Codigo.ToString() });
        }

        ddlCompradorAlteracao.Items.Insert(0, new ListItem()
        {
            Text = "Selecionar",
            Value = "0"
        });

        var listaPessoaJuridica = servicoDePessoasJuridicas.ListaPessoas();

        if (listaPessoaJuridica == null || !listaPessoaJuridica.Any())
        {
            throw new Exception();
        }

        var empresasAtivas = from l in listaPessoaJuridica where (l.Ativa) select l;

        foreach (var item in empresasAtivas)
        {
            ddlVendedorAlteracao.Items.Add(new ListItem() { Text = item.Nome, Value = item.Codigo.ToString() });
        }

        ddlVendedorAlteracao.Items.Insert(0, new ListItem()
        {
            Text = "Selecionar",
            Value = "0"
        });

        var id = Convert.ToInt32(e.CommandArgument);

        Pedido = servicoDePedidos.ObtemPedido(id);

        if (Pedido != null)
        {

            hfID.Value = Pedido.Codigo.ToString();
            ddlCompradorAlteracao.SelectedValue = Pedido.Comprador.Codigo.ToString();
            ddlVendedorAlteracao.SelectedValue = Pedido.Vendedor.Codigo.ToString();

            ddlProdutoAlteracao.Items.Clear();
            CarregaProdutosAlteracao(Pedido.Vendedor.Codigo);

            ddlProdutoAlteracao.SelectedValue = Pedido.Item.Produto.Codigo.ToString();
            txtValorUnitarioAlteracao.Value = Pedido.Item.ValorUnitario.ToString();
            txtQuantidadeAlteracao.Value = Pedido.Item.Quantidade.ToString();
        }


        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditarPedido", "openModalUpdate();", true);
    }

    protected void btnDeletarPedido_Click(object sender, EventArgs e)
    {
        Pedido.Codigo = Convert.ToInt32(hfID.Value);

        servicoDePedidos.RemovePedido(Pedido);

        ListarPedidos();
    }

    protected void btnDeletar_Click(object sender, CommandEventArgs e)
    {
        hfID.Value = e.CommandArgument.ToString();

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "DeletarPedido", "openModalDelete();", true);
    }

    protected void ddlVendedorCadastro_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlVendedorCadastro.SelectedIndex > 0)
        {
            CarregaProdutosCadastro(Convert.ToInt32(ddlVendedorCadastro.SelectedItem.Value));
        }

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CadastrarPedido", "openModalAdd();", true);
    }

    public void CarregaProdutosCadastro(int codigoEmpresa)
    {
        ddlProdutoCadastro.Items.Clear();

        var listaProdutos = servicoDeProdutos.ListarProdutos();

        if (listaProdutos == null || !listaProdutos.Any())
        {
            throw new Exception();
        }

        var produtosEmpresa = listaProdutos.Where(m => m.Fornecedor.Codigo == codigoEmpresa).ToList();

        foreach (var item in produtosEmpresa)
        {
            ddlProdutoCadastro.Items.Add(new ListItem() { Text = item.Nome, Value = item.Codigo.ToString() });
        }

        ddlProdutoCadastro.Items.Insert(0, new ListItem()
        {
            Text = "Selecionar",
            Value = "0"
        });
    }

    protected void ddlVendedorAlteracao_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlVendedorAlteracao.SelectedIndex > 0)
        {
            CarregaProdutosAlteracao(Convert.ToInt32(ddlVendedorAlteracao.SelectedItem.Value));
        }

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditarPedido", "openModalUpdate();", true);
    }

    public void CarregaProdutosAlteracao(int codigoEmpresa)
    {
        ddlProdutoAlteracao.Items.Clear();

        var listaProdutos = servicoDeProdutos.ListarProdutos();

        if (listaProdutos == null || !listaProdutos.Any())
        {
            throw new Exception();
        }

        var produtosEmpresa = listaProdutos.Where(m => m.Fornecedor.Codigo == codigoEmpresa).ToList();

        foreach (var item in produtosEmpresa)
        {
            ddlProdutoAlteracao.Items.Add(new ListItem() { Text = item.Nome, Value = item.Codigo.ToString() });
        }

        ddlProdutoAlteracao.Items.Insert(0, new ListItem()
        {
            Text = "Selecionar",
            Value = "0"
        });
    }

    protected void btnCadastrar_Click(object sender, CommandEventArgs e)
    {
        ddlCompradorCadastro.Items.Clear();
        ddlVendedorCadastro.Items.Clear();
        ddlProdutoCadastro.Items.Clear();
        txtValorUnitarioCadastro.Value = "";
        txtQuantidadeCadastro.Value = "";

        var listaPessoaFisica = servicoDePessoasFisicas.ListaPessoas();

        if (listaPessoaFisica == null || !listaPessoaFisica.Any())
        {
            throw new Exception();
        }

        foreach (var item in listaPessoaFisica)
        {
            ddlCompradorCadastro.Items.Add(new ListItem() { Text = item.Nome, Value = item.Codigo.ToString() });
        }

        ddlCompradorCadastro.Items.Insert(0, new ListItem()
        {
            Text = "Selecionar",
            Value = "0"
        });

        var listaPessoaJuridica = servicoDePessoasJuridicas.ListaPessoas();

        if (listaPessoaJuridica == null || !listaPessoaJuridica.Any())
        {
            throw new Exception();
        }

        var empresasAtivas = from l in listaPessoaJuridica where (l.Ativa) select l;

        foreach (var item in empresasAtivas)
        {
            ddlVendedorCadastro.Items.Add(new ListItem() { Text = item.Nome, Value = item.Codigo.ToString() });
        }

        ddlVendedorCadastro.Items.Insert(0, new ListItem()
        {
            Text = "Selecionar",
            Value = "0"
        });

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CadastrarPedido", "openModalAdd();", true);
    }


}