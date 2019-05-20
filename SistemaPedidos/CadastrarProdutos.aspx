<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CadastrarProdutos.aspx.cs" Inherits="CadastrarProdutos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Roboto|Varela+Round">
    <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link href="Content/css/CadastrarPessoa.css" rel="stylesheet" />
    <script src="Scripts/CadastrarProduto.js"></script>
    <asp:Panel ID="PanelFunci" runat="server" Width="810px">
        <div class="container" runat="server">
            <div class="table-wrapper">
                <div class="table-title">
                    <div class="row">
                        <div class="col-sm-6">
                            <h2>Produtos</h2>
                        </div>
                        <div class="col-sm-6">
                             <asp:LinkButton CssClass="btn btn-success" runat="server" OnCommand="btnCadastrar_Click"><i class="material-icons">&#xE147;</i> <span>Novo</span></asp:LinkButton>
                        </div>
                    </div>
                </div>


                <table class="table table-striped table-hover">
                    <asp:Repeater ID="rptProdutos" runat="server">
                        <HeaderTemplate>
                            <thead>
                                <tr>
                                    <th>Código</th>
                                    <th>Nome</th>
                                    <th>Preço</th>
                                    <th>Fornecedor</th>
                                </tr>
                            </thead>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td width="5%" style="text-align: center;"><%# Eval("Codigo") %></td>
                                <td width="20%" ><%# Eval("Nome") %></td>
                                <td width="20%" ><%# Eval("Preco") %></td>
                                <td width="20%" ><%# Eval("Fornecedor.Nome") %></td>
                                <td width="20%" >
                                    <asp:LinkButton CssClass="edit" runat="server" OnCommand="btnAlterar_Click" CommandArgument='<%#Eval("Codigo") %>'><i class="material-icons" title="Edit">&#xE254;</i></asp:LinkButton>
                                    <asp:LinkButton CssClass="delete" runat="server" OnCommand="btnDeletar_Click" CommandArgument='<%#Eval("Codigo") %>'><i class="material-icons" title="Delete">&#xE872;</i></asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>

                 <asp:Label Text="" runat="server" ID="lblErro" ForeColor="red" Visible="false"></asp:Label>

            </div>
        </div>

    </asp:Panel>
    <form id="Form1" runat="server">
        <div id="CadastrarProduto" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">

                    <div class="modal-header">
                        <h4 class="modal-title">Cadastrar Produto</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label>Nome</label>
                            <input type="text" id="txtNomeCadastro" class="form-control" runat="server" required>
                        </div>
                        <div class="form-group">
                            <label>Preço</label>
                            <input type="text" id="txtPrecoCadastro" class="form-control" runat="server" required>
                        </div>
                        <div class="form-group">
                            <label>Fornecedor</label>
                            <asp:DropDownList CssClass="form-control" runat="server" id="ddlFornecedorCadastro">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <input type="button" class="btn btn-default" data-dismiss="modal" value="Cancelar">
                        <asp:Button ID="btnCadastrarProduto" class="btn btn-success" Text="Cadastrar" runat="server" OnClick="btnCadastrarProduto_Click" UseSubmitBehavior="false" />
                    </div>
                </div>
            </div>
        </div>

        <div id="EditarProduto" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Editar Produto</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label>Nome</label>
                            <input type="text" id="txtNomeAlteracao" class="form-control" runat="server" required>
                            <asp:HiddenField ID="hfID" runat="server" />
                        </div>
                        <div class="form-group">
                            <label>Preço</label>
                            <input type="text" id="txtPrecoAlteracao" class="form-control" runat="server" required>
                        </div>
                        <div class="form-group">
                            <label>Fornecedor</label>
                             <asp:DropDownList CssClass="form-control" runat="server" id="ddlFornecedorAlteracao">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <input type="button" class="btn btn-default" data-dismiss="modal" value="Cancelar">
                        <asp:Button ID="btnAlterarProduto" class="btn btn-success" Text="Alterar" runat="server" OnClick="btnAlterarProduto_Click" UseSubmitBehavior="false" />
                    </div>
                </div>
            </div>
        </div>

        <!-- Delete Modal HTML -->
        <div id="DeletarProduto" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Deletar Produto</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField ID="hdIDDelete" runat="server" />
                            <p>Você tem certeza que deseja deletar o produto selecionado</p>
                            <p class="text-warning"><small>Essa ação não poderá ser desfeita.</small></p>
                        </div>
                        <div class="modal-footer">
                            <input type="button" class="btn btn-default" data-dismiss="modal" value="Cancelar">
                            <asp:Button ID="btnDeletarProduto" class="btn btn-danger" Text="Excluir" runat="server" OnClick="btnDeletarProduto_Click" UseSubmitBehavior="false" />
                        </div>
                </div>
            </div>
        </div>

         <div id="AvisoProduto" class="modal fade">
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

