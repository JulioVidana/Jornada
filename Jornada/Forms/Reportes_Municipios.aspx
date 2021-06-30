<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Reportes_Municipios.aspx.cs" Inherits="Forms_Reportes_Municipios" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="<%= ResolveUrl("~/content/css/plugins/dataTables/datatables.min.css") %>" rel="stylesheet">
    <link href="<%= ResolveUrl("~/content/css/plugins/iCheck/custom.css") %>" rel="stylesheet">
    <link href="<%= ResolveUrl("~/content/css/plugins/awesome-bootstrap-checkbox/awesome-bootstrap-checkbox.css") %>" rel="stylesheet">
    <link href="<%= ResolveUrl("~/content/css/plugins/sweetalert/sweetalert.css") %>" rel="stylesheet">
    <link href="<%= ResolveUrl("~/content/css/plugins/chosen/bootstrap-chosen.css") %>" rel="stylesheet">
    <link href="<%= ResolveUrl("~/content/css/plugins/footable/footable.core.css") %>" rel="stylesheet">
   
    <style>
        .btne {
            margin-top: 27px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="Server">

   

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="row">

        <div class="ibox float-e-margins">

            <div class="ibox-content">
                <h2><strong>Reporte Por Municipios</strong> </h2>

               
            </div>
        </div>

    </div>


       <div class="ibox animated fadeInRight">
        <asp:UpdatePanel runat="server" ID="UP_Totales" UpdateMode="Always">
            <ContentTemplate>
                <div class="ibox-content p0">
                    <div class="row">
                        <ul style="display: table; width: 100%; table-layout: fixed;">

                            <li style="display: table-cell;"><i class="mdi mdi-24px mdi-calendar-multiple-check"></i> Total Jornadas &nbsp
                        <button type="button" class="btn btn-danger m-r-sm">
                            <asp:Label ID="lblJornadas" runat="server"></asp:Label></button></li>
                            <li style="display: table-cell;"><i class="mdi mdi-24px mdi-account-group"></i> Total Atendidos &nbsp
                        <button type="button" class="btn btn-danger m-r-sm">
                            <asp:Label ID="lblbeneficiarios" runat="server"></asp:Label></button></li>
                            
                        </ul>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>


    <div class="row animated fadeInRight">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title my-ibox">
                    <h5>Jornadas</h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>
                    </div>
                </div>
                <div class="ibox-content">

                    <div class="table-responsive">
                        <asp:UpdatePanel runat="server" ID="UP_grid" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:GridView ID="GridSol" runat="server" AutoGenerateColumns="False"
                                    CssClass="table table-striped table-bordered table-hover dataTables-example"
                                    DataKeyNames="id_jornada"
                                    OnPreRender="GridSol_PreRender"
                                    OnRowCommand="Soli_Grid_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="id_jornada" HeaderText="ID" />
                                        <asp:BoundField DataField="jornada" HeaderText="Jornada" />
                                        <asp:BoundField DataField="host" HeaderText="Casa Amiga" />
                                        <asp:BoundField DataField="direccion" HeaderText="Dirección" />
                                        <asp:BoundField DataField="colonia" HeaderText="Municipio" />
                                        <asp:BoundField DataField="telefono" HeaderText="Teléfono" />
                                        <asp:BoundField DataField="Total" HeaderText="Atendidos" />
                                        <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                                        <asp:ButtonField Text="<i class='fa fa-pie-chart' data-toggle='tooltip' data-placement='bottom' title='Imprime Reporte'></i>" CommandName="reporte"
                                            ButtonType="Link" ControlStyle-CssClass="btn btn-warning" ItemStyle-Width="2%" />
                                   
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


 

    <%--VENTANA VER REPORTE--%>
     <div id="Imprimir_Div" class="modal fade" tabindex="-1" role="dialog" data-backdrop="static" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>

                            <h4 class="modal-title"><i  class='mdi text-danger mdi mdi-chart-areaspline'></i> Imprimir Reporte</h4>
                        </div>
                        <div class="modal-body">
                            <rsweb:ReportViewer ID="Reporte_ReportViewer" runat="server" Width="100%" AsyncRendering="false" OnPageNavigation="Reporte_ReportViewer_PageNavigation" ></rsweb:ReportViewer>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-white" data-dismiss="modal">Regresar</button>
                        </div>
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

    
    <script type="text/javascript">
     

        $(document).ready(function () {
            ejecuta_javascript();
        });


        function ejecuta_javascript() {
          
             
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
                destroy: true,
                pageLength: 10,
                responsive: true,
                dom: '<"html5buttons"B>lTfgitp',
                 buttons: [
                    
                ]
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



    </script>

</asp:Content>

