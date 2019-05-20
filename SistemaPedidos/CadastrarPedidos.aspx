<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CadastrarPedidos.aspx.cs" Inherits="CadastrarPedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Roboto|Varela+Round">
    <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link href="Content/css/CadastrarPedido.css" rel="stylesheet" />
    <script src="Scripts/CadastrarPedido.js"></script>
    <asp:Panel ID="PanelFunci" runat="server" Width="810px">
        <div class="container" runat="server">
            <div class="table-wrapper">
                <div class="table-title">
                    <div class="row">
                        <div class="col-sm-6">
                            <h2>Pedidos</h2>
                        </div>
                        <div class="col-sm-6">
                            <asp:LinkButton CssClass="btn btn-success" runat="server" OnCommand="btnCadastrar_Click"><i class="material-icons">&#xE147;</i> <span>Novo</span></asp:LinkButton>
                        </div>
                    </div>
                </div>


                <table class="table table-striped table-hover">
                    <asp:Repeater ID="rptPedidos" runat="server">
                        <HeaderTemplate>
                            <thead>
                                <tr>
                                    <th>Código</th>
                                    <th>Comprador</th>
                                    <th>Fornecedor</th>
                                    <th>Produto</th>
                                    <th>Quantidade</th>
                                    <th>Valor Unitário</th>
                                    <th>Data do Pedido</th>
                                </tr>
                            </thead>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td style="text-align: center;"><%# Eval("Codigo") %></td>
                                <td><%# Eval("Comprador.Nome") %></td>
                                <td><%# Eval("Vendedor.Nome") %></td>
                                <td><%# Eval("Item.Produto.Nome") %></td>
                                <td><%# Eval("Item.Quantidade") %></td>
                                <td><%# Eval("Item.ValorUnitario") %></td>
                                <td><%# Eval("DataPedido") %></td>
                                <td>
                                    <asp:LinkButton CssClass="edit" runat="server" OnCommand="btnAlterar_Click" CommandArgument='<%#Eval("Codigo") %>'><i class="material-icons" title="Edit">&#xE254;</i></asp:LinkButton>
                                    <asp:LinkButton CssClass="delete" runat="server" OnCommand="btnDeletar_Click" CommandArgument='<%#Eval("Codigo") %>'><i class="material-icons" title="Delete">&#xE872;</i></asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>

    </asp:Panel>
    <form id="Form1" runat="server">
        <div id="CadastrarPedido" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">

                    <div class="modal-header">
                        <h4 class="modal-title">Cadastrar Pedido</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label>Comprador</label>
                            <asp:DropDownList CssClass="form-control" runat="server" ID="ddlCompradorCadastro">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Vendedor</label>
                            <asp:DropDownList CssClass="form-control" runat="server" ID="ddlVendedorCadastro" OnSelectedIndexChanged="ddlVendedorCadastro_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Produto</label>
                            <asp:DropDownList CssClass="form-control" runat="server" ID="ddlProdutoCadastro">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Valor Unitário</label>
                            <input type="text" id="txtValorUnitarioCadastro" class="form-control" runat="server" required>
                        </div>
                        <div class="form-group">
                            <label>Quantidade</label>
                            <input type="text" id="txtQuantidadeCadastro" class="form-control" runat="server" required>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <input type="button" class="btn btn-default" data-dismiss="modal" value="Cancelar">
                        <asp:Button ID="btnCadastrarPedido" class="btn btn-success" Text="Cadastrar" runat="server" OnClick="btnCadastrarPedido_Click" UseSubmitBehavior="false" />
                    </div>
                </div>
            </div>
        </div>

        <div id="EditarPedido" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Editar Pedido</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label>Comprador</label>
                            <asp:DropDownList CssClass="form-control" runat="server" ID="ddlCompradorAlteracao">
                            </asp:DropDownList>
                            <asp:HiddenField ID="hfID" runat="server" />
                        </div>
                        <div class="form-group">
                            <label>Vendedor</label>
                            <asp:DropDownList CssClass="form-control" runat="server" ID="ddlVendedorAlteracao" OnSelectedIndexChanged="ddlVendedorAlteracao_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Produto</label>
                            <asp:DropDownList CssClass="form-control" runat="server" ID="ddlProdutoAlteracao">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Valor Unitário</label>
                            <input type="text" id="txtValorUnitarioAlteracao" class="form-control" runat="server" required>
                        </div>
                        <div class="form-group">
                            <label>Quantidade</label>
                            <input type="text" id="txtQuantidadeAlteracao" class="form-control" runat="server" required>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <input type="button" class="btn btn-default" data-dismiss="modal" value="Cancelar">
                        <asp:Button ID="btnAlterarPedido" class="btn btn-success" Text="Alterar" runat="server" OnClick="btnAlterarPedido_Click" UseSubmitBehavior="false" />
                    </div>
                </div>
            </div>
        </div>

        <!-- Delete Modal HTML -->
        <div id="DeletarPedido" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Deletar Pedido</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    </div>
                    <div class="modal-body">
                        <asp:HiddenField ID="hdIDDelete" runat="server" />
                        <p>Você tem certeza que deseja deletar o Pedido selecionado</p>
                        <p class="text-warning"><small>Essa ação não poderá ser desfeita.</small></p>
                    </div>
                    <div class="modal-footer">
                        <input type="button" class="btn btn-default" data-dismiss="modal" value="Cancelar">
                        <asp:Button ID="btnDeletarPedido" class="btn btn-danger" Text="Excluir" runat="server" OnClick="btnDeletarPedido_Click" UseSubmitBehavior="false" />
                    </div>
                </div>
            </div>
        </div>

         <div id="AvisoPedido" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <form>
                        <div class="modal-header">
                            <h4 class="modal-title">Aviso</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                            <p>Preencha todos os campos!</p>
                        </div>
                        <div class="modal-footer">
                            <input type="button" class="btn btn-default" data-dismiss="modal" value="Ok">                           
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </form>
</asp:Content>

