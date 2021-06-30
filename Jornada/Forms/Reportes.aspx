<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Reportes.aspx.cs" Inherits="Forms_Reportes" %>

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
                <h2>Filtros</h2>

                <asp:UpdatePanel runat="server" ID="UP_agregar" UpdateMode="Always">
                    <ContentTemplate>
                        <div class="form-group  row">
                            <div class="col-md-3 col-sm-12 col-xs-12">
                                <label>Area</label>
                                <asp:DropDownList ID="DD_Area" runat="server" class="chosen-select" DataTextField="Area"
                                    AppendDataBoundItems="true" DataValueField="id_area">
                                    <asp:ListItem Value="0" Text="Seleccionar opción" Selected="True">
                                    </asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3 col-sm-12 col-xs-12">
                                <label>Jornada</label>
                                <asp:DropDownList ID="DD_Jornadas" runat="server" class="chosen-select" DataTextField="jornada" required="true"
                                    AppendDataBoundItems="true" DataValueField="id_jornada">
                                    <asp:ListItem Value="0" Text="---Selecciona Jornada---"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3 col-sm-12 col-xs-12">
                                <label>Sexo</label>
                                <asp:DropDownList ID="DD_Sexo" runat="server" class="chosen-select" DataTextField="sexo"
                                    AppendDataBoundItems="true" DataValueField="id_sexo">
                                    <asp:ListItem Value="0" Text="Seleccionar opción" Selected="True">
                                    </asp:ListItem>
                                    <asp:ListItem Value="F">FEMENINO</asp:ListItem>
                                    <asp:ListItem Value="M">MASCULINO</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3 col-sm-12 col-xs-12">
                                <label>Colonia</label>
                                <asp:DropDownList ID="DD_Colonia" runat="server" class="chosen-select" DataTextField="colonia" required="true"
                                    AppendDataBoundItems="true" DataValueField="id_colonia">
                                    <asp:ListItem Value="0" Text="---Selecciona Colonia---"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>

                        <div class="form-group  row">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <asp:LinkButton runat="server" ID="LinkButton1" Text="<i class='fa fa-rotate-right'></i>" CssClass="btn  btn-default" OnClick="clear" />
                                <asp:Button ID="Btn_Buscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="Filtrar" />
                                <asp:LinkButton runat="server" ID="Btn_Reporte" Text="<i class='fa fa-file-pdf-o'></i> Imprimir Reporte" CssClass="btn  btn-success" OnClick="Imprimir_Button_Click" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>


            </div>
        </div>

    </div>





    <div class="ibox animated fadeInRight">
        <asp:UpdatePanel runat="server" ID="UP_Totales" UpdateMode="Always">
            <ContentTemplate>
                <div class="ibox-content p0">
                    <div class="row">
                        <ul style="display: table; width: 100%; table-layout: fixed;">

                            <li style="display: table-cell;"><i class="mdi mdi-24px mdi-account-group"></i>Beneficiarios &nbsp
                        <button type="button" class="btn btn-danger m-r-sm">
                            <asp:Label ID="lblbeneficiarios" runat="server"></asp:Label></button></li>
                            <li style="display: table-cell;"><i class="mdi mdi-24px mdi-human-male"></i>Hombres &nbsp
                        <button type="button" class="btn btn-danger m-r-sm">
                            <asp:Label ID="lblHombres" runat="server"></asp:Label></button></li>
                            <li style="display: table-cell;"><i class="mdi mdi-24px mdi-human-female"></i>Mujeres &nbsp
                        <button type="button" class="btn btn-danger m-r-sm">
                            <asp:Label ID="lblMujeres" runat="server"></asp:Label></button></li>
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
                    <h5>Padrón</h5>
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
                                    DataKeyNames="id_persona" OnRowDataBound="Pintate1"
                                    OnPreRender="GridSol_PreRender"
                                    OnRowCommand="Soli_Grid_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="completo" HeaderText="Nombre" />
                                        <asp:BoundField DataField="direccion" HeaderText="Dirección" />
                                        <asp:BoundField DataField="colonia" HeaderText="Colonia" />
                                        <asp:BoundField DataField="celular" HeaderText="Celular" />
                                        <asp:BoundField DataField="email" HeaderText="eMail" />
                                        <asp:BoundField DataField="apoyo" HeaderText="Apoyo" />
                                        <asp:BoundField DataField="Area" HeaderText="Area" />
                                        <asp:BoundField DataField="Estatus" HeaderText="Seguimiento" />
                                        <asp:BoundField DataField="sexo" HeaderText="Sexo" />
                                        <asp:ButtonField Text="<i class='mdi mdi-magnify' data-toggle='tooltip' data-placement='bottom' title='Seguimiento'></i>" CommandName="segui"
                                            ButtonType="Link" ControlStyle-CssClass="btn btn-success" ItemStyle-Width="2%" />
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="ap_paterno" HeaderText="Paterno" />
                                        <asp:BoundField DataField="ap_materno" HeaderText="Materno" />
                                        <asp:BoundField DataField="id_colonia" HeaderText="id col" />
                                        <asp:BoundField DataField="id_estatus" HeaderText="id estatus" />
                                        <asp:BoundField DataField="descripcion" HeaderText="descripcion" />
                                        <asp:BoundField DataField="id_persona" HeaderText="ID" />
                                        <asp:BoundField DataField="ApoyoId" HeaderText="ApoyoId" />
                                        <%--<asp:BoundField DataField="tipo_apoyo" HeaderText="tipo_apoyo" />--%>
                                        <asp:BoundField DataField="jornada" HeaderText="jornada" />
                                        <asp:BoundField DataField="id_jornada" HeaderText="id_jornada" />
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


 


    <%--VENTANA PARA SEGUIMIENTO--%>

    <div id="Div_Seguimiento" class="modal fade" tabindex="-1" role="dialog" data-backdrop="static" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="Updatepanel1" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="Label1" runat="server" Text="<i  class='mdi text-danger mdi-checkbox-marked'></i> Seguimiento de Solicitud"></asp:Label></h4>
                            <asp:HiddenField runat="server" ID="ID_Persona_Hidden_segui"></asp:HiddenField>

                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <form role="form">

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Nombre:</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="lblNombre2" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Apoyo:</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="text_asunto2" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control diff-textarea" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="hr-line-dashed"></div>

                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Seguimiento:</label>
                                        <div class="col-sm-10">
                                            <asp:RadioButtonList ID="RB_estatus" runat="server" CssClass="i-checks" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="&nbsp; Positivo &nbsp;" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="&nbsp; Negativo " Value="3"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-2 col-form-label">Observación:</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtObservacion" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control diff-textarea" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>


                                </form>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-white" data-dismiss="modal">Regresar</button>

                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

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
                    { extend: 'excel', title: 'Padrón de Jornadas' },
                    {
                        extend: 'print',
                        customize: function (win) {
                            $(win.document.body).addClass('white-bg');
                            $(win.document.body).css('font-size', '10px');
                            $(win.document.body).find('table')
                                .addClass('compact')
                                .css('font-size', 'inherit');
                        }
                    }
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

