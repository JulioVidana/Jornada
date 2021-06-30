<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="catalogo_jornadas.aspx.cs" Inherits="Forms_catalogo_jornadas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
     <link href="<%= ResolveUrl("~/content/css/plugins/dataTables/datatables.min.css") %>" rel="stylesheet">
    <link href="<%= ResolveUrl("~/content/css/plugins/iCheck/custom.css") %>" rel="stylesheet">
    <link href="<%= ResolveUrl("~/content/css/plugins/awesome-bootstrap-checkbox/awesome-bootstrap-checkbox.css") %>" rel="stylesheet">
    <link href="<%= ResolveUrl("~/content/css/plugins/sweetalert/sweetalert.css") %>" rel="stylesheet">
     <link href="<%= ResolveUrl("~/content/css/plugins/chosen/bootstrap-chosen.css") %>" rel="stylesheet">
     <link href="<%= ResolveUrl("~/content/css/plugins/footable/footable.core.css") %>" rel="stylesheet">
     <link href="<%= ResolveUrl("~/content/css/plugins/datapicker/datepicker3.css") %>" rel="stylesheet">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">
  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
  

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
                            <h5>Registro de Jornadas</h5>
                        </div>
                        <div class="ibox-content">

                            <asp:UpdatePanel runat="server" ID="UP_agregar" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:HiddenField runat="server" ID="Hid_IDJornada"  />
                                    <div class="form-group  row">
                                        <div class="col-md-3 col-sm-12 col-xs-12">
                                            <label>Jornada:</label>
                                            <asp:TextBox ID="Jornada_TextBox" runat="server" placeholder="Nombre de la Jornada" MaxLength="50" CssClass="form-control" autocomplete="off"  />
                                        </div>
                                        
                                         <div class="col-md-3 col-sm-12 col-xs-12">
                                            <label>Anfitrión:</label>
                                            <asp:TextBox ID="txt_anfitrion" runat="server" placeholder="Nombre de anfitrión" MaxLength="50" CssClass="form-control" autocomplete="off"  />
                                        </div>
                                         <div class="col-md-3 col-sm-12 col-xs-12">
                                            <label>Colonia:</label>
                                            <asp:DropDownList ID="DD_Colonia" runat="server" class="chosen-select" DataTextField="colonia" required="true"
                                                AppendDataBoundItems="true" DataValueField="id_colonia">
                                                <asp:ListItem Value="0" Text="---Selecciona Colonia---"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12">
                                            <label>Dirección:</label>
                                            <asp:TextBox ID="txt_direccion" runat="server" placeholder="Escribe Dirección" MaxLength="100" CssClass="form-control" autocomplete="off"  />
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12">
                                            <label>Teléfono:</label>
                                            <asp:TextBox ID="txt_telefono" runat="server" MaxLength="10" placeholder="Escribe Teléfono"  CssClass="form-control number" autocomplete="off"  />
                                        </div>
                                         <div class="col-md-3 col-sm-12 col-xs-12">
                                            <label>Fecha:</label>
                                              <div class="input-group m-b">
                                                <asp:TextBox ID="txt_Fecha" runat="server" onkeydown="return false;" CssClass="form-control datepicker" />
                                                <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-sm-12 col-xs-12">
                                            <label>Estatus:</label>
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
                                        DataKeyNames="id_jornada"
                                        OnPreRender="Listado_GridView_PreRender"
                                        OnRowCommand="Listado_GridView_RowCommand">
                                        <Columns>
                                            <asp:ButtonField Text="Editar" CommandName="Editar" ButtonType="Button" ControlStyle-CssClass="btn btn-info " ItemStyle-Width="8%" />
                                            <asp:BoundField DataField="id_jornada" HeaderText="ID" ItemStyle-Width="5%" />
                                            <asp:BoundField DataField="jornada" HeaderText="Jornada" />
                                            <asp:BoundField DataField="host" HeaderText="Anfitrion" />
                                            <asp:BoundField DataField="colonia" HeaderText="Colonia" />
                                             <asp:BoundField DataField="direccion" HeaderText="Direccion" />
                                            <asp:BoundField DataField="telefono" HeaderText="Telefono" />
                                            <asp:BoundField DataField="estatus" HeaderText="Estatus" />
                                            <asp:BoundField DataField="activo" HeaderText="activo" />
                                            <asp:BoundField DataField="id_colonia" HeaderText="id_colonia" />
                                             <asp:BoundField DataField="fecha" HeaderText="Fecha" />
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



</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphScript" Runat="Server">
      <script src="<%= ResolveUrl("~/content/js/plugins/dataTables/datatables.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/content/js/plugins/dataTables/dataTables.bootstrap4.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/content/js/plugins/iCheck/icheck.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/content/js/plugins/sweetalert/sweetalert.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/content/js/plugins/chosen/chosen.jquery.js") %>"></script>
    <script src="<%= ResolveUrl("~/content/js/plugins/footable/footable.all.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/content/js/plugins/datapicker/bootstrap-datepicker.js") %>"></script>

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

             $(".datepicker").datepicker({
                keyboardNavigation: false,
                forceParse: false,
                autoclose: true,
                format: "dd/mm/yyyy"
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

