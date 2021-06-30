<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="catalogo_accesos_usuario.aspx.cs" Inherits="Forms_catalogo_accesos_usuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
     <link href="<%= ResolveUrl("~/content/css/plugins/sweetalert/sweetalert.css") %>" rel="stylesheet">
    <link href="<%= ResolveUrl("~/content/css/plugins/chosen/bootstrap-chosen.css") %>" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:UpdatePanel runat="server" ID="UP_Pagina" UpdateMode="Always">
        <ContentTemplate>

            <div class="wrapper wrapper-content animated fadeInRight">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox ">
                            <div class="ibox-title">
                                <h5>Accesos por Usuario</h5>
                            </div>
                            <div class="ibox-content">
                                <div class="row">
                                        <div class="form-group row">
                                            <label class="col-lg-1 col-form-label text-lg-right text-sm-left">Usuario</label>
                                            <div class="col-lg-9">
                                                <asp:DropDownList ID="DD_Usrs" runat="server" class="chosen-select" AutoPostBack="True"
                                                    DataSourceID="Usuarios_SqlDataSource" DataTextField="nombre" DataValueField="id_usuario"
                                                    OnSelectedIndexChanged="Usuarios_DropDownList_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="Usuarios_SqlDataSource" runat="server" SelectCommand="spr_catalogo_usuarios" SelectCommandType="StoredProcedure" ConnectionString="<%$ ConnectionStrings:db_ConnectionString %>">
                                                    <SelectParameters>
                                                        <asp:Parameter DefaultValue="lee_usr" Name="funcion" Type="String" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:Button ID="Guardar_Button" runat="server" Text="Guardar" CssClass="btn btn-w-m btn-primary" OnClick="Guardar_Button_Click" />
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <div class="col-lg-12">
                                                <asp:TreeView ID="Accesos_TreeView" runat="server" ShowCheckBoxes="All" ExpandDepth="1" ShowLines="True">
                                                </asp:TreeView>
                                                <asp:SqlDataSource ID="Accesos_Usuario_SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:db_ConnectionString %>" SelectCommand="spr_portal_lee_accesos" SelectCommandType="StoredProcedure">
                                                    <SelectParameters>
                                                        <asp:Parameter DefaultValue="lee_Acceso_usr" Name="funcion" Type="String" />
                                                        <asp:ControlParameter ControlID="DD_Usrs"  Name="id_usuario" PropertyName="SelectedValue" Type="Int32" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                                <asp:SqlDataSource ID="Accesos_SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:db_ConnectionString %>" SelectCommand="spr_portal_lee_accesos" SelectCommandType="StoredProcedure">
                                                     <SelectParameters>
                                                        <asp:Parameter DefaultValue="lee_Acceso" Name="funcion" Type="String" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </div>
                                        </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScript" Runat="Server">

    <script src="<%= ResolveUrl("~/content/js/plugins/sweetalert/sweetalert.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/content/js/plugins/chosen/chosen.jquery.js") %>"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            ejecuta_javascript();
        });

        function ejecuta_javascript() {
            hideModal();

            $(".chosen-select").chosen({ width: "100%" });
        }

        function despliega_aviso(tipo, titulo, mensaje, txt_btn_no, txt_btn_si, txt_msj_no, txt_msj_si) {
            if (tipo == 'warning') {
                swal({ title: titulo, text: mensaje, type: tipo, showCancelButton: false });
            }
            else if (tipo == 'pregunta') {
                swal({ title: titulo, text: mensaje, type: "warning", showCancelButton: "true", confirmButtonColor: "#DD6B55", confirmButtonText: txt_btn_si, cancelButtonText: txt_btn_no, closeOnConfirm: true, closeOnCancel: true },
                    function (isConfirm) {
                        if (isConfirm) { despliega_aviso("normal", txt_msj_si, ""); return true; }
                        else { despliega_aviso("normal", txt_msj_no, ""); return false; }
                    });
            }
            else if (tipo == 'success') { swal({ title: titulo, text: mensaje, type: tipo }); }
            else { swal({ title: titulo, text: mensaje }); }
        }

        function hideModal() {
            $('.modal-backdrop').remove(); //Hide the backdrop
        }

    </script>


</asp:Content>

