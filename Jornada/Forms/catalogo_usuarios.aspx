<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="catalogo_usuarios.aspx.cs" Inherits="Forms_catalogo_usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="<%= ResolveUrl("~/content/css/plugins/dataTables/datatables.min.css") %>" rel="stylesheet">
    <link href="<%= ResolveUrl("~/content/css/plugins/iCheck/custom.css") %>" rel="stylesheet">
    <link href="<%= ResolveUrl("~/content/css/plugins/awesome-bootstrap-checkbox/awesome-bootstrap-checkbox.css") %>" rel="stylesheet">
    <link href="<%= ResolveUrl("~/content/css/plugins/sweetalert/sweetalert.css") %>" rel="stylesheet">
     <link href="<%= ResolveUrl("~/content/css/plugins/chosen/bootstrap-chosen.css") %>" rel="stylesheet">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="Server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
     <div class="row wrapper page-heading">
        <div class="col-md-10 col-sm-10 col-xs-12">
            <h2>Catálogo de Usuarios</h2>
        </div>
        <div class="col-md-2 col-sm-2 col-xs-2 hidden-xs text-right">
            <i class=" text-danger   fa fa-users"></i>
        </div>
    </div>
     <p class="alert alert-danger animated fadeInRight">
        <label class="col-form-label">Si el usuario tiene asignado algún acceso, NO se podrá eliminar</label>
    </p>

     <div class="ibox animated fadeInRight" id="ibox-index">
        <div class="ibox-content">
            <div class="sk-spinner sk-spinner-double-bounce">
                <div class="sk-double-bounce1"></div>
                <div class="sk-double-bounce2"></div>
            </div>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div class="ibox float-e-margins" style="border-left: 1px solid #6c6e71; border-right: 1px solid #6c6e71; border-bottom: 1px solid #6c6e71;">
                        <div class="ibox-title my-ibox">
                            <h5>Registro</h5>
                        </div>
                        <div class="ibox-content">

                            <asp:UpdatePanel runat="server" ID="UP_agregar" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:HiddenField runat="server" ID="Hid_IDUsuario"  />
                                    <div class="form-group  row">
                                        <div class="col-md-3 col-sm-12 col-xs-12">
                                            <label>Nombre</label>
                                            <asp:TextBox ID="Nombre_TextBox" runat="server" placeholder="nombre del usuario" MaxLength="50" CssClass="form-control" autocomplete="off"  />
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12">
                                            <label>Correo</label>
                                            <asp:TextBox ID="Correo_TextBox" runat="server" placeholder="correo del usuario" MaxLength="150" CssClass="form-control " autocomplete="off"  />
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12">
                                            <label>Area</label>
                                            <asp:DropDownList ID="DD_Area" runat="server" class="chosen-select" DataTextField="Area" 
                                                AppendDataBoundItems="true" DataValueField="id_area">
                                                <asp:ListItem Value="0" Text="Seleccionar opción" Selected="True">
                                                </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12">
                                            <label>Estatus</label>
                                           <asp:RadioButtonList ID="Activo_RadioButtonList" runat="server" class="i-checks" RepeatDirection="Horizontal" >
                                            <asp:ListItem Value="True" Text="&nbsp; Activo &nbsp;"></asp:ListItem>
                                            <asp:ListItem Value="False" Text="&nbsp; Baja &nbsp;"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        </div>
                                    </div>

                                    <div class="form-group  row">
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <asp:Button ID="Btn_Agregar" runat="server" Text="Agregar" CssClass="btn btn-w-m btn-default" OnClick="Nuevo_Registro" />
                                            <asp:Button ID="Btn_Actualizar" runat="server" Text="Actualizar" CssClass="btn  btn-primary" Enabled="false" Visible="false" OnClick="Nuevo_Registro"  />
                                             <button runat="server" id="btn_Limpiar" onserverclick="LimpiarForm" class="btn btn-white btn-bitbucket btne " type="button"  visible="false"><i class="mdi text-danger mdi-close-outline"></i>Cancelar</button>
                                            <asp:Button ID="Btn_Borrar" runat="server" Text="Borrar" CssClass="btn btn-w-s btn-danger" Enabled="false" Visible="false"    OnClientClick="return borrar(this);" OnClick="Borra_Registro" /> 
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="hr-line-dashed"></div>
                             <div class="table-responsive">
                        <asp:UpdatePanel runat="server" ID="UP_grid" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="Listado_GridView" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-striped table-bordered table-hover dataTables-example"
                                        EmptyDataText="No hay Registros..."
                                        DataKeyNames="id_usuario"
                                        OnPreRender="Listado_GridView_PreRender"
                                        OnRowCommand="Listado_GridView_RowCommand">
                                        <Columns>
                                            <asp:ButtonField Text="Editar" CommandName="Editar" ButtonType="Button" ControlStyle-CssClass="btn btn-info " ItemStyle-Width="8%" />
                                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                            <asp:BoundField DataField="correo" HeaderText="Correo" />
                                            <asp:BoundField DataField="estatus" HeaderText="Estatus" />
                                             <asp:BoundField DataField="Areas" HeaderText="Area" />
                                             <asp:ButtonField  Text="<i class='fa fa-lock' data-toggle='tooltip' data-placement='bottom' title='Ver Password'></i>"  CommandName="Pass" 
                                                 ButtonType="Link" ControlStyle-CssClass="btn btn-success" ItemStyle-Width="2%" />
                                             <asp:BoundField DataField="id_usuario" HeaderText="ID" />
                                             <asp:BoundField DataField="area" HeaderText="ID Area" />
                                             <asp:BoundField DataField="activo" HeaderText="ID Activo" />
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                                    </asp:GridView>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                        </div>
                    </div>
                </div>
            </div>

            </div>
         </div>


      <%--VENTANA PARA CAMBIAR CONTRASEÑA--%>

    <div id="Pass_Div" class="modal fade" tabindex="-1" role="dialog" data-backdrop="static" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:updatepanel id="Updatepanel1" runat="server" updatemode="Always">
                    <ContentTemplate>
                <div class="modal-header">
                    <h4 class="modal-title"> <asp:Label ID="Label1" runat="server" Text="<i  class='fa fa-lock'></i> Contraseña de Usuario"></asp:Label></h4>
                    <asp:HiddenField runat="server" ID="ID_Usuario_Hidden"></asp:HiddenField>
   
                </div>
                <div class="modal-body">
                    <div class="row">
                        <form role="form">

                             <div class="form-group row"><label class="col-sm-2 col-form-label">Usuario:</label>
                                    <div class="col-sm-10">
                                     <asp:TextBox runat="server" ID="txt_usurio_pass" CssClass="form-control" ReadOnly="true"></asp:TextBox>

                                    </div>
                                </div>
                            <div class="form-group row"><label class="col-sm-2 col-form-label">Contraseña:</label>
                                    <div class="col-sm-10">
                                        <asp:TextBox runat="server" ID="Contraseña_Actual_TextBox" CssClass="form-control" ReadOnly="true"></asp:TextBox>

                                    </div>
                                </div>

                             <div class="hr-line-dashed"></div>
                            <p class="text-danger">Teclee y confirme su nueva contraseña.</p>
                             <div class="form-group row"><label class="col-sm-2 col-form-label">Nueva:</label>
                                    <div class="col-sm-10">
                                       <asp:TextBox runat="server" ID="Password1_TextBox" placeholder="contraseña" type="password" MaxLength="20" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group row"><label class="col-sm-2 col-form-label">Confirme:</label>
                                    <div class="col-sm-10">
                                        <asp:TextBox runat="server" ID="Password2_TextBox" placeholder="confirme" type="password" MaxLength="20" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                       
                        </form>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btn_guardar_segui" runat="server" Text="Cambiar" CssClass="btn btn-w-m btn-success" OnClick="Cambiar_contra"  />
                    <button type="button" class="btn btn-w-m btn-default" data-dismiss="modal" formnovalidate="formnovalidate" value="Save">Cancelar</button>
                    <%--<asp:Button ID="btn_cancelar_segui" runat="server" Text="Cancelar" CssClass="btn btn-w-m btn-default"   OnClick="Cancelar" />--%>
                </div>
                </ContentTemplate>
                </asp:updatepanel>

            </div>
        </div>
    </div>

   
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScript" runat="Server">
    <script src="<%= ResolveUrl("~/content/js/plugins/dataTables/datatables.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/content/js/plugins/dataTables/dataTables.bootstrap4.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/content/js/plugins/iCheck/icheck.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/content/js/plugins/sweetalert/sweetalert.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/content/js/plugins/chosen/chosen.jquery.js") %>"></script>

     <!-- Jquery Validate -->
    <script src="<%= ResolveUrl("~/content/js/plugins/validate/jquery.validate.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/content/js/plugins/validate/validate_ES.js") %>"></script>

    <script type="text/javascript">



        $(document).ready(function () {
            ejecuta_javascript();
             fn_init();
        });

        function ejecuta_javascript() {
            hideModal();

            $(".chosen-select").chosen({ width: "100%" });

            $('.i-checks').iCheck({
                checkboxClass: 'icheckbox_square-green',
                radioClass: 'iradio_square-green',
            });

            $('.dataTables-example').DataTable({
                "language": {
                    "lengthMenu": "Mostrar _MENU_ registros por página",
                    "zeroRecords": "No se encontraron registros",
                    "info": "Página _PAGE_ de _PAGES_",
                    "infoEmpty": "No se encontraron registros",
                    "infoFiltered": "(Filtrado de _MAX_ total de registros)",
                    "sSearch": "Buscar:",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sLast": "Último",
                        "sNext": "Siguiente",
                        "sPrevious": "Anterior"
                    },
                },
                stateSave: true,
                destroy: true,
                pageLength: 10,
                responsive: true,

            });
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

        var obj = {status:false,ele:null};
        function borrar(me) {
	        if(obj.status) return true;
	        swal({
		       title: "¿Quieres Eliminar Registro?",
		        text: "",
		        type: "warning",
		        showCancelButton: true,
		        confirmButtonColor: "#DD6B55",
                confirmButtonText: "SI",
                cancelButtonText: "NO",
                closeOnConfirm: false,
                closeOnCancel: true,
		        showLoaderOnConfirm: true
		        },
                function (isConfirm) {
                    if (isConfirm) {
                        obj.status=true;
                        obj.ele = me;
                        obj.ele.click();
                         //swal("Deleted!", "Your imaginary file has been deleted.", "success");
                    } else {
                        //swal("Cancelled", "Your imaginary file is safe :)", "error");
                    }
                });
	        
	        return false;
        }

         function fn_init() {
             $("#form").validate({
               
            });

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(onEachRequest);
        }

         function onEachRequest(sender, args) {
            if ($("#form").valid() == false) {
                args.set_cancel(true);
            }
        }

    </script>


</asp:Content>

