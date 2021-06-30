<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Forms_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
     <link href="<%= ResolveUrl("~/content/css/plugins/dataTables/datatables.min.css") %>" rel="stylesheet">
    <link href="<%= ResolveUrl("~/content/css/plugins/iCheck/custom.css") %>" rel="stylesheet">
    <link href="<%= ResolveUrl("~/content/css/plugins/awesome-bootstrap-checkbox/awesome-bootstrap-checkbox.css") %>" rel="stylesheet">
    <link href="<%= ResolveUrl("~/content/css/plugins/sweetalert/sweetalert.css") %>" rel="stylesheet">
    <link href="<%= ResolveUrl("~/content/css/plugins/chosen/bootstrap-chosen.css") %>" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
      <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div runat="server" class="col-lg-3" id="div_btn_capturas" visible="false">
                <button runat="server" id="btn_Capturas" type="button" class="btn dim btn-warning btn-block" onclick="window.location.href = '/Forms/Capturas.aspx';"  >
                    <i class="fa fa-edit fa-2x"></i>
                    <h3 class="font-bold no-margins">Capturas</h3>
                </button>
            </div>
            <div runat="server" class="col-lg-3" id="div_btn_seguimiento" visible="false">
                <button runat="server" id="Button1" type="button" class="btn dim btn-warning btn-block" onclick="window.location.href = '/Forms/Seguimiento.aspx';"  >
                    <i class="fa fa-check-circle fa-2x"></i>
                    <h3 class="font-bold no-margins">Seguimiento</h3>
                </button>
            </div>
              <div runat="server" class="col-lg-3" id="div_btn_reportes" visible="false" >
                <button runat="server" id="btn_Reportes" type="button" class="btn dim btn-warning btn-block" onclick="window.location.href = '/Forms/Reportes.aspx';"  >
                    <i class="fa fa-file-pdf-o fa-2x"></i>
                    <h3 class="font-bold no-margins">Reportes General</h3>
                </button>
            </div>
             <div runat="server" class="col-lg-3" id="div_btn_repjornadas" visible="false" >
                <button runat="server" id="Button2" type="button" class="btn dim btn-warning btn-block" onclick="window.location.href = '/Forms/Reportes_Jornadas.aspx';"  >
                    <i class="fa fa-calendar fa-2x"></i>
                    <h3 class="font-bold no-margins">Reporte KZ Amiga</h3>
                </button>
            </div>

              <div runat="server" class="col-lg-3" id="div_btn_repmunicipios" visible="false" >
                <button runat="server" id="btn_Rep_municipios" type="button" class="btn dim btn-warning btn-block" onclick="window.location.href = '/Forms/Reportes_Municipios.aspx';"  >
                    <i class="fa fa-map-marker fa-2x"></i>
                    <h3 class="font-bold no-margins">Reporte Municipios</h3>
                </button>
            </div>

             <div runat="server" class="col-lg-3" id="div_btn_usuarios" visible="false">
                <button runat="server" id="btn_Usuarios" type="button" class="btn dim btn-warning btn-block " onclick="window.location.href = '/Forms/catalogo_usuarios.aspx';"  >
                    <i class="fa fa-users fa-2x"></i>
                    <h3 class="font-bold no-margins">Usuarios</h3>
                </button>
            </div>

              <div runat="server" class="col-lg-3" id="div_tbn_accessos" visible="false">
                <button runat="server" id="btn_Accesos" type="button" class="btn dim btn-warning btn-block " onclick="window.location.href = '/Forms/catalogo_accesos_usuario.aspx';"  >
                    <i class="fa fa-lock fa-2x"></i>
                    <h3 class="font-bold no-margins">Accesos</h3>
                </button>
            </div>

             <div runat="server" class="col-lg-3" id="div_btn_jornadas" visible="false">
                <button runat="server" id="btn_Jornadas" type="button" class="btn dim btn-warning btn-block " onclick="window.location.href = '/Forms/catalogo_jornadas.aspx';"  >
                    <i class="fa fa-list-alt fa-2x"></i>
                    <h3 class="font-bold no-margins">Catalogo Jornadas</h3>
                </button>
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
    <!-- Jquery Validate -->
    <script src="<%= ResolveUrl("~/content/js/plugins/validate/jquery.validate.min.js") %>"></script>
    <script type="text/javascript">
       
        $(document).ready(function () {
            ejecuta_javascript();
        });

        function ejecuta_javascript() {

            $('.i-checks').iCheck({
                checkboxClass: 'icheckbox_square-green',
                radioClass: 'iradio_square-green',
            });


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

        }
        

    </script>
  
</asp:Content>

